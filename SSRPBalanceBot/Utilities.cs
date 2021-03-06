﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using System.Text.RegularExpressions;
using System.Linq;

namespace SSRPBalanceBot
{
    class Utilities
    {
        public static List<ForumTemplate> templateList = SSRPItems.FillList<ForumTemplate>("Templates/templates.json");

        public static dynamic GetStatistics()
        {
            using (WebClient wc = new WebClient())
            {
                dynamic json = JsonConvert.DeserializeObject(wc.DownloadString("https://nickgor.com/scripts/get_total.php"));
                return json;
            }
        }

        public static async Task<string> GetGang(string steamID)
        {
            using (WebClient wc = new WebClient()) 
            { 
                string page = await wc.DownloadStringTaskAsync(new Uri($"http://fastdl.friendlyplayers.com/loadingscreen/zrp/?steam={steamID}")); 
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

        private static Random randomStr = new Random(DateTime.Now.Millisecond);
        //Generates a random string from the characters in the variable chars
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[randomStr.Next(s.Length)]).ToArray());
        }

        public static Task<ForumTemplate> GetTemplate(string input)
        {
            return Task.Run(() =>
            {
                foreach (ForumTemplate template in templateList)
                {
                    foreach (string alias in template.aliases)
                    {
                        if (input.ToLower() == alias.ToLower())
                        {
                            return template;
                        }
                    }
                }
                return null;
            });
        }

        public static string GetDID(string text)
        {
            text = text ?? string.Empty;
            return new string(text.Where(p => char.IsDigit(p)).ToArray());
        }


        public class ForumTemplate
        {
            public string name { get; set; }
            public string bbcode { get; set; }
            public string[] aliases { get; set; }
        }
    }
}
