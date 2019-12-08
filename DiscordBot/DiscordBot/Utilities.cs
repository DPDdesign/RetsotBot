using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace DiscordBot
{
   public class Utilities
    {
        private static Dictionary<string, string> alerts;
        private const string ConfigFolder = "SystemLang";
        private const string ConfigFile = "tournament.json";

        static Utilities()
        {
            string json = File.ReadAllText("SystemLang/alerts.json");
            var data = JsonConvert.DeserializeObject<dynamic>(json);
            alerts = data.ToObject<Dictionary<string, string>>();
        }

        public static string GetAlert(string key)
        {
            if (alerts.ContainsKey(key)) return alerts[key];
            return " key not fund";
        }


        public struct TournamentPlayer
        {
            public string InGameNick;
            public string DiscordName;
        }

    public static int playerscount = 0;
    public static string DiscordNames = "\n";
    public static string GameNames = "";
    public static List<string> checkedin =new List<string>();
    }
}
