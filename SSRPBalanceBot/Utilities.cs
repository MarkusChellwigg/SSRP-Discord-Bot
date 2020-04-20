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
        
        //Retrieves current database statistics
        public static dynamic GetStatistics()
        {
            using (WebClient wc = new WebClient())
            {
                dynamic json = JsonConvert.DeserializeObject(wc.DownloadString("https://nickgor.com/scripts/get_total.php"));
                return json;
            }
        }
        
        //Returns total balance of everyone in the database
        public static string GetTotal(dynamic json)
        {
            return json.sum;
        }
        
        //Returns average balance split between all users currently in the database
        public static double GetAverage(dynamic json)
        {
            int users = Convert.ToInt32(json.total);
            Int64 total = Convert.ToInt64(json.sum);
            double average = Math.Round(total / users + 0.0, 0);
            return average;
        }
        
        //Returns number of users currently in the database
        public static int GetUsers(dynamic json)
        {
            return json.total;
        }
        
        //Returns the link to the specified users signature
        public static string GetSignature(string input)
        {
            string id = SteamIDUtils.RetrieveID(input);

            if (id == null) { throw new Exception("Input was invalid."); }

            AddNew(id);
            return $"http://fastdl.friendlyplayers.com/siggen/darkrpbase/{id}.png";
        }
        
        //Logs all steam64id's to a file for scanning later
        public static async void AddNew(string id)
        {
            await File.AppendAllTextAsync("idList.txt", id + Environment.NewLine);
        }

        //Prints the specified message to the console
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
                "Messages here",
                "Yes"
            };

            Random rnd = new Random();
            return messageList[rnd.Next(0, messageList.Length)];
        }
    }
}
