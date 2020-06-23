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
    [Alias("sp00ns")]
    [Summary("Spoons.")]
    public async Task SendSpoons()
    {
        await ReplyAsync("https://cdn.discordapp.com/attachments/721875650738258063/722895001419251802/unknown.png");
        await Utilities.StatusMessage("case", Context);
    }
}