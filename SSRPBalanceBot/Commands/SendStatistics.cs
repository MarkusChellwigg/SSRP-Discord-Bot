using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Statistics : ModuleBase<SocketCommandContext>
{
    [Command("statistics", RunMode = RunMode.Async)]
    [Summary("Returns all statistics")]
    public async Task SendStats()
    {
        await Context.Channel.SendMessageAsync($"Total size of SSRP Economy as of {DateTime.Now.Date.ToString("dd/MM/yy")}: `${Convert.ToInt64(Utilities.GetTotal(Utilities.GetStatistics())).ToString("#,##0")}`");
        await Context.Channel.SendMessageAsync($"Average balance shared between {Utilities.GetUsers(Utilities.GetStatistics()).ToString("#,##0")} players: `${Utilities.GetAverage(Utilities.GetStatistics()).ToString("#,##0")}`");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [statistics] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }
}