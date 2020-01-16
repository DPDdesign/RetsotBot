using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.Addons.Interactive;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;


namespace DiscordBot.Modules
{

    public class Misc : InteractiveBase //ModuleBase<SocketCommandContext>
    {
        [Command("delete")]
        public async Task<RuntimeResult> Test_DeleteAfterAsync()
        {
            await ReplyAndDeleteAsync("this message will delete in 10 seconds", timeout: TimeSpan.FromSeconds(10));
            return Ok();
        } 



        [Command("next", RunMode = RunMode.Async)]
        public async Task Test_NextMessageAsync()
        {
            await ReplyAsync("What is 2+2?");
            var response = await NextMessageAsync();
            if (response != null)
                await ReplyAsync($"You replied: {response.Content}");
            else
                await ReplyAsync("You did not reply before the timeout");
        }

        [Command("Me")]
        public async Task Me()
        {
            string text = "";
            List<IMessage> listofmessages = new List<IMessage>();
            IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(1).FlattenAsync();
            listofmessages = messages.ToList<IMessage>();


            text+= listofmessages[0].Author;
            await ReplyAsync(text);
            
        } 


        [Command("OpenSignUp")]
        public async Task OpenSignUp([Remainder]string message)
        {
            string[] options = message.Split('|');
            var embed = new EmbedBuilder();

            embed.WithTitle(options[0]);
            embed.WithDescription(options[1] + " Sign Up for tournament now open! \n Want to join? React with the :white_check_mark: emoji to this message. \n Make sure you have enough time to play before you do though." );
            embed.WithColor(new Discord.Color(0, 255, 0));
            embed.WithFooter(footer => footer.Text = "Event start:" + options [2]);
            //embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
            //embed.WithUrl("https://example.com");
            //embed.WithCurrentTimestamp();

            IUserMessage m = await Context.Channel.SendMessageAsync(null, false, embed.Build());
            Utilities.g = m;
            
        }


        [Command("CloseSignUp")]
        public async Task CloseSignUp(int number)
        {
            var emoji = new Emoji("✅");
            string text = "SignUp is closed! \n Following players are in: \n \n";
            var reactedusers = await Utilities.g.GetReactionUsersAsync(emoji, number).FlattenAsync();
            List<IUser> asList = reactedusers.ToList();

            foreach (IUser k in reactedusers)
            {
                text += k.Mention + "\n";
            }
            IUserMessage m = await ReplyAsync(text + "Now please write your IGN to check in!");
        }


        [Command("getplayers")]
        public async Task PurgeChat(int amount)
        {
            string text = "";
            List<IMessage> listofmessages = new List<IMessage>();
            IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(amount+1).FlattenAsync();
            listofmessages = messages.ToList<IMessage>();
            listofmessages.RemoveAt(0);


            await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);
            foreach (IMessage k in listofmessages)
            {
                if (k.Author.IsBot) { text += k.Content + "\n"; }
                else{Utilities.GeneratePlayer(k.Author.ToString(),k.Content);}                                                                   
            }

                            Utilities.StartUp();
                foreach (__Player p in Utilities.playerobject.InputList){
                text+= " @" + p.DCN + " - " + p.IGN + "\n";    
                }  
           // const int delay = 3000;
            var emoji = new Emoji("U0002705");
            IUserMessage m = await ReplyAsync("Here is a player list: \n" + text);

            // IUserMessage x = await ReplyAsync(reactedUsers.ToString);
            //await Task.Delay(delay);
            //await m.DeleteAsync();
        }

        [Command("GenerateLobbies")]
        public async Task GenerateLobbies()
        {
            Utilities.StartUp();
            Utilities.GenerateLobbies();

            var pages = new[] { Utilities.CopyTextA, Utilities.CopyTextB, Utilities.CopyTextC, Utilities.CopyTextD, Utilities.CopyTextE};
            await PagedReplyAsync(pages);
        }

        [Command("ReadPoints")]
        public async Task ReadPoints()
        {
            Utilities.SetPoints();
            //Utilities.StartUp();
            //Utilities.GenerateLobbies();
          // Utilities.SetPoints();
            //var pages = new[] { Utilities.CopyTextA, Utilities.CopyTextB, Utilities.CopyTextC, Utilities.CopyTextD, Utilities.CopyTextE};
            await Context.Channel.SendMessageAsync("DX");
        }

