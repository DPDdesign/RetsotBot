using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Addons.Interactive;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Discord.Commands;

namespace DiscordBot
{
   public class Program
    {


        static void Main(string[] args)
            => new Program().StartAsync().GetAwaiter().GetResult();

                DiscordSocketClient _client;
        CommandHandler _handler;
        private CommandService commands;
        private IServiceProvider services;


        public async Task StartAsync()
        {
            if (Config.bot.token == "" || Config.bot.token == null) return;
            _client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = Discord.LogSeverity.Verbose
            });
            
  
            _client.Log += Log;
            await _client.LoginAsync(TokenType.Bot, Config.bot.token);
            await _client.StartAsync();

            services = new ServiceCollection()
                .AddSingleton(_client)
                .AddSingleton<InteractiveService>()
                .BuildServiceProvider();

            commands = new CommandService();
            await commands.AddModulesAsync(Assembly.GetEntryAssembly(), services);
            _client.MessageReceived += HandleCommandAsync;

            _handler = new CommandHandler();
         //   await _handler.InitializeAsync(_client);
            await Task.Delay(-1);
        }

        private async Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.Message);
        }

        public async Task HandleCommandAsync(SocketMessage m)
        {
            if (!(m is SocketUserMessage msg)) return;
            if (msg.Author.IsBot) return;

            int argPos = 0;
            if (!(msg.HasStringPrefix("$", ref argPos))) return;

            var context = new SocketCommandContext(_client, msg);
            await commands.ExecuteAsync(context, argPos, services);
        }

    }
}
