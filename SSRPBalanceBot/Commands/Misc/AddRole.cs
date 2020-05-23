using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using System.Linq;
using Discord.WebSocket;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
[Group("admin")]
public class AddRole : ModuleBase<SocketCommandContext>
{
    [Command("addrole", RunMode = RunMode.Async)]
    [Summary("Adds user to role")]
    public async Task SendRoll(string id)
    {
        var role = Context.Guild.Roles.FirstOrDefault(x => x.Id.ToString() == id);
        var member = Context.User as SocketGuildUser;

        await member.AddRoleAsync(role);
        await Utilities.StatusMessage("addrole", Context);
    }
}