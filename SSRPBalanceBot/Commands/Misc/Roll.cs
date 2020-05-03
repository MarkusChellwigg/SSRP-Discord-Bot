using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Coinflip : ModuleBase<SocketCommandContext>
{
    [Command("coinflip", RunMode = RunMode.Async)]
    public async Task SendRoll(string opponent)
    {
        Random rnd = new Random();

        string oppID = opponent.Replace("<@!", "").Replace(">", "");
        string[] players = { Context.Message.Author.Id.ToString(), oppID };

        await Context.Channel.SendMessageAsync($"<@!{players[rnd.Next(0,2)]}> wins!");
        await Utilities.StatusMessage("roll", Context);
    }
}