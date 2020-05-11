using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using System.IO;
using System.Collections.Generic;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
[Group("admin")]
public class RemovePrinter : ModuleBase<SocketCommandContext>
{
    [Command("removeprinter", RunMode = RunMode.Async)]
    [Summary("Removes the specified printer")]
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
                    file.RemoveAt(count - 1);
                    File.WriteAllLines("Items/printers.json", file.ToArray());

                    await Context.Channel.SendMessageAsync($"Removed {printerName} printer.");
                    await Utilities.StatusMessage("removeprinter", Context);
                    return;
                }
            }

            await Context.Channel.SendMessageAsync($"Printer not found.");
            await Utilities.StatusMessage("removeprinter", Context);
        }
    }
}