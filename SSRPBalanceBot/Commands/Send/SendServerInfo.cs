using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using QueryMaster;
using System.Collections.Generic;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class ServerInfo : ModuleBase<SocketCommandContext>
{
    [Command("online", RunMode = RunMode.Async)]
    [Summary("Returns the total balance of all users in the database")]
    public async Task SendInfo()
    {
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendTotal) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        QueryMaster.Server server1 = ServerQuery.GetServerInstance(EngineType.Source, "54.36.229.194", 27015);
        QueryMaster.ServerInfo s1Info = server1.GetInfo();
        QueryMaster.Server server2 = ServerQuery.GetServerInstance(EngineType.Source, "51.81.120.22", 27015);
        QueryMaster.ServerInfo s2Info = server2.GetInfo();

        await Context.Channel.SendMessageAsync($"```--- Current Online Players ----\nServer 1: {s1Info.Players}/{s1Info.MaxPlayers}\nServer 3: {s2Info.Players}/{s2Info.MaxPlayers}```");
        await Utilities.StatusMessage("online", Context);
    }
}