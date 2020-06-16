using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Statistics : ModuleBase<SocketCommandContext>
{
    [Command("statistics", RunMode = RunMode.Async)]
    [Summary("Returns all statistics")]
    public async Task SendStats()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.User) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        await Context.Channel.SendMessageAsync($"Total size of SSRP Economy as of {DateTime.Now.Date.ToString("dd/MM/yy")}: `${Convert.ToInt64(Utilities.GetTotal(Utilities.GetStatistics())).ToString("#,##0")}`");
        await Context.Channel.SendMessageAsync($"Average balance shared between {Utilities.GetUsers(Utilities.GetStatistics()).ToString("#,##0")} players: `${Utilities.GetAverage(Utilities.GetStatistics()).ToString("#,##0")}`");
        await Utilities.StatusMessage("statistics", Context);
    }
}