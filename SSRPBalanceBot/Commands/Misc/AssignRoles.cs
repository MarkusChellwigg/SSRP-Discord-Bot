﻿using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using System.Linq;
using SSRPBalanceBot.Permissions;
using SSRPBalanceBot.LinkedSignatures;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
[Group("admin")]
public class AutoAssign : ModuleBase<SocketCommandContext>
{
    [Command("autoassign", RunMode = RunMode.Async)]
    [Summary("Assigns users to their gang roles")]
    public async Task AssignRoles()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.User) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        foreach (var member in Context.Guild.Users)
        {
            if (!member.IsBot)
            {
                string steamID64 = LinkedSignatures.GetSteam(member.Id.ToString());
                string gang = await Utilities.GetGang(steamID64);

                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == gang);
                if (role != null)
                {
                    await member.AddRoleAsync(role);
                    await ReplyAsync($"{member.Username} has been assigned to {gang}");
                }
                else
                {
                    if (gang != "No Gang yet.")
                    {
                        await Context.Guild.CreateRoleAsync(gang, null, Discord.Color.Blue, false, null);
                        await member.AddRoleAsync(role);
                        await ReplyAsync($"{member.Username} has been assigned to {gang}");
                    }
                }
            }
        }

        //await Context.Channel.SendMessageAsync($"{Context.Message.Author.Mention} rolled `{roll}`");
        await Utilities.StatusMessage("autoassign", Context);
    }
}