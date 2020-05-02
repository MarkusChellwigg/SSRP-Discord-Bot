using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class ItemInfo : ModuleBase<SocketCommandContext>
{
    [Command("item", RunMode = RunMode.Async)]
    [Summary("Returns the bind for the specified item.")]
    public async Task SendItemInfo([Remainder]string item)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendItemInfo) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        SSRPItems.Item i = await SSRPItems.GetItem(item);

        if (i == null) { await Context.Channel.SendMessageAsync("Item not found. Please enclose the item name in quotes: `\"name\"`"); await Utilities.StatusMessage("item", Context); }
        else
        {
            await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention}\n`Item Name: {i.itemName}\nCategory: {i.category}\nInfo: {i.info}`");
            await Utilities.StatusMessage("item", Context);
        }
    }
}