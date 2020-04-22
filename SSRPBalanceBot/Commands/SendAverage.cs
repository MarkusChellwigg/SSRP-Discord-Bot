using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Average : ModuleBase<SocketCommandContext>
{
    [Command("average", RunMode = RunMode.Async)]
    [Summary("Retuns an average balance of all users in the database")]
    public async Task SendAverage()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendAverage) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        await Context.Channel.SendMessageAsync($"Average balance shared between {Utilities.GetUsers(Utilities.GetStatistics()).ToString("#,##0")} players: `${Utilities.GetAverage(Utilities.GetStatistics()).ToString("#,##0")}`");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [average] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }
}