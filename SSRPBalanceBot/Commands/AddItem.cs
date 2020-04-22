using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class AddItem : ModuleBase<SocketCommandContext>
{
    [Command("additem", RunMode = RunMode.Async)]
    [Summary("Retuns an average balance of all users in the database")]
    public async Task AddItemAsync(string itemname, string aliases)
    {
        if (itemname == "" | aliases == "") { return; }

        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.AddItem) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        else
        {
            string[] aliasList = aliases.Split(',');
            SSRPItems.Item newItem = new SSRPItems.Item { bind = $"zarp_equipitem {itemname}", aliases = aliasList };
            SSRPItems.WriteToJsonFile<SSRPItems.Item>("Items/items.json", newItem, true);
            SSRPItems.itemList.Add(newItem);

            await Context.Channel.SendMessageAsync($"New Item Has Been Added. Name: {itemname} | Bind: zarp_equipitem {itemname}");
            await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [average] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
        }
    }

    [Command("addprinter", RunMode = RunMode.Async)]
    [Summary("Retuns an average balance of all users in the database")]
    public async Task AddPrinterAsync(string printerName, double perSecond,string aliases)
    {
        if (printerName == "" | aliases == "") { return; }

        if (Context.Message.Author.ToString() != "Bunny#9220") { await Context.Channel.SendMessageAsync("No permission"); }
        else
        {
            string[] aliasList = aliases.Split(',');
            SSRPItems.Printer newPrinter = new SSRPItems.Printer { printerName = printerName, perSecond = perSecond, aliases = aliasList};
            SSRPItems.WriteToJsonFile<SSRPItems.Printer>("Items/printers.json", newPrinter, true);
            SSRPItems.printerList.Add(newPrinter);

            await Context.Channel.SendMessageAsync($"New Printer Has Been Added. Name: {printerName} | Per Second: {perSecond.ToString("#,##0")}");
            await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [average] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
        }
    }
}