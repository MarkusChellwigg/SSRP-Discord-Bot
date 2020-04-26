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
    public async Task LinkAccount(string steamID)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.Link) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        string steamID64 = SteamIDUtils.RetrieveID(steamID);

        if (await LinkedSignatures.CheckExists(steamID64, Context)) { await Context.Channel.SendMessageAsync("The ID specified is already linked to an account. If this is a mistake, please contact an admin."); return; }

        LinkedSignatures.LinkSignature ls = new LinkedSignatures.LinkSignature { SteamID64 = steamID64, DiscordID = Context.Message.Author.Id.ToString() };
        SSRPItems.WriteToJsonFile<LinkedSignatures.LinkSignature>("LinkedSignatures/linkedSignatures.json", ls, true);
        LinkedSignatures.linkedSigs.Add(ls);

        await Context.Channel.SendMessageAsync($"You have succesfully linked your Discord account to your Steam account.");
        await Utilities.StatusMessage("link", Context);
    }
}