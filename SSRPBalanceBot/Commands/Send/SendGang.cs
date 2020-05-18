using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using SSRPBalanceBot.LinkedSignatures;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Gang : ModuleBase<SocketCommandContext>
{
    [Command("gang", RunMode = RunMode.Async)]
    [Summary("Returns gang of a user.")]
    public async Task SendGang(string mention = null)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendBind) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        if(mention == null)
        {
            string steamID64 = LinkedSignatures.GetSteam(Context.Message.Author.Id.ToString());
            await ReplyAsync($"{Context.Message.Author.Mention} is in {await Utilities.GetGang(steamID64)}");
        }
        else
        {
            string dID = mention.Replace("<@!", "").Replace(">", "");
            string steamID64 = LinkedSignatures.GetSteam(dID);
            await ReplyAsync($"<@!{dID}> is in gang: {await Utilities.GetGang(steamID64)}");
        }
        await Utilities.StatusMessage("bind", Context);
    }
}