                [Command("WriteToSpreadsheet")]
        public async Task WriteToSpreadsheet([Remainder]string message)
        {
            string[] options = message.Split('|');   
              var oblist = new List<object>();

            int k = options.Count();
           for(int i=2; i<k; i++)
            {
                oblist.Add(options[i]);
            }

            var ValueRange = new ValueRange();
          
           // oblist.Add(options[2]);
            ValueRange.Values = new List<IList<object>> { oblist };

            

            Program.WriteToSpreadsheet(options[0],options[1],oblist);
           await Context.Channel.SendMessageAsync("XD");

        }


        


        [Command("Tiebreak")]
        public async Task Pickone([Remainder]string message)
        {
            string[] options = message.Split('|');

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];


            var embed = new EmbedBuilder();

            embed.WithTitle("I have chosen:");
            embed.WithDescription(selection);


            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("test")]
        public async Task test()
        {

            var embed = new EmbedBuilder();

            embed.WithDescription("Number of objects: " + Utilities.x());

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("ListrangeIGN")]
        public async Task ListrangeIGN([Remainder]string message)
        {
            string[] options = message.Split('|');

            var embed = new EmbedBuilder();
            embed.WithDescription(Utilities.GenerateIGNFrom(Convert.ToInt32(options[0]), Convert.ToInt32(options[1])));


            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("getcheckins")]
        public async Task getcheckins(int amount)
        {
            string text ="";
            List<IMessage> listofmessages = new List<IMessage>();
            IEnumerable<IMessage> messages = await Context.Channel.GetMessagesAsync(amount + 1).FlattenAsync();
            listofmessages = messages.ToList<IMessage>();
            listofmessages.RemoveAt(0);
            await ((ITextChannel)Context.Channel).DeleteMessagesAsync(messages);
            foreach (IMessage k in listofmessages)
            {

                Utilities.GeneratePlayer("@" + k.Author, k.Content);
               text = Utilities.GeneratePlayersList();
            }
            const int delay = 3000;
            IUserMessage m = await ReplyAsync(text);
            await Task.Delay(delay);
            //await m.DeleteAsync();
        }


        [Command("ListrangeDCN")]
        public async Task ListrangeDCN([Remainder]string message)
        {
            string[] options = message.Split('|');

            var embed = new EmbedBuilder();
            embed.WithDescription(Utilities.GenerateDCNFrom(Convert.ToInt32(options[0]), Convert.ToInt32(options[1])));

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }


        [Command("format")]
        public async Task format([Remainder]string message)
        {
            var embed = new EmbedBuilder();

            Utilities.playersnumber = Convert.ToInt32(message);
            embed.WithDescription("format set to: " + message + " players");

            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }


        [Command("Checkin")]
        public async Task Checkin([Remainder]string message)
        {
            string DCN = Context.User.ToString();
            string IGN = Context.Message.ToString().Remove(0, 9);
            

            var embed = new EmbedBuilder();



            //if (!Utilities.checkedin.Contains(DCN))
            //{   
                Utilities.GeneratePlayer(DCN, IGN);
                embed.WithTitle("Welcome!");
                embed.WithDescription(Context.User.Mention + " you have succesfully checked in as: \n" + IGN);
                embed.WithColor(new Discord.Color(0, 255, 0));
                embed.WithFooter(footer => footer.Text = "Current players checked in: " + Utilities.playerscount + "/ 128");
                embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
                //embed.WithUrl("https://example.com");
                embed.WithCurrentTimestamp();
                Utilities.checkedin.Add(DCN);
            //}

            //else
            //{
     //           embed.WithTitle("WARNING!");
   //             embed.WithDescription("You have already checked in!");
 //           }

            await Context.Channel.SendMessageAsync(null, false, embed.Build());

        }



        [Command("ChangeDCN")]
        public async Task changeDCN([Remainder]string message)
        {

            string[] options = message.Split('|');

            var embed = new EmbedBuilder();
            Utilities.ChangeDCN(options[0],options[1]);
            embed.WithDescription("Discord Tag changed to: " +options[1]);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("ChangeIGN")]
        public async Task changeIGN([Remainder]string message)
        {

            string[] options = message.Split('|');

            var embed = new EmbedBuilder();
            Utilities.ChangeIGN(options[0], options[1]);
            embed.WithDescription("Game Name changed to: " + options[1]);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("ChangeScore")]
        public async Task changeScore([Remainder]string message)
        {

            string[] options = message.Split('|');

            var embed = new EmbedBuilder();
            Utilities.ChangePoints(options[0], Convert.ToInt32(options[1]));
            embed.WithDescription("Points changed to: " + options[1]);
            await Context.Channel.SendMessageAsync("", false, embed.Build());
        }

        [Command("Open")]
        public async Task Start()
        {
            var embed = new EmbedBuilder();
            embed.WithDescription("Tournamend Opened!");
            embed.WithDescription("Good luck \n  " + (Utilities.playersnumber - Utilities.playerscount) + " Random playersgenerated");
            Utilities.StartUp();
            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("ListIGN")]
        public async Task IGN()
        {
            var embed = new EmbedBuilder();

            embed.WithTitle("List of game names:");
            embed.WithDescription(Utilities.GenerateIGN());
            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("ListDCN")]
        public async Task DCN()
        {
            var embed = new EmbedBuilder();

            embed.WithTitle("List of Discord tags:");
            embed.WithDescription(Utilities.GenerateDCN());
            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("ListPlayers")]
        public async Task Players()
        {
            var embed = new EmbedBuilder();

            embed.WithTitle("List of players:");
            embed.WithDescription(Utilities.GeneratePlayersList());
            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        [Command("Leaderboard")]
        public async Task Leaderboard()
        {
            var embed = new EmbedBuilder();

            embed.WithTitle("List of players:");
            embed.WithDescription(Utilities.GenerateLeaderboard());
            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

        #region old
        /*
        [Command("Checkin")]
        public async Task Checkin([Remainder]string message)
        {          
            string DCN = Context.User.ToString();
            string IGN = Context.Message.ToString().Remove(0,8);
            Utilities.playerscount ++;

            var embed = new EmbedBuilder();
           
      

            if(!Utilities.checkedin.Contains(DCN)){
            Utilities.DiscordNames += DCN + "\n";
            Utilities.GameNames += IGN + "\n";
            embed.WithTitle("Welcome!");
            embed.WithDescription(Context.User.Mention + " you have succesfully checked in as: \n" + IGN);
            embed.WithColor(new Color(0, 255, 0));
            embed.WithFooter(footer => footer.Text = "Current players checked in: " + Utilities.playerscount + "/ 128");
             embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
            //embed.WithUrl("https://example.com");
            embed.WithCurrentTimestamp();
            Utilities.checkedin.Add(DCN);
            }

            else{
            embed.WithTitle("WARNING!");
            embed.WithDescription("You have already checked in!");
            }

           await Context.Channel.SendMessageAsync(null, false, embed.Build());

        }


        [Command("IGN")]
        public async Task IGN()
        {          
            var embed = new EmbedBuilder();

            embed.WithTitle("List of Game Names:");
            embed.WithDescription(Utilities.DiscordNames);
            await Context.Channel.SendMessageAsync("", false, embed.Build());

        } */



        /*
         * 
         *         [Command("echo")]
                public async Task Echo([Remainder]string message)
                {

                    var embed = new EmbedBuilder();

                    embed.WithTitle("Echoed message");
                    embed.WithDescription(message);
                    embed.WithColor(new Color(0, 255, 0));

                    await Context.Channel.SendMessageAsync("", false,embed.Build());

                }
                public List<string> Roles = new List<string>();
         * 
         * 
         * 
                [Command("GenerateTeam")]
                public async Task JoinVoiceChannel([Remainder]string message)
                {

                    Roles.Add("Top");
                    Roles.Add("Mid");
                    Roles.Add("Jungle");
                    Roles.Add("ADC");
                    Roles.Add("Support");

                    Random r = new Random();
                    int i = 0;
                    string write = "";

                                var embed = new EmbedBuilder();


                        int n = Roles.Count;
                        while (n > 1)
                        {
                            n--;
                            int k = r.Next(n + 1);
                            var value = Roles[k];
                            Roles[k] = Roles[n];
                            Roles[n] = value;
                        }




                    //await Context.Channel.SendMessageAsync(Context.User.Username + "Have randomized a team named: " + message);
                    var VoiceUsers = Context.Guild.GetVoiceChannel(619552205892091927).Users;
                    foreach (var v in VoiceUsers)
                    {
                        write += (v.Mention  +  "   - "  + Roles[i] + "\n");

                        i++;
                    }



                    embed.WithTitle("Team " +  "DD: " + Context.Message.Author + message);
                    embed.WithDescription(write);
                    embed.WithColor(new Color(0, 255, 0));
                    //embed.WithFooter(footer => footer.Text = "I am a footer.");
                    //embed.WithUrl("https://example.com");
                    embed.WithCurrentTimestamp();


                    Roles.Clear();
                   await Context.Channel.SendMessageAsync(Context.User.Username + " Created:", false, embed.Build());

                }
        */

        // embed.WithColor(new Color(255, 255, 0));
        // embed.WithThumbnailUrl(Context.User.GetAvatarUrl());
        #endregion
    }
}
