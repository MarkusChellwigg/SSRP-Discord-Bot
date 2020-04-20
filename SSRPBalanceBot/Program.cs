using System;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Commands;
using System.Reflection;

namespace SSRPBalanceBot
{
    class Program
    {
        public static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;
        private CommandService _commands;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _commands = new CommandService();

            _client.Log += Log;

            await InstallCommandsAsync();
            var token = "tokenHere";

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();
            await SSRPItems.AddItems();

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
            if (message == null) return;

            int argPos = 0;

            if (message.Author.IsBot) { return; }

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Time: {DateTime.Now} | User: {message.Author} | Channel: {message.Channel}| Message: {message.Content.Replace("\n", "\\n")} | Server: {message.Source}");
            Console.ForegroundColor = ConsoleColor.Gray;
+
    
            if (message.Content.ToLower() == "nice meth")
            {
                await message.Channel.SendMessageAsync("nice meth");
                await Utilities.StatusMessage($"Time: {DateTime.Now} | Ran command: [nice meth] | Server: {message.Source}");
            }

            if (!(message.HasCharPrefix('!', ref argPos) ||
                message.HasMentionPrefix(_client.CurrentUser, ref argPos)))
                return;



            var context = new SocketCommandContext(_client, message);

            await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: null);
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
