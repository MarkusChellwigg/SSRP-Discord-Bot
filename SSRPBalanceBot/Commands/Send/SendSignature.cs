using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using SSRPBalanceBot.LinkedSignatures;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Signature : ModuleBase<SocketCommandContext>
{
    [Command("signature", RunMode = RunMode.Async)]
    [Summary("Returns the signature of the specified SteamID. Also adds the user to the database")]
    public async Task SendSignature([Remainder]string id = null)
    {
        string sig;
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendSignature) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        if(id == null)
        {
            string steamID64 = await LinkedSignatures.GetSteam(Context.Message.Author.Id.ToString());
            if (steamID64 == null) 
            { 
                await Context.Channel.SendMessageAsync($"Your Discord isn't linked to a Steam profile. To link your account, run !link [SteamID]");
                await Utilities.StatusMessage("signature", Context);
                return;
            }
            else
            {
                sig = Utilities.GetSignature(steamID64);

                await Context.Channel.SendMessageAsync($"Here's the signature you were looking for: \n{sig}");
                await Utilities.StatusMessage("signature", Context);
                return;
            }
        }
        else
        {
            sig = Utilities.GetSignature(id);
            await Context.Channel.SendMessageAsync($"Here's the signature you were looking for: \n{sig}");
            await Utilities.StatusMessage("signature", Context);
        }
    }
}