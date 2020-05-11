using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using System.Collections.Generic;
using Discord;
using System.Linq;
using System.Text;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
[Group("admin")]
public class AdminHelp : ModuleBase<SocketCommandContext>
{
    [Command("help", RunMode = RunMode.Async)]
    [Summary("Sends help info")]
    public async Task SendAdminHelp()
    {
        Program p = new Program();

        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendAdminHelp) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        List<CommandInfo> commands = Program._commands.Commands.ToList();
        StringBuilder helpMessage = new StringBuilder();
        StringBuilder individualCMDs = new StringBuilder();


        foreach (CommandInfo command in commands)
        {
            if (command.Module.Group == "admin")
            {
                //Name of command - Example, !help
                individualCMDs.Append("!" + command.Name);

                //Appends all parameters - Example, [item]
                foreach (ParameterInfo param in command.Parameters)
                {
                    individualCMDs.Append($" [{param.Name}]");
                }
                //Appends the command summary, Example - Sends help information
                individualCMDs.Append($" - {command.Summary}\n");

                helpMessage.Append(individualCMDs.ToString());
                individualCMDs.Clear();
            }
        }

        await Context.Message.Channel.SendMessageAsync($"{Context.Message.Author.Mention}\n```{helpMessage.ToString()}```");

        await Utilities.StatusMessage("help", Context);
    }
}