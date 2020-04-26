using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class AdminHelp : ModuleBase<SocketCommandContext>
{
    [Command("adminhelp", RunMode = RunMode.Async)]
    public async Task SendAdminHelpMessage()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.AddAdmin) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        await Context.Channel.SendMessageAsync(
            $"Hello {Context.Message.Author.Username}.\n" +
            $"The current available commands are as follows:\n" +
            $"`!additem [Item Name] [Aliases (in quotes, separated by commas, no spaces between commas)]`\n" +
            $"`!addprinter [Printer Name] [Per Second (double)] [Aliases (in quotes, separated by commas, no spaces between commas)]`\n" +
            $"`!removeitem [Item Name]- Removes specified item`\n" +
            $"`!removeprinter [Printer Name] - Removes specified printer`\n" +
            $"`!reloaditems - Reloads items and printers.`\n" +
            $"`!reloadpermissions - Reloads all users permissions`");
        await Utilities.StatusMessage("adminhelp", Context);
    }
}