using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using System.Text;
using Discord;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class CaseSim : ModuleBase<SocketCommandContext>
{
    [Command("simulate", RunMode = RunMode.Async)]
    [Summary("Returns case simulations")]
    public async Task SendCaseSimulation(string casE, int amount = 1)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendBind) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        if (amount > 3000 || amount < 1) { await ReplyAsync("Amount can't be higher than 3000 or smaller than 1."); return; }

        List<string> wins = new List<string> { };
        int spins = Convert.ToInt32(amount);
        SSRPItems.Case selectedCase = SelectCase(casE);
        StringBuilder sb = new StringBuilder();

        if (selectedCase == null) { await ReplyAsync("Case not found."); }
        else
        {
            for (int i = 0; i < spins; i++)
            {
                wins.Add(SelectItem(selectedCase));
            }

            var q = from x in wins
                    group x by x into g
                    let count = g.Count()
                    orderby count descending
                    select new { Value = g.Key, Count = count };
            foreach (var x in q)
            {
                sb.Append($"{x.Count} - {x.Value}\n");
            }

            EmbedBuilder eb = new EmbedBuilder();
            EmbedFooterBuilder fb = new EmbedFooterBuilder();


            fb.WithText($"Called by {Context.Message.Author.Username}");
            fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

            eb.WithTitle($"{selectedCase.caseName} Case");
            eb.AddField("Wins", $"{sb.ToString()}");
            eb.WithColor(Color.Blue);
            eb.WithFooter(fb);

            await ReplyAsync("", false, eb.Build());
        }

        await Utilities.StatusMessage("case simulation", Context);
    }


    static List<string> items = new List<string> { };
    static string SelectItem(SSRPItems.Case c)
    {
        int totalOdds = TotalOdds(c);

        for (int i = 0; i < c.items.Count(); i++)
        {
            double add = c.odds[i] * 100;
            for (int j = 0; j < add; j++)
            {
                items.Add(c.items[i]);
            }
        }

        var rand = new Random();
        int num = rand.Next(0, items.Count());
        return items[num];
    }

    static SSRPItems.Case SelectCase(string userInput)
    {
        foreach (SSRPItems.Case c in SSRPItems.caseList)
        {
            foreach (string alias in c.aliases)
            {
                if (alias == userInput)
                {
                    return c;
                }
            }
        }
        return null;
    }

    static int TotalOdds(SSRPItems.Case c)
    {
        int total = 0;
        foreach (int odd in c.odds)
        {
            total += odd;
        }

        return total;
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
}