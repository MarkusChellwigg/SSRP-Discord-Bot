using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace SSRPBalanceBot.Permissions
{
    class PermissionManager
    {
        static List<Admin> admins = SSRPItems.FillList<Admin>("Users/admins.json");
        public static async Task<bool> AddAdmin(ulong id, string name, int permLevel)
        {
            if (CheckAdmin(id)) { return false; }

            Admin a = new Admin { id = id.ToString(), name = name, permLevel = permLevel};
            SSRPItems.WriteToJsonFile<Admin>("Users/admins.json", a, true);
            admins.Add(a);

            return true;
        }

        public static async Task<bool> RemoveAdmin(string id)
        {
            int count = 0;
            if (id == "") { return false; }
            else
            {
                string[] admins = File.ReadAllLines("Users/admins.json");

                foreach (var admin in admins)
                {
                    count++;
                    Admin a = SSRPItems.ReadFromJsonFile<Admin>(admin);

                    if (a.id == id)
                    {
                        var file = new List<string>(System.IO.File.ReadAllLines("Users/admins.json"));
                        file.RemoveAt(count - 1);
                        File.WriteAllLines("Users/admins.json", file.ToArray());
                        return true;
                    }
                }

                return false;
            }
        }

        public static Task ReloadPermissions()
        {
            admins.Clear();
            admins = SSRPItems.FillList<Admin>("Users/admins.json");
            return Task.CompletedTask;
        }


        public static bool CheckAdmin(ulong id)
        {
            //string[] admins = File.ReadAllLines("Users/admins.json");

            foreach(Admin admin in admins)
            {
                //Admin a = SSRPItems.ReadFromJsonFile<Admin>(admin);
                if (admin.id == id.ToString()) { return true; }
            }
            return false;
        }

        public static int GetPerms(ulong id)
        {
            //string[] admins = File.ReadAllLines("Users/admins.json");

            foreach (Admin admin in admins)
            {
                //Admin a = SSRPItems.ReadFromJsonFile<Admin>(admin);
                if (admin.id == id.ToString()) 
                {
                    return admin.permLevel;
                }
            }
            return -1;
        }

    }

    public class Admin
    {
        public string id { get; set; }
        public string name { get; set; }
        public int permLevel { get; set; }
    }

    public class PermissionConfig
    {
        public static int WitchHunt = -1;
        public static int SendTotal = -1;
        public static int SendStatistics = -1;
        public static int SendSiteURL = -1;
        public static int SendSignature = -1;
        public static int SendPrinter = -1;
        public static int SendHelp = -1;
        public static int SendDatabaseURL = -1;
        public static int SendBind = -1;
        public static int SendAverage = -1;
        public static int SendAdminHelp = 100;
        public static int RemoveItems = 100;
        public static int RemoveAdmin = 100;
        public static int ReloadItems = 100;
        public static int ReloadPermissions = 100;
        public static int ReloadLinks = 100;
        public static int AddItem = 100;
        public static int AddAdmin = 100;
        public static int Link = -1;
        public static int Unlink = -1;

    }
}
