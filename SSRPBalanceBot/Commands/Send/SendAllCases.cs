using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using System.Text;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class SendCases : ModuleBase<SocketCommandContext>
{
    [Command("cases", RunMode = RunMode.Async)]
    [Summary("Returns the bind for the specified item.")]
    public async Task SendCasesAsync()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendBind) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        StringBuilder sb = new StringBuilder();

        foreach(SSRPItems.Case c in SSRPItems.caseList)
        {
            sb.Append($"Case: {c.caseName}\n");
        }

        await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention}\n```--- All Current Cases ---\n{sb.ToString()}```");
        await Utilities.StatusMessage("bind", Context);
    }
}