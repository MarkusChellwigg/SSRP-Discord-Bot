using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Average : ModuleBase<SocketCommandContext>
{
    [Command("average", RunMode = RunMode.Async)]
    [Summary("Retuns an average balance of all users in the database")]
    public async Task SendAverage()
    {
        await Context.Channel.SendMessageAsync($"Average balance shared between {Utilities.GetUsers(Utilities.GetStatistics()).ToString("#,##0")} players: `${Utilities.GetAverage(Utilities.GetStatistics()).ToString("#,##0")}`");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [average] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }
}