using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using static SSRPBalanceBot.LinkedSignatures.LinkedSignatures;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
[Group("admin")]
public class ReloadLinks : ModuleBase<SocketCommandContext>
{
    [Command("reloadlinks", RunMode = RunMode.Async)]
    [Summary("Reloads linked accounts.")]
    public async Task ReloadLinksAsync()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.Admin) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        linkedSigs = SSRPItems.FillList<LinkSignature>("LinkedSignatures/linkedSignatures.json");
        await Context.Channel.SendMessageAsync("Linked accounts have been reloaded.");
        await Utilities.StatusMessage("reloadlinks", Context);
    }
}