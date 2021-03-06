using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using SSRPBalanceBot.LinkedSignatures;
using Discord;
using System.Web;
using SSRPBalanceBot.Leaderboards;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Ganginfo : ModuleBase<SocketCommandContext>
{
    [Command("ganginfo")]
    [Summary("Returns information about the specified gang")]
    public async Task SendGangInfo([Remainder]string gang)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.User) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        LeaderboardUtils.GangInfo gInfo = await LeaderboardUtils.GetGangInfo(HttpUtility.UrlEncode(gang));

        Console.WriteLine(gInfo.gangOwner);

        EmbedBuilder eb = new EmbedBuilder();
        EmbedFooterBuilder fb = new EmbedFooterBuilder();


        fb.WithText($"Called by {Context.Message.Author.Username}");
        fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

        eb.WithTitle($"{gInfo.gangName}");
        eb.AddField("Owner", $"{gInfo.gangOwner}");
        eb.AddField("Member Count", $"{gInfo.memberCount}");
        eb.AddField("Cash", $"{gInfo.gangCash}");
        eb.AddField("Loot", $"{gInfo.gangLoot}");
        if (gInfo.gangIcon.Contains("imgur")) { eb.WithThumbnailUrl(gInfo.gangIcon); }
        eb.WithColor(Color.Blue);
        eb.WithFooter(fb);

        await ReplyAsync("", false, eb.Build());

        await Utilities.StatusMessage("ganginfo", Context);
    }
}