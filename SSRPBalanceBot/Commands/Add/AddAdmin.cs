using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using Discord.WebSocket;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class AddAdmin : ModuleBase<SocketCommandContext>
{
    [Command("addadmin", RunMode = RunMode.Async)]
    [Summary("Adds mentioned user to admins")]
    public async Task AddAdminAsync(string mentioned, int permLevel)
    {
        if (!PermissionManager.CheckAdmin(Context.Message.Author.Id)) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.AddAdmin) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        string id = mentioned.Replace("<@!", "").Replace(">", "");
        ulong idParsed;
        ulong.TryParse(id, out idParsed);
        SocketGuildUser u = Context.Guild.GetUser(idParsed);
        bool success = await PermissionManager.AddAdmin(idParsed, u.Username, permLevel);

        if (success) { await Context.Channel.SendMessageAsync($"Admin added with id `{id}`"); }
        else { await Context.Channel.SendMessageAsync($"Admin either already exists or is invalid"); }

        await Utilities.StatusMessage("addadmin", Context);
    }
}