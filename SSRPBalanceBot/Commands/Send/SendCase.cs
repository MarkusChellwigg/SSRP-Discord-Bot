using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using System.Text;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Case : ModuleBase<SocketCommandContext>
{
    [Command("case", RunMode = RunMode.Async)]
    [Alias("odds","chances")]
    [Summary("Returns info about the specified case.")]
    public async Task SendCase([Remainder]string item)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.User) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        StringBuilder sb = new StringBuilder();
        int count = 0;

        SSRPItems.Case c = await SSRPItems.GetCase(item);
        if (c == null) { await Context.Channel.SendMessageAsync("Specified case not found. You can request for it to be added by contacting an Admin."); return; }

        foreach(string i in c.items)
        {
            sb.Append($"Item: {i} | Odds: {c.odds[count]}%\n");
            count++;
        }

        await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention}\n```--- The {c.caseName} Case ---\n{sb.ToString()}```");
        await Utilities.StatusMessage("case", Context);
    }
}