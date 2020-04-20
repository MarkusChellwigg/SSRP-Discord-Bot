using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class AdminHelp : ModuleBase<SocketCommandContext>
{
    [Command("adminhelp", RunMode = RunMode.Async)]
    public async Task SendAdminHelpMessage()
    {
        await Context.Channel.SendMessageAsync(
            $"Hello {Context.Message.Author.Username}.\n" +
            $"The current available commands are as follows:\n" +
            $"`!additem [Item Name] [Aliases (in quotes, separated by commas, no spaces between commas)]- Adds item with specified data`\n" +
            $"`!addprinter [Printer Name] [Per Second (double)] [Aliases (in quotes, separated by commas, no spaces between commas)] - Adds printer with specified data`\n" +
            $"`!removeitem [Item Name]- Removes specified item`\n" +
            $"`!removeprinter [Printer Name] - Removes specified printer`\n" +
            $"`!reloaditems - Reloads items and printers.`\n");
    }
}