using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class CommandModule : ModuleBase<SocketCommandContext>
{
    [Command("help", RunMode = RunMode.Async)]
    [Summary("Displays the help message")]
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

    [Command("total", RunMode = RunMode.Async)]
    [Summary("Returns the total balance of all users in the database")]
    public async Task SendTotal()
    {
        await Context.Channel.SendMessageAsync($"Total size of SSRP Economy as of {DateTime.Now.Date.ToString("dd/MM/yy")}: `${Convert.ToInt64(Utilities.GetTotal(Utilities.GetStatistics())).ToString("#,##0")}`");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [total] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }

    [Command("average", RunMode = RunMode.Async)]
    [Summary("Retuns an average balance of all users in the database")]
    public async Task SendAverage()
    {
        await Context.Channel.SendMessageAsync($"Average balance shared between {Utilities.GetUsers(Utilities.GetStatistics()).ToString("#,##0")} players: `${Utilities.GetAverage(Utilities.GetStatistics()).ToString("#,##0")}`");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [average] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }

    [Command("statistics", RunMode = RunMode.Async)]
    [Summary("Returns all statistics")]
    public async Task SendStats()
    {
        await Context.Channel.SendMessageAsync($"Total size of SSRP Economy as of {DateTime.Now.Date.ToString("dd/MM/yy")}: `${Convert.ToInt64(Utilities.GetTotal(Utilities.GetStatistics())).ToString("#,##0")}`");
        await Context.Channel.SendMessageAsync($"Average balance shared between {Utilities.GetUsers(Utilities.GetStatistics()).ToString("#,##0")} players: `${Utilities.GetAverage(Utilities.GetStatistics()).ToString("#,##0")}`");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [statistics] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }

    [Command("signature", RunMode = RunMode.Async)]
    [Summary("Returns the signature of the specified SteamID. Also adds the user to the database")]
    public async Task SendSignature(string id)
    {
        string sig = Utilities.GetSignature(id);
        await Context.Channel.SendMessageAsync($"Here's the signature you were looking for: \n{sig}");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [signature] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }

    [Command("bind", RunMode = RunMode.Async)]
    [Summary("Returns the bind for the specified item.")]
    public async Task SendBind([Remainder]string item)
    {
        string bind = await SSRPItems.GetBind(item.ToLower());
        await Context.Channel.SendMessageAsync($"The bind you are looking for is: `{bind}`");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [bind] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }

    [Command("printer", RunMode = RunMode.Async)]
    [Summary("Returns the bind for the specified item.")]
    public async Task SendPrinter(string item, int boost = 1)
    {
        SSRPItems.Printer p = await SSRPItems.GetPrinter(item);

        if (p == null) { await Context.Channel.SendMessageAsync("Printer not found. Please enclose the printer name in quotes: `\"name\"`"); }
        else
        {
            await Context.Channel.SendMessageAsync($"The `{p.printerName} Printer` prints `${(p.perSecond * boost).ToString("#,##0")}` every second, `${((p.perSecond * 60) * boost).ToString("#,##0")}` per minute, or `${(((p.perSecond * 60) * 60) * boost).ToString("#,##0")}` per hour with a boost of `x{boost.ToString("#,##0")}`.");
            await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [printer] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
        }

    }

    [Command("database", RunMode = RunMode.Async)]
    [Summary("Link to the SSRP Database")]
    public async Task SendDatabaseURL()
    {
        await Context.Channel.SendMessageAsync($"https://nickgor.com/SSRPBalances.php");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [database] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }

    [Command("site", RunMode = RunMode.Async)]
    [Summary("Link to the site")]
    public async Task SendSiteURL()
    {
        await Context.Channel.SendMessageAsync($"https://nickgor.com/");
        await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [site] | Called by: {Context.Message.Author} | Server: {Context.Guild.Name}");
    }
}
