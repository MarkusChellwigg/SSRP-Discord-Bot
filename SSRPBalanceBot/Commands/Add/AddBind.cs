using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class AddBind : ModuleBase<SocketCommandContext>
{
    [Command("addbind", RunMode = RunMode.Async)]
    [Summary("Adds a new bind with the specified data")]
    public async Task AddItemAsync(string itemname, string aliases)
    {
        if (itemname == "" | aliases == "") { return; }

        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.AddItem) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        else
        {
            string[] aliasList = aliases.Split(',');
            SSRPItems.Bind newBind = new SSRPItems.Bind { bind = $"{itemname}", aliases = aliasList };
            SSRPItems.WriteToJsonFile<SSRPItems.Bind>("Items/binds.json", newBind, true);
            SSRPItems.bindList.Add(newBind);

            await Context.Channel.SendMessageAsync($"New Item Has Been Added. Name: {itemname} | Bind: zarp_equipitem {itemname}");
            await Utilities.StatusMessage("bind", Context);
        }
    }
}