using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Bind : ModuleBase<SocketCommandContext>
{
    [Command("bind", RunMode = RunMode.Async)]
    [Summary("Returns the bind for the specified item.")]
    public async Task SendBind([Remainder]string item)
    {
        string bind = await SSRPItems.GetBind(item.ToLower());
        await Context.Channel.SendMessageAsync($"The bind you are looking for is: `{bind}`");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [bind] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }
}