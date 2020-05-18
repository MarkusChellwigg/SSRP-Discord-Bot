using Discord.Commands;
using System;
using System.Threading.Tasks;
using SSRPBalanceBot;
using SSRPBalanceBot.Permissions;
using SSRPBalanceBot.LinkedSignatures;
using System.Net;
using System.IO;

// Keep in mind your module **must** be public and inherit ModuleBase.
// If it isn't, it will not be discovered by AddModulesAsync!
public class Signature : ModuleBase<SocketCommandContext>
{
    [Command("signature", RunMode = RunMode.Async)]
    [Alias("sig", "myguap")]
    [Summary("Returns the signature of the specified SteamID. Also adds the user to the database")]
    public async Task SendSignature([Remainder]string id = null)
    {
        byte[] sig;
        if (PermissionManager.GetPerms(Context.Message.Author.Id) < PermissionConfig.SendSignature) { await Context.Channel.SendMessageAsync("Not authorised to run this command."); return; }

        //Gets the current users signature
        if(id == null)
        {
            string steamID64 = LinkedSignatures.GetSteam(Context.Message.Author.Id.ToString());
            if (steamID64 == null) 
            { 
                await Context.Channel.SendMessageAsync($"Your Discord isn't linked to a Steam profile. To link your account, run !link");
                await Utilities.StatusMessage("signature", Context);
                return;
            }
            else
            {
                using (WebClient wc = new WebClient())
                {
                    sig = await wc.DownloadDataTaskAsync(new Uri(Utilities.GetSignature(steamID64)));
                }
                string path = $"{Utilities.RandomString(8)}.png";
                await File.WriteAllBytesAsync(path, sig);

                await Context.Channel.SendFileAsync(path, "Here's the signature you were looking for:");
                File.Delete(path);
                await Utilities.StatusMessage("signature", Context);
                return;
            }
        }
        
        //Gets the signature of a mentioned user
        else if (id.StartsWith("<"))
        {
            string steamID64 = LinkedSignatures.GetSteam(id.Replace("<@!", "").Replace(">", ""));
            if (steamID64 == null)
            {
                await Context.Channel.SendMessageAsync($"Their Discord isn't linked to a Steam profile.");
                await Utilities.StatusMessage("signature", Context);
                return;
            }
            else
            {
                using (WebClient wc = new WebClient())
                {
                    sig = await wc.DownloadDataTaskAsync(new Uri(Utilities.GetSignature(steamID64)));
                }
                string path = $"{Utilities.RandomString(8)}.png";
                await File.WriteAllBytesAsync(path, sig);

                await Context.Channel.SendFileAsync(path, "Here's the signature you were looking for:");
                File.Delete(path);

                await Utilities.StatusMessage("signature", Context);
                return;
            }
        }

        //Gets the signature of the specified syeamID
        else
        {
            using (WebClient wc = new WebClient())
            {
                sig = await wc.DownloadDataTaskAsync(new Uri(Utilities.GetSignature(id)));
            }
            string path = $"{Utilities.RandomString(8)}.png";
            await File.WriteAllBytesAsync(path, sig);

            await Context.Channel.SendFileAsync(path, "Here's the signature you were looking for:");
            File.Delete(path);

            await Utilities.StatusMessage("signature", Context);
        }
        
    }
}