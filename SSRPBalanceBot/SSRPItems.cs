using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SSRPBalanceBot
{
    class SSRPItems
    {
        public static List<Item> itemList = FillList<Item>("Items/items.json");
        public static List<Printer> printerList = FillList<Printer>("Items/printers.json");

        public static void WriteToJsonFile<T>(string filePath, T objectToWrite, bool append = false) where T : new()
        {
            TextWriter writer = null;
            try
            {
                var contentsToWriteToFile = JsonConvert.SerializeObject(objectToWrite);
                writer = new StreamWriter(filePath, append);
                writer.Write(contentsToWriteToFile);
                writer.Write(Environment.NewLine);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }

        public static T ReadFromJsonFile<T>(string line) where T : new()
        {
            TextReader reader = null;
            try
            {
                return JsonConvert.DeserializeObject<T>(line);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        public static List<T> FillList<T>(string path) where T : new()
        {
            List<T> list = new List<T> { };
            string[] lines = File.ReadAllLines(path);
            foreach (var item in lines)
            {
                list.Add(ReadFromJsonFile<T>(item));
            }

            return list;
        }



        public static async Task<string> GetBind(string input)
        {
            foreach (Item item in itemList)
            {
                foreach (string alias in item.aliases)
                {
                    if (input == alias)
                    {
                        return item.bind;
                    }
                }
            }
            return "Not Found";
        }

        public static async Task<Printer> GetPrinter(string input)
        {
            foreach (Printer printer in printerList)
            {
                foreach (string alias in printer.aliases)
                {
                    if (input.ToLower() == alias.ToLower())
                    {
                        Console.WriteLine(printer.printerName);
                        return printer;
                    }
                }
            }
            return null;
        }

        public class Item
        {
            public string bind { get; set; }
            public string[] aliases { get; set; }
        }

        public class Printer
        {
            public string printerName { get; set; }
            public double perSecond { get; set; }
            public string[] aliases { get; set; }
        }
    }
}
