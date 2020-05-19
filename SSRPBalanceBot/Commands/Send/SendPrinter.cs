using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using Discord;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Printer : ModuleBase<SocketCommandContext>
{
    [Command("printer", RunMode = RunMode.Async)]
    [Alias("guap", "p")]
    [Summary("Returns printer info.")]
    public async Task SendPrinter(string item, int boost = 1, int time = 1)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendPrinter) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        SSRPItems.Printer p = await SSRPItems.GetPrinter(item);

        if (p == null) { await Context.Channel.SendMessageAsync("Printer not found. Please enclose the printer name in quotes: `\"name\"`"); await Utilities.StatusMessage("printer", Context); }
        else
        {


            //If loot/gem/uranium
            if(p.printerName == "Uranium" || p.printerName == "Loot" || p.printerName == "Gem")
            {
                //If 1, don't print the plural
                if (time == 1)
                {
                    EmbedBuilder eb = new EmbedBuilder();
                    EmbedFooterBuilder fb = new EmbedFooterBuilder();

                    fb.WithText($"Called by {Context.Message.Author.Username}");
                    fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

                    eb.WithTitle($"{p.printerName}");
                    eb.AddField("Per Second", $"{((p.perSecond * boost) * time).ToString("#,##0")}");
                    eb.AddField("Per Minute", $"{(((p.perSecond * 60) * boost) * time).ToString("#,##0")}");
                    eb.AddField("Per Hour", $"{(((p.perSecond * 60) * 60) * boost).ToString("#,##0")}");
                    eb.AddField("With Boost", $"x{boost}");
                    eb.WithColor(Color.Blue);
                    eb.WithFooter(fb);

                    await ReplyAsync("", false, eb.Build());

                    await Utilities.StatusMessage("printer", Context);
                }
                else
                {
                    EmbedBuilder eb = new EmbedBuilder();
                    EmbedFooterBuilder fb = new EmbedFooterBuilder();

                    fb.WithText($"Called by {Context.Message.Author.Username}");
                    fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

                    eb.WithTitle($"{p.printerName}");
                    eb.AddField($"Per {time} Seconds", $"{((p.perSecond * boost) * time).ToString("#,##0")}");
                    eb.AddField($"Per {time} Minutes", $"{(((p.perSecond * 60) * boost) * time).ToString("#,##0")}");
                    eb.AddField($"Per {time} Hours", $"{((((p.perSecond * 60) * 60) * boost) * time).ToString("#,##0")}");
                    eb.AddField($"With Boost", $"x{boost}");
                    eb.WithColor(Color.Blue);
                    eb.WithFooter(fb);

                    await ReplyAsync("", false, eb.Build());

                    await Utilities.StatusMessage("printer", Context);
                }
            }
            //If normal printer
            else
            {
                //If 1, don't print the plural
                if(time == 1)
                {
                    EmbedBuilder eb = new EmbedBuilder();
                    EmbedFooterBuilder fb = new EmbedFooterBuilder();

                    fb.WithText($"Called by {Context.Message.Author.Username}");
                    fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

                    eb.WithTitle($"{p.printerName}");
                    eb.AddField("Per Second", $"${((p.perSecond * boost) * time).ToString("#,##0")}");
                    eb.AddField("Per Minute", $"${(((p.perSecond * 60) * boost) * time).ToString("#,##0")}");
                    eb.AddField("Per Hour", $"${(((p.perSecond * 60) * 60) * boost).ToString("#,##0")}");
                    eb.AddField("With Boost", $"x{boost}");
                    eb.WithColor(Color.Blue);
                    eb.WithFooter(fb);

                    await ReplyAsync("", false, eb.Build());

                    await Utilities.StatusMessage("printer", Context);
                }
                else
                {
                    EmbedBuilder eb = new EmbedBuilder();
                    EmbedFooterBuilder fb = new EmbedFooterBuilder();

                    fb.WithText($"Called by {Context.Message.Author.Username}");
                    fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

                    eb.WithTitle($"{p.printerName}");
                    eb.AddField($"Per {time} Seconds", $"${((p.perSecond * boost) * time).ToString("#,##0")}");
                    eb.AddField($"Per {time} Minutes", $"${(((p.perSecond * 60) * boost) * time).ToString("#,##0")}");
                    eb.AddField($"Per {time} Hours", $"${((((p.perSecond * 60) * 60) * boost)* time).ToString("#,##0")}");
                    eb.AddField($"With Boost", $"x{boost}");
                    eb.WithColor(Color.Blue);
                    eb.WithFooter(fb);

                    await ReplyAsync("", false, eb.Build());

                    await Utilities.StatusMessage("printer", Context);
                }
            }
        }

    }
}