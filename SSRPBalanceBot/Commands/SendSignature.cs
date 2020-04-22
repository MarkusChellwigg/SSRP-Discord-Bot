using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Signature : ModuleBase<SocketCommandContext>
{
    [Command("signature", RunMode = RunMode.Async)]
    [Summary("Returns the signature of the specified SteamID. Also adds the user to the database")]
    public async Task SendSignature(string id)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendSignature) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        string sig = Utilities.GetSignature(id);
        await Context.Channel.SendMessageAsync($"Here's the signature you were looking for: \n{sig}");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [signature] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }
}