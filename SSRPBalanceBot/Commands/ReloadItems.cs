using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class ReloadItems : ModuleBase<SocketCommandContext>
{
    [Command("reloaditems", RunMode = RunMode.Async)]
    [Summary("Reloads all item lists. Useful after adding new items.")]
    public async Task ReloadItemsAsync()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.ReloadItems) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        SSRPItems.itemList = SSRPItems.FillList<SSRPItems.Item>("Items/items.json");
        SSRPItems.printerList = SSRPItems.FillList<SSRPItems.Printer>("Items/printers.json");
        await Context.Channel.SendMessageAsync("Items have been reloaded.");
    }
}