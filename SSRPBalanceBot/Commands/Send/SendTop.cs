using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using System.Collections.Generic;
using System.Linq;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Top : ModuleBase<SocketCommandContext>
{
    [Command("top", RunMode = RunMode.Async)]
    [Summary("Returns the highest stat for the specified stat and type")]
    public async Task SendBind(string type, string stat = null)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendBind) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        if(type.ToLower() == "printer")
        {
            List<SSRPItems.Printer> sorted = SSRPItems.printerList.OrderByDescending(o => o.perSecond).ToList();
            await ReplyAsync($"The current fastest printer is the `{sorted[0].printerName} printer` which prints `${sorted[0].perSecond.ToString("#,##0.00")}` per second, `${(sorted[0].perSecond * 60).ToString("#,##0")}` per minute or `${((sorted[0].perSecond * 60) * 60).ToString("#,##0")}` per hour");
        }
        else if(type.ToLower() == "suit")
        {
            if (stat == "hp" || stat == "health")
            {
                List<SSRPItems.Suit> sorted = SSRPItems.suitList.OrderByDescending(o => o.hp).ToList();
                await ReplyAsync($"The suit with the most hp is the `{sorted[0].suitName}` with `{sorted[0].hp}HP`");
            }
            else if (stat == "armor" || stat == "armour")
            {
                List<SSRPItems.Suit> sorted = SSRPItems.suitList.OrderByDescending(o => o.armor).ToList();
                await ReplyAsync($"The suit with the most armor is the `{sorted[0].suitName}` with `{sorted[0].armor} armor`");
            }
            else if (stat == "speed")
            {
                List<SSRPItems.Suit> sorted = SSRPItems.suitList.OrderByDescending(o => o.speed).ToList();
                await ReplyAsync($"The fastest suit is the `{sorted[0].suitName}` which moves at `{sorted[0].speed}` units");
            }
        }
    }
}