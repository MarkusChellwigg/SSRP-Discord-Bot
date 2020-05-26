using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using System.Data;
using Discord;
using System.Text;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
[Group("admin")]
public class GuildList : ModuleBase<SocketCommandContext>
{
    [Command("guildlist", RunMode = RunMode.Async)]
    [Summary("Returns list of guilds the bot is currently in")]
    public async Task SendGuildList()
    {
        StringBuilder sb = new StringBuilder();

        foreach(var guild in Program._client.Guilds)
        {
            sb.Append($"{guild.Name} | {guild.Id}\n");
        }


        EmbedBuilder eb = new EmbedBuilder();
        EmbedFooterBuilder fb = new EmbedFooterBuilder();


        fb.WithText($"Called by {Context.Message.Author.Username}");
        fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

        eb.WithTitle($"Guild List");
        eb.AddField($"{Program._client.Guilds.Count}", sb.ToString());
        eb.WithColor(Color.Blue);
        eb.WithFooter(fb);



        await ReplyAsync("", false, eb.Build());
        await Utilities.StatusMessage("roll", Context);
    }

    [Command("guildlist", RunMode = RunMode.Async)]
    [Summary("Returns members of a guild")]
    public async Task SendGuildList(string guildID)
    {
        StringBuilder sb = new StringBuilder();
        string gName = "";


        foreach (var guild in Program._client.Guilds)
        {
            if (guild.Id.ToString() == guildID)
            {
                foreach (var member in guild.Users)
                {
                    sb.Append(member.Username + "\n");
                }
                gName = guild.Name;
            }
        }


        EmbedBuilder eb = new EmbedBuilder();
        EmbedFooterBuilder fb = new EmbedFooterBuilder();


        fb.WithText($"Called by {Context.Message.Author.Username}");
        fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

        eb.WithTitle($"Guild List");
        eb.AddField($"{gName}", sb.ToString());
        eb.WithColor(Color.Blue);
        eb.WithFooter(fb);



        await ReplyAsync("", false, eb.Build());
        await Utilities.StatusMessage("roll", Context);
    }
}