using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Printer : ModuleBase<SocketCommandContext>
{
    [Command("printer", RunMode = RunMode.Async)]
    [Summary("Returns the bind for the specified item.")]
    public async Task SendPrinter(string item, int boost = 1)
    {
        SSRPItems.Printer p = await SSRPItems.GetPrinter(item);

        if (p == null) { await Context.Channel.SendMessageAsync("Printer not found. Please enclose the printer name in quotes: `\"name\"`"); }
        else
        {
            await Context.Channel.SendMessageAsync($"The `{p.printerName} Printer` prints `${(p.perSecond * boost).ToString("#,##0")}` every second, `${((p.perSecond * 60) * boost).ToString("#,##0")}` per minute, or `${(((p.perSecond * 60) * 60) * boost).ToString("#,##0")}` per hour with a boost of `x{boost.ToString("#,##0")}`.");
            await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [printer] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
        }

    }
}