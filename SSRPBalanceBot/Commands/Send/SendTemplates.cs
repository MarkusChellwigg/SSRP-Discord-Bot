using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using Discord;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Templates : ModuleBase<SocketCommandContext>
{
    [Command("template")]
    [Summary("Returns the specified forum template")]
    public async Task SendTemplates(string input)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.User) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        Utilities.ForumTemplate t = await Utilities.GetTemplate(input);

        EmbedBuilder eb = new EmbedBuilder();
        EmbedFooterBuilder fb = new EmbedFooterBuilder();


        fb.WithText($"Called by {Context.Message.Author.Username}");
        fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

        eb.WithTitle($"{t.name} Template");
        eb.AddField("BB Code", $"```{t.bbcode}```");
        eb.WithColor(Color.Blue);
        eb.WithFooter(fb);

        await ReplyAsync("", false, eb.Build());
        await Utilities.StatusMessage("total", Context);
    }
}