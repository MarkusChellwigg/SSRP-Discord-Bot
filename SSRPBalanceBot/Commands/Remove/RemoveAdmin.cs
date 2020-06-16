using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using Discord.WebSocket;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
[Group("admin")]
public class RemoveAdmin : ModuleBase<SocketCommandContext>
{
    [Command("removeadmin", RunMode = RunMode.Async)]
    [Summary("Removes mentioned user from admins")]
    public async Task RemoveAdminAsync(string mentioned)
    {
        bool success;
        string id = mentioned.Replace("<@!", "").Replace(">", "");
        ulong parsedId;
        ulong.TryParse(id, out parsedId);

        if (!PermissionManager.CheckAdmin(Context.Message.Author.Id).Result) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionManager.GetPerms(parsedId)) { await Context.Channel.SendMessageAsync("Permission level lower than or equal to user being removed"); return; }


        if (!PermissionManager.CheckAdmin(parsedId).Result) { success = false; }
        else { success = await PermissionManager.RemoveAdmin(id); }

        if (success) { await Context.Channel.SendMessageAsync($"Admin removed with id `{id}`"); }
        else { await Context.Channel.SendMessageAsync($"Admin doesn't exist"); }

        await Utilities.StatusMessage("removeadmin", Context);
    }
}