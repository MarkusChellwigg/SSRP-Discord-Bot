using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using QueryMaster;
using System.Collections.Generic;
using Discord;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class ServerInfo : ModuleBase<SocketCommandContext>
{
    [Command("online", RunMode = RunMode.Async)]
    [Summary("Displays players online for each server")]
    public async Task SendInfo()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.User) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        try
        {
            QueryMaster.Server server1 = ServerQuery.GetServerInstance(EngineType.Source, "54.36.229.194", 27015);
            QueryMaster.ServerInfo s1Info = server1.GetInfo();
            QueryMaster.Server server3 = ServerQuery.GetServerInstance(EngineType.Source, "51.81.120.22", 27015);
            QueryMaster.ServerInfo s3Info = server3.GetInfo();

            EmbedBuilder eb = new EmbedBuilder();
            EmbedFooterBuilder fb = new EmbedFooterBuilder();


            fb.WithText($"Called by {Context.Message.Author.Username}");
            fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

            eb.WithTitle($"Server Info");
            eb.AddField("Server 1", $"[{s1Info.Players}/{s1Info.MaxPlayers}](http://s1.nickgor.com)", true);
            eb.AddField("Server 3", $"[{s3Info.Players}/{s3Info.MaxPlayers}](http://s3.nickgor.com)", true);

            eb.WithColor(Color.Blue);
            eb.WithFooter(fb);

            await ReplyAsync("", false, eb.Build());

            await Utilities.StatusMessage("online", Context);
        }
        catch (Exception)
        {
            await ReplyAsync("A connection could not be made to one or more ZARP servers. Please try again later.");
            await Utilities.StatusMessage("online", Context);
            return;
        }
    }
}