using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class SendCommands : ModuleBase<SocketCommandContext>
{
    [Command("commands", RunMode = RunMode.Async)]
    [Summary("Returns some useful SSRP commands")]
    public async Task SendHelpMessage()
    {
        Program p = new Program();

        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendCommands) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        await Context.Channel.SendMessageAsync(
            $"Hello {Context.Message.Author.Mention}.\n" +
            $"```Useful ZARP Console Commands:\n" +
            $"• zarp_equipitem - Equips the specified item\n" +
            $"• zarp_dropitem - Drops the specified item\n" +
            $"• M9KGasEffect 0 - Removes the M9K Gas Effect\n" +
            $"• hud_draw_fixed_reticle 1 - Draws a crosshair on the hud\n" +
            $"• bind key \"say /holster\" - Binds the specified key to holster your weapon\n" +
            $"• _inven gems ruby 1 - Converts rubies to gems\n" +
            $"• _omatic item1 item2 item3 - Runs the Item O' Matic with the specified items\n" +
            $"• _startnuclearstrike ```\n");

        await Utilities.StatusMessage("commands", Context);
    }
}