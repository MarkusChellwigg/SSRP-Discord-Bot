using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class ReloadItems : ModuleBase<SocketCommandContext>
{
    [Command("reloaditems", RunMode = RunMode.Async)]
    [Summary("Reloads all item lists. Useful after adding new items.")]
    public async Task ReloadItemsAsync()
    {
        if (Context.Message.Author.ToString() != "Bunny#9220") { await Context.Channel.SendMessageAsync("No permission"); return; }
        SSRPItems.itemList = SSRPItems.FillList<SSRPItems.Item>("Items/items.json");
        SSRPItems.printerList = SSRPItems.FillList<SSRPItems.Printer>("Items/printers.json");
    }
}