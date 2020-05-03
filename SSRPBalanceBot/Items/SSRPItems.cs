using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SSRPBalanceBot
{
    class SSRPItems
    {
        public static List<Bind> bindList = FillList<Bind>("Items/binds.json");
        public static List<Printer> printerList = FillList<Printer>("Items/printers.json");
        public static List<Suit> suitList = FillList<Suit>("Items/suits.json");
        public static List<Item> itemList = FillList<Item>("Items/items.json");
        public static List<Case> caseList = FillList<Case>("Items/cases.json");

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



        public static Task<string> GetBind(string input)
        {
            return Task.Run(() =>
            {
                foreach (Bind bind in bindList)
                {
                    foreach (string alias in bind.aliases)
                    {
                        if (input == alias)
                        {
                            return bind.bind;
                        }
                    }
                }
                return null;
            });
        }

        public static Task<Printer> GetPrinter(string input)
        {
            return Task.Run(() =>
            {
                foreach (Printer printer in printerList)
                {
                    foreach (string alias in printer.aliases)
                    {
                        if (input.ToLower() == alias.ToLower())
                        {
                            return printer;
                        }
                    }
                }
                return null;
            });
        }

        public static Task<Suit> GetSuit(string input)
        {
            return Task.Run(() =>
            {
                foreach (Suit suit in suitList)
                {
                    foreach (string alias in suit.aliases)
                    {
                        if (input.ToLower() == alias.ToLower())
                        {
                            return suit;
                        }
                    }
                }
                return null;
            });
        }

        public static Task<Item> GetItem(string input)
        {
            return Task.Run(() =>
            {
                foreach (Item item in itemList)
                {
                    foreach (string alias in item.aliases)
                    {
                        if (input.ToLower() == alias.ToLower())
                        {
                            return item;
                        }
                    }
                }
                return null;
            });

        }

        public static Task<Case> GetCase(string input)
        {
            return Task.Run(() =>
            {
                foreach (Case item in caseList)
                {
                    foreach (string alias in item.aliases)
                    {
                        if (input.ToLower() == alias.ToLower())
                        {
                            return item;
                        }
                    }
                }
                return null;
            });
        }

        public class Item
        {
            public string itemName { get; set; }
            public string category { get; set; }
            public string info { get; set; }
            public string[] aliases { get; set; }
        }

        public class Bind
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

        public class Suit
        {
            public string suitName { get; set; }
            public int hp { get; set; }
            public int armor { get; set; }
            public double speed { get; set; }
            public string ability { get; set; }
            public string[] aliases { get; set; }
        }
        public class Case
        {
            public string caseName { get; set; }
            public string[] items { get; set; }
            public double[] odds { get; set; }
            public string[] aliases { get; set; }
        }
    }
}
