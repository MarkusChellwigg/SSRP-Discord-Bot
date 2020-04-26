using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class SiteURL : ModuleBase<SocketCommandContext>
{
    [Command("site", RunMode = RunMode.Async)]
    [Summary("Link to the site")]
    public async Task SendSiteURL()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendSiteURL) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        await Context.Channel.SendMessageAsync($"https://nickgor.com/");
        await Utilities.StatusMessage("site", Context);
    }
}