using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using System.IO;
using System.Collections.Generic;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class RemoveBind : ModuleBase<SocketCommandContext>
{
    [Command("removebind", RunMode = RunMode.Async)]
    [Summary("Removes the specified bind")]
    public async Task RemoveItemAsync(string itemname)
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.RemoveItems) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        if (itemname == "") { return; }
        int count = 0;

        string[] currBinds = File.ReadAllLines("Items/binds.json");

        foreach(string bind in currBinds)
        {
            count++;
            SSRPItems.Bind b = SSRPItems.ReadFromJsonFile<SSRPItems.Bind>(bind);

            if (b.bind == itemname)
            {
                var file = new List<string>(System.IO.File.ReadAllLines("Items/binds.json"));
                file.RemoveAt(count - 1);
                File.WriteAllLines("Items/binds.json", file.ToArray());

                await Context.Channel.SendMessageAsync($"Removed {itemname}.");
                await Utilities.StatusMessage("removebind", Context);
                return;
            }
        }

        await Context.Channel.SendMessageAsync($"Could not find {itemname}. Make sure it is typed correctly.");
        await Utilities.StatusMessage("removebind", Context);
    }
}