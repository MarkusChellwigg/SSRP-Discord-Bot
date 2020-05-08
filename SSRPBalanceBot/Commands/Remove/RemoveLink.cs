using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using SSRPBalanceBot.LinkedSignatures;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class RemoveLink : ModuleBase<SocketCommandContext>
{
    [Command("unlink", RunMode = RunMode.Async)]
    [Summary("Unlinks a users Discord and Steam account")]
    public async Task LinkAccount()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.Link) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        if (await LinkedSignatures.GetSteam(Context.Message.Author.Id.ToString()) == null) { await Context.Channel.SendMessageAsync("Your Discord isn't linked to a Steam profile. To link your account, run !link [SteamID]"); return; }

        await LinkedSignatures.RemoveLink(Context.Message.Author.Id.ToString());


        await Context.Channel.SendMessageAsync($"You have succesfully unlinked your Discord account and your Steam account.");
        await Utilities.StatusMessage("unlink", Context);
    }
}