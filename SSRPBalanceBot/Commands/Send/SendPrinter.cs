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
    public async Task SendPrinter(string item, int boost = 1, int time = 1)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendPrinter) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        SSRPItems.Printer p = await SSRPItems.GetPrinter(item);

        if (p == null) { await Context.Channel.SendMessageAsync("Printer not found. Please enclose the printer name in quotes: `\"name\"`"); await Utilities.StatusMessage("printer", Context); }
        else
        {


            //If the printer is a Uranium unit
            if(p.printerName == "Uranium")
            {
                //If 1, don't print the plural
                if (time == 1)
                {
                    await Context.Channel.SendMessageAsync($"The `{p.printerName} Unit` prints `{(p.perSecond * boost).ToString("#,##0.00")} Uranium` every second, `{((p.perSecond * 60) * boost).ToString("#,##0.00")}` every minute, or `{(((p.perSecond * 60) * 60) * boost).ToString("#,##0")}` every hour with a boost of `x{boost.ToString("#,##0")}`.");
                    await Utilities.StatusMessage("printer", Context);
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"The `{p.printerName} Unit` prints `{((p.perSecond * boost) * time).ToString("#,##0.00")} Uranium` every {time} seconds, `{(((p.perSecond * 60) * boost) * time).ToString("#,##0.00")}` every {time} minutes, or `{((((p.perSecond * 60) * 60) * boost) * time).ToString("#,##0.00")}` every {time} hours with a boost of `x{boost.ToString("#,##0")}`.");
                    await Utilities.StatusMessage("printer", Context);
                }
            }
            //If the printer is one of the O' Matics
            else if(p.printerName == "Loot" || p.printerName == "Gem")
            {
                //If 1, don't print the plural
                if (time == 1)
                {
                    await Context.Channel.SendMessageAsync($"The `{p.printerName} O' Matic` prints `{(p.perSecond * boost).ToString("#,##0")}` every second, `{((p.perSecond * 60) * boost).ToString("#,##0")}` every minute, or `{(((p.perSecond * 60) * 60) * boost).ToString("#,##0")}` every hour with a boost of `x{boost.ToString("#,##0")}`.");
                    await Utilities.StatusMessage("printer", Context);
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"The `{p.printerName} O' Matic` prints `{((p.perSecond * boost) * time).ToString("#,##0")}` every {time} seconds, `{(((p.perSecond * 60) * boost) * time).ToString("#,##0")}` every {time} minutes, or `{((((p.perSecond * 60) * 60) * boost) * time).ToString("#,##0")}` every {time} hours with a boost of `x{boost.ToString("#,##0")}`.");
                    await Utilities.StatusMessage("printer", Context);
                }
            }
            //If normal printer
            else
            {
                //If 1, don't print the plural
                if(time == 1)
                {
                    await Context.Channel.SendMessageAsync($"The `{p.printerName} Printer` prints `${((p.perSecond * boost) * time).ToString("#,##0")}` every second, `${(((p.perSecond * 60) * boost) * time).ToString("#,##0")}` every minute, or `${(((p.perSecond * 60) * 60) * boost).ToString("#,##0")}` every hour with a boost of `x{boost.ToString("#,##0")}`.");
                    await Utilities.StatusMessage("printer", Context);
                }
                else
                {
                    await Context.Channel.SendMessageAsync($"The `{p.printerName} Printer` prints `${((p.perSecond * boost) * time).ToString("#,##0")}` every {time} seconds, `${(((p.perSecond * 60) * boost) * time).ToString("#,##0")}` every {time} minutes, or `${((((p.perSecond * 60) * 60) * boost) * time).ToString("#,##0")}` every {time} hours with a boost of `x{boost.ToString("#,##0")}`.");
                    await Utilities.StatusMessage("printer", Context);
                }
            }
        }

    }
}