using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using SSRPBalanceBot.LinkedSignatures;
using Discord;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Ganginfo : ModuleBase<SocketCommandContext>
{
    [Command("ganginfo", RunMode = RunMode.Async)]
    [Summary("Returns gang of a user.")]
    public async Task SendGangInfo(string gang)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendBind) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        Utilities.GangInfo gInfo = await Utilities.GetGangInfo(gang);

        EmbedBuilder eb = new EmbedBuilder();
        EmbedFooterBuilder fb = new EmbedFooterBuilder();


        fb.WithText($"Called by {Context.Message.Author.Username}");
        fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

        eb.WithTitle($"{gInfo.gangName}");
        eb.AddField("Owner", $"{gInfo.gangOwner}");
        eb.AddField("Member Count", $"{gInfo.memberCount}");
        eb.AddField("Cash", $"{gInfo.gangCash}");
        eb.AddField("Loot", $"{gInfo.gangLoot}");
        eb.WithThumbnailUrl(gInfo.gangIcon);
        eb.WithColor(Color.Blue);
        eb.WithFooter(fb);

        await ReplyAsync("", false, eb.Build());

        await Utilities.StatusMessage("ganginfo", Context);
    }
}