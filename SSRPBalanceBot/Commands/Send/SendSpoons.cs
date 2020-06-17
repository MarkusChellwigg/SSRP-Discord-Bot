using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using System.Text;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Spoons : ModuleBase<SocketCommandContext>
{
    [Command("spoons", RunMode = RunMode.Async)]
    [Summary("Spoons.")]
    public async Task SendSpoons([Remainder]string item)
    {
        await ReplyAsync("https://cdn.discordapp.com/attachments/686995715314286613/722894347149901854/spoones.png");
        await Utilities.StatusMessage("case", Context);
    }
}