using System;
using System.Net;
using System.Xml;
using Newtonsoft.Json;

namespace SSRPBalanceBot
{
    public class SteamIDUtils
    {
        
        //SteamID's can also be calculated without the use of an API - Here for convenience
        static string apiURL = "https://api.steamid.uk/convert.php?api=APIKEYHERE";
        static string nameAPI = "https://steamidapi.uk/request.php?api=APIKEYHERE";

        private static string[] GetSteamIDs(string input)
        {
            //Console.WriteLine("Contacting API");
            WebClient wc = new WebClient();
            dynamic j = JsonConvert.DeserializeObject(wc.DownloadString(apiURL + $"&input={input}&format=json"));
            string[] ids = { j.converted.steamid, j.converted.steamid64 };
            return ids;
        }

        public static string RetrieveID(string input)
        {
            //Console.WriteLine("Retrieving SteamID64");
            if (input.StartsWith("http://steamcommunity.com/profiles/") || input.StartsWith("https://steamcommunity.com/profiles/"))
            {
                if (input.StartsWith("http:")) { return GetSteamIDs(input.Replace("http://steamcommunity.com/profiles/", ""))[1]; }
                else { return GetSteamIDs(input.Replace("https://steamcommunity.com/profiles/", ""))[1]; }
            }
            else if (input.StartsWith("STEAM_") || input.StartsWith("7656119"))
            {
                return GetSteamIDs(input)[1];
            }
            return null;
        }

        public static string GetName(string steamID64)
        {
            try
            {
                WebClient wc = new WebClient();
                string xml = wc.DownloadString($"http://steamcommunity.com/profiles/{steamID64}/?xml=1");
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xml);
                XmlNodeList nodeList = doc.SelectNodes("/profile");

                return nodeList[0].SelectSingleNode("steamID").InnerText;
                

            }
            catch (Exception e)
            {
                return e.Message;
            }
        }
    }
}
