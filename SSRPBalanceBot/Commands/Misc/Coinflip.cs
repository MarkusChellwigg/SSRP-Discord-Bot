using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Roll : ModuleBase<SocketCommandContext>
{
    [Command("roll", RunMode = RunMode.Async)]
    public async Task SendRoll(int max)
    {
        Random rnd = new Random();
        int roll = rnd.Next(0, max);

        if (Context.Message.Author.Id.ToString() == "282947612141682689")
        {
            if(roll < ((max / 100) * 40))
            {
                roll = roll * 2;
            }
        }

        await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} rolled `{roll}`");
        await Utilities.StatusMessage("roll", Context);
    }
}