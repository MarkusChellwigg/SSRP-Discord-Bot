using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Total : ModuleBase<SocketCommandContext>
{
    [Command("total", RunMode = RunMode.Async)]
    [Summary("Returns the total balance of all users in the database")]
    public async Task SendTotal()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendTotal) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        await Context.Channel.SendMessageAsync($"Total size of SSRP Economy as of {DateTime.Now.Date.ToString("dd/MM/yy")}: `${Convert.ToInt64(Utilities.GetTotal(Utilities.GetStatistics())).ToString("#,##0")}`");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [total] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }
}