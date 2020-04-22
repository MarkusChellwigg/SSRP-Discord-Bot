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
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendBind) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        string bind = await SSRPItems.GetBind(item.ToLower());
        await Context.Channel.SendMessageAsync($"The bind you are looking for is: `bind {key} {bind}`");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [bind] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }
}