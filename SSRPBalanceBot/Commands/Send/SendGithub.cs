using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class GithubURL : ModuleBase<SocketCommandContext>
{
    [Command("github", RunMode = RunMode.Async)]
    [Summary("Link to the bot's GitHub repository")]
    public async Task SendSiteURL()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.User) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        await Context.Channel.SendMessageAsync($"https://github.com/bunnyslippers69");
        await Utilities.StatusMessage("github", Context);
    }
}