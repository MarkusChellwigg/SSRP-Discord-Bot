using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
[Group("admin")]
public class ReloadPermissions : ModuleBase<SocketCommandContext>
{
    [Command("reloadpermissions", RunMode = RunMode.Async)]
    [Summary("Reloads all user permissions")]
    public async Task ReloadPermissionsAsync()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.ReloadPermissions) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        await PermissionManager.ReloadPermissions();
        await Context.Channel.SendMessageAsync("Permissions have been reloaded.");
        await Utilities.StatusMessage("reloadpermissions", Context);
    }
}