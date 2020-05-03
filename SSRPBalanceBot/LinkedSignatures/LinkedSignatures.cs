using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;

namespace SSRPBalanceBot.LinkedSignatures
{
    class LinkedSignatures
    {
        public static List<LinkSignature> linkedSigs = SSRPItems.FillList<LinkSignature>("LinkedSignatures/linkedSignatures.json");


        public static Task<string> GetSteam(string discordID)
        {
            return Task.Run(() =>
            {
                foreach (LinkSignature ls in linkedSigs)
                {
                    if (ls.DiscordID == discordID) { return ls.SteamID64; }
                }
                return null;
            });
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

