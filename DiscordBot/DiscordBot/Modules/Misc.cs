using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace DiscordBot.Modules
{



    public class Misc :ModuleBase<SocketCommandContext>
    {

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

        }

        [Command("DCN")]
        public async Task DCN()
        {          
            var embed = new EmbedBuilder();

            embed.WithTitle("List of Discord Tags:");
            embed.WithDescription(Utilities.GameNames);
            await Context.Channel.SendMessageAsync("", false, embed.Build());

        }

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
    }
}
