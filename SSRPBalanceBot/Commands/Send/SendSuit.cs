using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Suit : ModuleBase<SocketCommandContext>
{
    [Command("suit", RunMode = RunMode.Async)]
    [Summary("Returns info about the specified suit.")]
    public async Task SendBind([Remainder]string item)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendSuit) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        SSRPItems.Suit s = await SSRPItems.GetSuit(item);

        if (s == null) { await Context.Channel.SendMessageAsync("Specified suit not found. You can request for it to be added by contacting an Admin."); return; }

        await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention}\n```--- The {s.suitName} ---\nHP: {s.hp}\nArmor: {s.armor}\nSpeed: {s.speed}\nAbility: {s.ability}```");
        await Utilities.StatusMessage("suit", Context);
    }
}