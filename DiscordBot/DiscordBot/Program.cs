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
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace DiscordBot
{
   public class Program
    {


        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static SheetsService service;
        static String spreadsheetId = "1Z85dCVAAJ8YjDNnh_6WKOMWWq3mB4Vi6jENDWit1-yg";
        static string ApplicationName = "Google Sheets API .NET Quickstart";
        
        
        static void Main(string[] args)
        {
            string credpath = "token.json";

                            GoogleCredential credential;
                using (var stream = new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
                {
                  credential = GoogleCredential.FromStream(stream)
        .CreateScoped(Scopes);
                /*
                    credential = GoogleWebAuthorizationBroker.AuthorizeAsync(GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        "Ephos",
                        CancellationToken.None,
                        new FileDataStore(credpath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credpath);*/
                }

                // Create Google Sheets API service.
                service = new SheetsService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential,
                    ApplicationName = ApplicationName,
                });

            /*
            UserCredential credential;
            
            using (var stream =
                new FileStream("credentials.json", FileMode.Open, FileAccess.Read))
            {
                // The file token.json stores the user's access and refresh tokens, and is created
                // automatically when the authorization flow completes for the first time.
                string credPath = "token.json";
                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    "user",
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Console.WriteLine("Credential file saved to: " + credPath);
            }

            // Create Google Sheets API service.
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
            CreateEntry();
            ReadEntries();
            */
          //  ReadEntries();
          //  CreateEntry();
          //UpdateEntry();
            DeleteEntry();
            new Program().StartAsync().GetAwaiter().GetResult(); }

     //   static string[] Scopes = { SheetsService.Scope.Spreadsheets};
       
        

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

        static void ReadEntries(){
        // Define request parameters.
           
            String range = "Players!A1:E";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadsheetId, range);

            // Prints the names and majors of students in a sample spreadsheet:
            // https://docs.google.com/spreadsheets/d/1BxiMVs0XRA5nFMdKvBdBZjgmUUqptlbs74OgvE2upms/edit
            ValueRange response = request.Execute();
            IList<IList<Object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                Console.WriteLine("Name, Major");
                foreach (var row in values)
                {
                    // Print columns A and E, which correspond to indices 0 and 4.
                    Console.WriteLine("{0}, {1}", row[0], row[0]);

                }
            }
            else
            {
                Console.WriteLine("No data found.");
            }
            Console.Read();
         }

        static void CreateEntry()
        {
            var range = "Players!A:F";
            var valueRange = new ValueRange();

            var oblist = new List<object>() { "Hello!", "This", "was", "insertd", "via", "C#" };
            valueRange.Values = new List<IList<object>> { oblist };


            var AppendRequest = service.Spreadsheets.Values.Append(valueRange,spreadsheetId,range);
            AppendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendResponse = AppendRequest.Execute();

        }

        static void UpdateEntry()
{
    var range = "Players!D543";
    var valueRange = new ValueRange();

    var oblist = new List<object>() { "updated" };
    valueRange.Values = new List<IList<object>> { oblist };

    var updateRequest = service.Spreadsheets.Values.Update(valueRange, spreadsheetId, range);
    updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
    var appendReponse = updateRequest.Execute();
}

        static void DeleteEntry()
{
    var range = "Players!A3";
    var requestBody = new ClearValuesRequest();

    var deleteRequest = service.Spreadsheets.Values.Clear(requestBody, spreadsheetId, range);
    var deleteReponse = deleteRequest.Execute();
}



    }
}
