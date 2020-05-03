using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Help : ModuleBase<SocketCommandContext>
{
    [Command("help", RunMode = RunMode.Async)]
    public async Task SendHelpMessage()
    {
        Program p = new Program();

        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendHelp) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        await Context.Channel.SendMessageAsync(
            $"Hello {Context.Message.Author.Mention}.\n" +
            $"```The current available commands are as follows:\n" +
            $"!total - Returns the total balance of all users in the database\n" +
            $"!average - Retuns an average balance of all users in the database\n" +
            $"!statistics - Returns all statistics\n" +
            $"!signature [SteamID] - Returns the signature of the specified SteamID. Also adds the user to the database\n" +
            $"!bind [Key] [Item] - Returns the bind for the specified item\n" +
            $"!printer \"[Printer Name]\" [Boost - Default 1] [Time - Default 1] - Returns info about the specified printer\n" +
            $"!case [Case Name] - Returns info about the specified case\n" +
            $"!item [Item Name] - Returns information about a specific item\n" +
            $"!suit [Suit Name] - Returns information about a specific suit\n" +
            $"!roll [Max] - Rolls a random number between 0 and the specified value\n" +
            $"!coinflip [Mention Opponent] - Randomly picks a winner\n"+
            $"!database - Link to SSRP Database\n" +
            $"!site - Link to site```");

        await Utilities.StatusMessage("help", Context);
    }
}