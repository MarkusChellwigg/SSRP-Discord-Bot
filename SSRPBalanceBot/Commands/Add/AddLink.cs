using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using SSRPBalanceBot.LinkedSignatures;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class AddLink : ModuleBase<SocketCommandContext>
{
    [Command("link", RunMode = RunMode.Async)]
    [Summary("Links a users Discord and Steam")]
    public async Task LinkAccount()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.User) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        if (LinkedSignatures.GetSteam(Context.Message.Author.Id.ToString()) != null) { await Context.Channel.SendMessageAsync("Account already linked"); return; }

        var u = Context.Message.Author;
        await Discord.UserExtensions.SendMessageAsync(u, $"Click here to link your account: https://nickgor.com/SteamAuth.php?DiscordID={u.Id}");
        await ReplyAsync("Check your DMs");
    }
}