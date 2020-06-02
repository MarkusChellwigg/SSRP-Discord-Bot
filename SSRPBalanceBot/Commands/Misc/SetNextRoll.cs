using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
[Group("admin")]
public class NextRoll : ModuleBase<SocketCommandContext>
{
    [Command("nextroll", RunMode = RunMode.Async)]
    [Summary("Chooses a random number between 0 and the specified value")]
    public async Task SetNextRoll(int nextRoll)
    {
        Program.nextRoll = nextRoll;

        
        await Utilities.StatusMessage("nextroll", Context);
    }
}