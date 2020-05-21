using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using System.Data;
using Discord;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Calculator : ModuleBase<SocketCommandContext>
{
    [Command("calculate", RunMode = RunMode.Async)]
    [Summary("Returns the value of the calculation specified")]
    public async Task SendCalc([Remainder]string math)
    {
        EmbedBuilder eb = new EmbedBuilder();
        EmbedFooterBuilder fb = new EmbedFooterBuilder();


        fb.WithText($"Called by {Context.Message.Author.Username}");
        fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

        eb.WithTitle($"{new DataTable().Compute(math, null).ToString()}");
        eb.WithColor(Color.Blue);
        eb.WithFooter(fb);



        await ReplyAsync("", false, eb.Build());
        await Utilities.StatusMessage("roll", Context);
    }
}