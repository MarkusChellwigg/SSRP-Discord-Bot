﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using System.Text.RegularExpressions;

namespace SSRPBalanceBot
{
    class Utilities
    {
        public static List<Insult> insultsList = SSRPItems.FillList<Insult>("Items/insults.json");

        public static dynamic GetStatistics()
        {
            using (WebClient wc = new WebClient())
            {
                dynamic json = JsonConvert.DeserializeObject(wc.DownloadString("https://nickgor.com/scripts/get_total.php"));
                return json;
            }
        }

        public static string GetGang(string id)
        {
            using (WebClient wc = new WebClient()) 
            { 
                string page = wc.DownloadString($"http://fastdl.friendlyplayers.com/loadingscreen/zrp/?steam={id}"); 
                Regex regex = new Regex("(?<=<li><span class=\"glyphicon glyphicon-user\"><\\/span> )(.*)(?=<\\/li>)");
                return regex.Match(page).ToString();
            }
        }


        public static string GetTotal(dynamic json)
        {
            return json.sum;
        }

        public static double GetAverage(dynamic json)
        {
            int users = Convert.ToInt32(json.total);
            Int64 total = Convert.ToInt64(json.sum);
            double average = Math.Round(total / users + 0.0, 0);
            return average;
        }

        public static int GetUsers(dynamic json)
        {
            return json.total;
        }

        public static string GetSignature(string input)
        {
            string id = SteamIDUtils.RetrieveID(input);

            if (id == null) { throw new Exception("Input was invalid."); }

            AddNew(id);
            return $"http://fastdl.friendlyplayers.com/siggen/darkrpbase/{id}.png";
        }

        public static async void AddNew(string id)
        {
            await File.AppendAllTextAsync("idList.txt", id + Environment.NewLine);
        }

        public static Task StatusMessage(string cmd, SocketCommandContext Context)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Time: {DateTime.Now} | Ran command: [{cmd}] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
            Console.ForegroundColor = ConsoleColor.Gray;
            return Task.CompletedTask;
        }

        public static string RandomMessage()
        {

            Random rnd = new Random();
            return insultsList[rnd.Next(0, insultsList.Count)].insult;
        }

        public class Insult
        {
            public string insult { get; set; }
        }

    }
}
