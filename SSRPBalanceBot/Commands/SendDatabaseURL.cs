using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class DatabaseURL : ModuleBase<SocketCommandContext>
{
    [Command("database", RunMode = RunMode.Async)]
    [Summary("Link to the SSRP Database")]
    public async Task SendDatabaseURL()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendDatabaseURL) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        await Context.Channel.SendMessageAsync($"https://nickgor.com/SSRPBalances.php");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [database] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }
}