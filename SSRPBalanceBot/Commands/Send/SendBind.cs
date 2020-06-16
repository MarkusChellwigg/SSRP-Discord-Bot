using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Bind : ModuleBase<SocketCommandContext>
{
    [Command("bind", RunMode = RunMode.Async)]
    [Summary("Returns the bind for the specified item.")]
    public async Task SendBind(char key,[Remainder]string item)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.User) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        string bind = await SSRPItems.GetBind(item.ToLower());

        if (bind == null) { await Context.Channel.SendMessageAsync("Specified item not found. You can request for it to be added by contacting an Admin."); return; }

        await Context.Channel.SendMessageAsync($"The bind you are looking for is: `bind {key} \"zarp_equipitem {bind}\"`");
        await Utilities.StatusMessage("bind", Context);
    }
}