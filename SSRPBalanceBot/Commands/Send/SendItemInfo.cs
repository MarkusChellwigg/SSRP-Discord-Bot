using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using Discord;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class ItemInfo : ModuleBase<SocketCommandContext>
{
    [Command("item", RunMode = RunMode.Async)]
    [Summary("Returns item info")]
    public async Task SendItemInfo([Remainder]string item)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendItemInfo) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        SSRPItems.Item i = await SSRPItems.GetItem(item);

        if (i == null) { await Context.Channel.SendMessageAsync("Item not found. Please enclose the item name in quotes: `\"name\"`"); await Utilities.StatusMessage("item", Context); }
        else
        {
            EmbedBuilder eb = new EmbedBuilder();
            EmbedFooterBuilder fb = new EmbedFooterBuilder();


            fb.WithText($"Called by {Context.Message.Author.Username}");
            fb.WithIconUrl(Context.Message.Author.GetAvatarUrl());

            eb.WithTitle($"{i.itemName}");
            eb.AddField("Description", $"{i.itemDesc}");
            eb.AddField("Weight", $"{i.weight}");
            eb.WithColor(Color.Blue);
            eb.WithFooter(fb);

            await ReplyAsync("", false, eb.Build());
            await Utilities.StatusMessage("item", Context);
        }
    }
}