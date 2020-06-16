using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using SSRPBalanceBot.Leaderboards;
using SSRPBalanceBot.LinkedSignatures;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
[Group("admin")]
public class ReloadItems : ModuleBase<SocketCommandContext>
{
    [Command("reloadall", RunMode = RunMode.Async)]
    [Summary("Reloads all item lists. Useful after adding new items.")]
    public async Task ReloadItemsAsync()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.Admin) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        //SSRP Item lists
        SSRPItems.bindList = SSRPItems.FillList<SSRPItems.Bind>("Items/binds.json");
        SSRPItems.printerList = SSRPItems.FillList<SSRPItems.Printer>("Items/printers.json");
        SSRPItems.itemList = SSRPItems.FillList<SSRPItems.Item>("Items/items.json");
        SSRPItems.suitList = SSRPItems.FillList<SSRPItems.Suit>("Items/suits.json");
        SSRPItems.caseList = SSRPItems.FillList<SSRPItems.Case>("Items/cases.json");

        //Leaderboard lists
        LeaderboardUtils.boards = SSRPItems.FillList<LeaderboardUtils.Boards>("Leaderboards/boards.json");
        LeaderboardUtils.categories = SSRPItems.FillList<LeaderboardUtils.Categories>("Leaderboards/categories.json");

        //Link list
        LinkedSignatures.linkedSigs = SSRPItems.FillList<LinkedSignatures.LinkSignature>("LinkedSignatures/linkedSignatures.json");

        await Context.Channel.SendMessageAsync("All item lists have been reloaded. Any changes to any item files are now in effect.");
        await Utilities.StatusMessage("reloaditems", Context);
    }
}