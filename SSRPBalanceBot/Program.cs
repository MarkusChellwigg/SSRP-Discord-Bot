using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;
using System.Collections.Generic;

namespace SSRPBalanceBot
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        public CommandService _commands;

        public static string witchhunt;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _client.Log += Log;

            await InstallCommandsAsync();
            var token = "NzAxODMxOTQ5OTQ1Mjc0Mzg4.XqXh3A.hcIcpgytAnLq-xGldY2ycYYBlO8";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();


            await SetGame();
            await Task.Delay(-1);
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += HandleCommandAsync;

            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                            services: null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {

            var message = messageParam as SocketUserMessage;

            var context = new SocketCommandContext(_client, message);
            if (message == null) return;
            if (message.Author.IsBot) { return; }
            int argPos = 0;



            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Time: {DateTime.Now} | User: {message.Author} | Channel: {message.Channel}| Message: {message.Content.Replace("\n", "\\n")} | Server: {context.Guild.Name}");
            Console.ForegroundColor = ConsoleColor.Gray;

            if (message.Author.ToString().ToLower() == witchhunt)
            {
                await message.Channel.SendMessageAsync(Utilities.RandomMessage());
                return;
            }

            if (message.Content.ToLower() == "nice meth")
            {
                await message.Channel.SendMessageAsync("nice meth");
                await Utilities.StatusMessage("average", context);
                return;
            }
            else if (message.Content.ToLower().Contains("kill yourself"))
            {
                await message.Channel.SendMessageAsync("do it vro");
                await Utilities.StatusMessage("average", context);
                return;
            }

            if (!(message.HasCharPrefix('!', ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)))
                return;

            var result = await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);

            if (!result.IsSuccess && result.ErrorReason != "Unknown command.") { await context.Channel.SendMessageAsync("Check the syntax of your command and try again. Try the !help docs"); await Utilities.StatusMessage("error", context); }
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task SetGame()
        {
            await _client.SetGameAsync("!help");
        }
    }
}
