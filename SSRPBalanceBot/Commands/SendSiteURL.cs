using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class SiteURL : ModuleBase<SocketCommandContext>
{
    [Command("site", RunMode = RunMode.Async)]
    [Summary("Link to the site")]
    public async Task SendSiteURL()
    {
        await Context.Channel.SendMessageAsync($"https://nickgor.com/");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [site] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }
}