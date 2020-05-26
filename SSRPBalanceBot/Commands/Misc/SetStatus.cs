using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
[Group("admin")]
public class SetGame : ModuleBase<SocketCommandContext>
{
    [Command("setgame", RunMode = RunMode.Async)]
    [Summary("Sets the bots status")]
    public async Task SetGameAsync(string status)
    {
        await Program._client.SetGameAsync(status);
        await Utilities.StatusMessage("setstatus", Context);
    }
}