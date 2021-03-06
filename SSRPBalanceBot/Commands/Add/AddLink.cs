﻿using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using SSRPBalanceBot.LinkedSignatures;
using System.Text;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class AddLink : ModuleBase<SocketCommandContext>
{
    [Command("link", RunMode = RunMode.Async)]
    [Summary("Links a users Discord and Steam")]
    public async Task LinkAccount()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.User) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }
        if (LinkedSignatures.GetSteam(Context.Message.Author.Id.ToString()) != null) { await Context.Channel.SendMessageAsync("Account already linked"); return; }
        
        var u = Context.Message.Author;
        byte[] idBytes = Encoding.UTF8.GetBytes(u.Id.ToString());
        Console.WriteLine(idBytes);
        string encodedID = Convert.ToBase64String(idBytes);

        await Discord.UserExtensions.SendMessageAsync(u, $"Click here to link your account: https://nickgor.com/SteamAuth.php?DiscordID={Reverse(encodedID)}");
        await ReplyAsync("Check your DMs");
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}