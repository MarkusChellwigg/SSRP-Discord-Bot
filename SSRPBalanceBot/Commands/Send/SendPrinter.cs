using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Printer : ModuleBase<SocketCommandContext>
{
    [Command("printer", RunMode = RunMode.Async)]
    [Summary("Returns the bind for the specified item.")]
    public async Task SendPrinter(string item, int boost = 1)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendPrinter) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        SSRPItems.Printer p = await SSRPItems.GetPrinter(item);

        if (p == null) { await Context.Channel.SendMessageAsync("Printer not found. Please enclose the printer name in quotes: `\"name\"`"); await Utilities.StatusMessage("printer", Context); }
        else
        {
            await Context.Channel.SendMessageAsync($"The `{p.printerName} Printer` prints `${(p.perSecond * boost).ToString("#,##0")}` every second, `${((p.perSecond * 60) * boost).ToString("#,##0")}` per minute, or `${(((p.perSecond * 60) * 60) * boost).ToString("#,##0")}` per hour with a boost of `x{boost.ToString("#,##0")}`.");
            await Utilities.StatusMessage("printer", Context);
        }

    }
}