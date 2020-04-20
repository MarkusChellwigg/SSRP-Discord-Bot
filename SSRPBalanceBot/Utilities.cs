using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SSRPBalanceBot
{
    class Utilities
    {
        public static dynamic GetStatistics()
        {
            using (WebClient wc = new WebClient())
            {
                dynamic json = JsonConvert.DeserializeObject(wc.DownloadString("https://nickgor.com/scripts/get_total.php"));
                return json;
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

        public static Task StatusMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            return Task.CompletedTask;
        }

        public static string RandomMessage()
        {
            string[] messageList =
            {
                "Die of cancer nigger",
                "Kill yourself",
                "Your mum has aids",
                "Your dad has aids",
                "100% should've been aborted",
                "Cancer looks like a better choice than you",
                "Size doesn't matter, don't worry"
            };

            Random rnd = new Random();
            return messageList[rnd.Next(0, messageList.Length)];
        }
    }
}
