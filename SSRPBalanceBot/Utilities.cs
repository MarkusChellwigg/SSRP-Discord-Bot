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
        public static List<Insult> insultsList = SSRPItems.FillList<Insult>("Items/insults.json");

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

        public static async Task<GangInfo> GetGangInfo(string gangName)
        {
            using (WebClient wc = new WebClient())
            {
                string page = await wc.DownloadStringTaskAsync(new Uri($"https://zarpgaming.com/index.php/leaderboards/darkrp/gangs?search={gangName}"));

                Regex regex = new Regex("specialrow\">(?s)(.*)<\\/tr>|krow(?s)(.*)<\\/tr>");
                Regex infoX = new Regex("(?<=<td class=\"kcol-mid\">)(.*)(?=<\\/td>)");
                Regex gangIcon = new Regex("(?<=\" src=\")(.*?)(?=\")");
                Regex gangOwner = new Regex("(?<=rel=\"nofollow\">)(.*)(?=<\\/a>)");

                string gangRow = regex.Match(page).ToString();

                //mc[0] = gang icon
                //mc[1] = gang name
                //mc[2] = gang owner
                //mc[3] = member count
                //mc[4] = gang cash
                //mc[5] = gang loot

                MatchCollection mc = infoX.Matches(gangRow);

                string gIcon = gangIcon.Match(mc[0].ToString()).ToString();
                string gName = mc[1].ToString();

                string gOwner;
                if (mc[2].ToString().Contains("nofollow")) 
                {
                    gOwner = gangOwner.Match(mc[2].ToString()).ToString();
                }
                else
                {
                    gOwner = mc[2].ToString();
                }

                string memberCount = mc[3].ToString();
                string gangCash = mc[4].ToString();
                string gangLoot = mc[5].ToString();

                GangInfo gInfo = new GangInfo { gangIcon = gIcon, gangName = gName, gangCash = gangCash, gangLoot = gangLoot, memberCount = memberCount, gangOwner = gOwner};

                return gInfo;
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

        private static Random randomStr = new Random(DateTime.Now.Millisecond);
        //Generates a random string from the characters in the variable chars
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[randomStr.Next(s.Length)]).ToArray());
        }

        public class Insult
        {
            public string insult { get; set; }
        }

        public class GangInfo
        {
            public string gangIcon { get; set; }
            public string gangName { get; set; }
            public string gangOwner { get; set; }
            public string memberCount { get; set; }
            public string gangCash { get; set; }
            public string gangLoot { get; set; }
        }

    }
}
