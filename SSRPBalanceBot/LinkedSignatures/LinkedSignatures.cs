using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Newtonsoft.Json;

namespace SSRPBalanceBot.LinkedSignatures
{
    class LinkedSignatures
    {
        public static List<LinkSignature> linkedSigs = SSRPItems.FillList<LinkSignature>("LinkedSignatures/linkedSignatures.json");


        public static dynamic GetSteam(string id)
        {
            using (WebClient wc = new WebClient())
            {
                dynamic json = JsonConvert.DeserializeObject(wc.DownloadString($"https://nickgor.com/scripts/get_link.php?discordID={id}"));
                return json.steamID;
            }
        }

        public static Task<bool> CheckExists(string input)
        {
            return Task.Run(() =>
            {
                string steamID64 = SteamIDUtils.RetrieveID(input);

                foreach (LinkSignature lc in linkedSigs)
                {
                    if (lc.SteamID64 == steamID64) { return true; }
                }
                return false;
            });
        }

        public static Task<bool> RemoveLink(string id)
        {
            return Task.Run(() =>
            {
                int count = 0;
                if (id.Length == 0) { return false; }
                else
                {
                    string[] admins = File.ReadAllLines("LinkedSignatures/linkedSignatures.json");

                    foreach (var admin in admins)
                    {
                        count++;
                        LinkSignature ls = SSRPItems.ReadFromJsonFile<LinkSignature>(admin);

                        if (ls.DiscordID == id)
                        {
                            var file = new List<string>(System.IO.File.ReadAllLines("LinkedSignatures/linkedSignatures.json"));
                            file.RemoveAt(count - 1);
                            File.WriteAllLines("LinkedSignatures/linkedSignatures.json", file.ToArray());
                            linkedSigs = SSRPItems.FillList<LinkSignature>("LinkedSignatures/linkedSignatures.json");

                            return true;
                        }
                    }

                    return false;
                }
            });
        }


        public class LinkSignature
        {
            public string SteamID64 { get; set; }
            public string DiscordID { get; set; }
        }

    }
}

