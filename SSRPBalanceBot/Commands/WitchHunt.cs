using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class WitchHunt : ModuleBase<SocketCommandContext>
{
    [Command("witchhunt", RunMode = RunMode.Async)]
    [Summary("Witch Hunt")]
    public async Task SetWitchHunt(string user)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.WitchHunt) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        if (user == "Bunny#9220") { Program.witchhunt = Context.Message.Author.ToString().ToLower(); }
        else
        {
            Program.witchhunt = user.ToString().ToLower();
            await Context.Channel.SendMessageAsync($"Witch Hunting `{user}`");
            await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [witchhunt] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
        }
    }
}