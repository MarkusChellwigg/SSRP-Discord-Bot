using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Help : ModuleBase<SocketCommandContext>
{
    [Command("help", RunMode = RunMode.Async)]
    public async Task SendHelpMessage()
    {
        await Context.Channel.SendMessageAsync(
            $"Hello {Context.Message.Author.Username}.\n" +
            $"The current available commands are as follows:\n" +
            $"`!total - Returns the total balance of all users in the database`\n" +
            $"`!average - Retuns an average balance of all users in the database`\n" +
            $"`!statistics - Returns all statistics`\n" +
            $"`!signature [SteamID] - Returns the signature of the specified SteamID. Also adds the user to the database`\n" +
            $"`!bind [Item] - Returns the bind for the specified item`\n" +
            $"`!printer \"[Printer Name]\" [Boost - Default 1] - Returns info about the specified printer`\n" +
            $"`!database - Link to SSRP Database`\n" +
            $"`!site - Link to site`");
    }
}