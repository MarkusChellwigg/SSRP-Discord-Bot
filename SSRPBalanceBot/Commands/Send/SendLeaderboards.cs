using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using SSRPBalanceBot.LinkedSignatures;
using SSRPBalanceBot.Leaderboards;
using System.Collections.Generic;
using Discord;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Leaderboards : ModuleBase<SocketCommandContext>
{
    [Command("leaderboards")]
    [Alias("boards", "lb")]
    [Summary("Returns leaderboard info.")]
    public async Task SendLeaderboards(string board, string player = null)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendBind) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        var msg = await ReplyAsync("Obtaining data");

        try
        {
            List<LeaderboardUtils.BoardInfo> bList = LeaderboardUtils.GetList(await LeaderboardUtils.GetBoard(board, player), player);

            string bName = board;

            foreach (LeaderboardUtils.Boards b in LeaderboardUtils.boards)
            {
                foreach (string alias in b.aliases)
                {
                    if (alias == board)
                    {
                        bName = b.name;
                    }
                }
            }

            EmbedBuilder eb = new EmbedBuilder();
            EmbedFooterBuilder fb = new EmbedFooterBuilder();


            fb.WithText($"Called by {Context.Message.Author.Username}");
            fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

            eb.WithTitle($"{bName}");
            eb.WithColor(Color.Blue);
            eb.WithFooter(fb);

            foreach (LeaderboardUtils.BoardInfo b in bList)
            {
                eb.AddField($"{b.position}. {b.name}", $"{b.stat}");
            }

            await ReplyAsync("", false, eb.Build());
            await msg.DeleteAsync();

            await Utilities.StatusMessage("leaderboards", Context);
        }
        catch (Exception)
        {
            await ReplyAsync("Either this board doesn't exist or this player doesn't meet the requirements.");
            await msg.DeleteAsync();
            await Utilities.StatusMessage("leaderboards", Context);
            return;
        }
    }
}