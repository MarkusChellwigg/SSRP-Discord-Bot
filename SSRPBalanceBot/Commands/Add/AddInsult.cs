using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class AddInsult : ModuleBase<SocketCommandContext>
{
    [Command("addinsult", RunMode = RunMode.Async)]
    [Summary("Adds a new insult")]
    public async Task AddInsultAsync([Remainder]string insult)
    { 
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.AddInsult) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        else
        {
            Utilities.Insult newInsult = new Utilities.Insult { insult = insult};
            SSRPItems.WriteToJsonFile("Items/insults.json", newInsult, true);
            Utilities.insultsList.Add(newInsult);

            await Context.Channel.SendMessageAsync($"New Insult Has Been Added. Insult: {insult}");
            await Utilities.StatusMessage("addinsult", Context);
        }
    }
}