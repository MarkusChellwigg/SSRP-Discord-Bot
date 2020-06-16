using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using SSRPBalanceBot.LinkedSignatures;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Steam : ModuleBase<SocketCommandContext>
{
    [Command("steam", RunMode = RunMode.Async)]
    [Summary("Returns the steam profile of a user.")]
    public async Task SendSteam(string mention = null)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.User) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        if(mention == null)
        {
            string steamID64 = LinkedSignatures.GetSteam(Context.Message.Author.Id.ToString());

            //If account not linked
            if (steamID64 == null) { await ReplyAsync("Your Discord isn't linked to a Steam profile. Run !link."); return; }
            //If account is linked
            await ReplyAsync($"Here's the profile you're looking for\nhttps://steamcommunity.com/profiles/{steamID64}");
        }
        else
        {
            string steamID64 = LinkedSignatures.GetSteam(Utilities.GetDID(mention));

            //If account not linked
            if (steamID64 == null) { await ReplyAsync("Their Discord isn't linked to a Steam profile. You can ask them to run !link."); return; }
            //If account is linked
            await ReplyAsync($"Here's the profile you're looking for\nhttps://steamcommunity.com/profiles/{steamID64}");
        }

        await Utilities.StatusMessage("steam", Context);
    }
}