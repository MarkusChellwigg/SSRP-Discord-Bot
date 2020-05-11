using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
[Group("admin")]
public class AddPrinter : ModuleBase<SocketCommandContext>
{
    [Command("addprinter", RunMode = RunMode.Async)]
    [Summary("Adds a new printer with the specified data")]
    public async Task AddPrinterAsync(string printerName, double perSecond, string aliases)
    {
        if (printerName == "" | aliases == "") { return; }

        if (Context.Message.Author.ToString() != "Bunny#9220") { await Context.Channel.SendMessageAsync("No permission"); }
        else
        {
            string[] aliasList = aliases.Split(',');
            SSRPItems.Printer newPrinter = new SSRPItems.Printer { printerName = printerName, perSecond = perSecond, aliases = aliasList };
            SSRPItems.WriteToJsonFile<SSRPItems.Printer>("Items/printers.json", newPrinter, true);
            SSRPItems.printerList.Add(newPrinter);

            await Context.Channel.SendMessageAsync($"New Printer Has Been Added. Name: {printerName} | Per Second: {perSecond.ToString("#,##0")}");
            await Utilities.StatusMessage("addprinter", Context);
        }
    }
}