using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using System.IO;
using System.Collections.Generic;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class RemoveItem : ModuleBase<SocketCommandContext>
{
    /*
    [Command("additem", RunMode = RunMode.Async)]
    [Summary("Retuns an average balance of all users in the database")]
    public async Task RemoveItemAsync(string itemname)
    {
        if (itemname == "") { return; }

        if (Context.Message.Author.ToString() != "Bunny#9220") { await Context.Channel.SendMessageAsync("No permission"); }
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
    */

    [Command("removeprinter", RunMode = RunMode.Async)]
    [Summary("Retuns an average balance of all users in the database")]
    public async Task RemovePrinterAsync(string printerName)
    {
        int count = 0;
        if (printerName == "") { return; }

        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.RemoveItems) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        else
        {
            string[] currPrinters = File.ReadAllLines("Items/printers.json");

            foreach (var printer in currPrinters)
            {
                count++;
                SSRPItems.Printer p = SSRPItems.ReadFromJsonFile<SSRPItems.Printer>(printer);

                if (p.printerName == printerName) 
                {
                    var file = new List<string>(System.IO.File.ReadAllLines("Items/printers.json"));
                    Console.WriteLine(file[count - 1]);
                    file.RemoveAt(count - 1);
                    File.WriteAllLines("Items/printers.json", file.ToArray());
                }
            }

            await Context.Channel.SendMessageAsync($"Removed {printerName} printer.");
            await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [average] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
        }
    }
}