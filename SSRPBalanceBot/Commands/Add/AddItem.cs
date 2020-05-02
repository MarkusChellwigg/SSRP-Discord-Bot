using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class AddItem : ModuleBase<SocketCommandContext>
{
    [Command("additem", RunMode = RunMode.Async)]
    [Summary("Adds a new item with the specified data")]
    public async Task AddItemAsync(string itemname, string category, string info, string aliases)
    {
        if (itemname == "" |category == "" | info == "" | aliases == "") { return; }

        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.AddItem) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        else
        {
            string[] aliasList = aliases.Split(',');
            SSRPItems.Item newItem = new SSRPItems.Item { itemName = $"{itemname}", category = category, info = info, aliases = aliasList };
            SSRPItems.WriteToJsonFile<SSRPItems.Item>("Items/items.json", newItem, true);
            SSRPItems.itemList.Add(newItem);

            await Context.Channel.SendMessageAsync($"New Item Has Been Added. Name: {itemname} | Info: {info}");
            await Utilities.StatusMessage("additem", Context);
        }
    }
}