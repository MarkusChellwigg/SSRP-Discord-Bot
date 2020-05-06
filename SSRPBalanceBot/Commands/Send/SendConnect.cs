using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using Discord;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Connect : ModuleBase<SocketCommandContext>
{
    [Command("connect", RunMode = RunMode.Async)]
    [Summary("Sends links to the servers")]
    public async Task SendConnect()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendAverage) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        await Context.Channel.SendMessageAsync("`Server 1` - steam://connect/darkrp.zarpgaming.com:27015\n`Server 3` - steam://connect/usa.zarpgaming.com:27015");
        await Utilities.StatusMessage("connect", Context);
    }
}