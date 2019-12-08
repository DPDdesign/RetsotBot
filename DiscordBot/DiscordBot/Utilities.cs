﻿using System;
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

        public static int playersnumber = 128;
        public static int playerscount = 0;

        private static Dictionary<string, string> alerts;
        private const string ConfigFolder = "SystemLang";
        private const string ConfigFile = "tournament.json";
        static __Player playerobject = new __Player();

        List<__Player> RandomPlayers = new List<__Player>();



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

        public static void GeneratePlayer(string DCN, string IGN)
        {
            playerobject.GeneratePlayer(DCN, IGN, playerscount);
            playerscount++;
        }

        public static string Test(int i)
        {
            __Player p = playerobject.FindPlayerByID(i);
            return p.DCN;
        }

        public static string GenerateIGN()
        {
            string text = "";
            foreach (__Player p in playerobject.PlayersList)
            {
                text += p.IGN + "\n";
            }
            return text;
        }

        public static string GenerateIGNFrom(int i, int j)
        {
            string text = "";

            for (int k=i; k<j; k++)
            {
                text += playerobject.PlayersList[k].IGN + "\n";
            }
            
            return text;
        }

        public static string GenerateDCNFrom(int i, int j)
        {
            string text = "";

            for (int k = i; k < j; k++)
            {
                text += playerobject.PlayersList[k].DCN + "\n";
            }

            return text;
        }

        public static string GenerateDCN()
        {
            string text = "";
            foreach (__Player p in playerobject.PlayersList)
            {
                text += p.DCN + "\n";
            }
            return text;
        }

        public static string GeneratePlayersList()
        {
            int i = 1;
            string text = "";
            foreach (__Player p in playerobject.PlayersList)
            {
                text += p.DCN + "  -  " + p.IGN + "\n";
            }
            return text;
        }

        public static string GenerateLeaderboard()
        {
            int i = 1;
            string text = "";
            foreach (__Player p in playerobject.LeaderBoardList(playerobject.PlayersList))
            {
                text += i + ". " + p.DCN + "  -  " + p.IGN + " " + p.Score + "\n";
                i++;
            }
            return text;
        }

        public static void ChangeDCN(string OldDCN, string NewDCN)
        {
            playerobject.ChangeDiscordName(OldDCN, NewDCN);
        }

        public static void ChangeIGN(string OldIGN, string NewIGN)
        {
            playerobject.ChangeGameName(OldIGN, NewIGN);
        }

        public static void ChangePoints(string DCN, int points)
        {
            playerobject.ChangePoints(DCN, points);
        }

        void GenerateLobbies()
        {
            RandomPlayers = playerobject.RandomizePlayers();


            foreach (List<__Player> l in ListOfLobbies)
            {
                l.Clear();
            }

            foreach (string s in ListOfString)
            {
                s.Remove(0);
            }

            for (int i = 0; i < 8; i++)
            {

                CopyTextA += "@" + RandomPlayers[i].DCN + "-" + RandomPlayers[i].IGN + "\n";
                TempLobby1.Add(new __Player() { IGN = RandomPlayers[i].IGN });

                CopyTextB += "@" + RandomPlayers[i + 8].DCN + "-" + RandomPlayers[i + 8].IGN + "\n";
                TempLobby2.Add(new __Player() { IGN = RandomPlayers[i + 8].IGN });

                CopyTextC += "@" + RandomPlayers[i + 16].DCN + "-" + RandomPlayers[i + 16].IGN + "\n";
                TempLobby3.Add(new __Player() { IGN = RandomPlayers[i + 16].IGN });

                CopyTextD += "@" + RandomPlayers[i + 24].DCN + "-" + RandomPlayers[i + 24].IGN + "\n";
                TempLobby4.Add(new __Player() { IGN = RandomPlayers[i + 24].IGN });

                //

                CopyTextE += "@" + RandomPlayers[i + 32].DCN + "-" + RandomPlayers[i + 32].IGN + "\n";
                TempLobby5.Add(new __Player() { IGN = RandomPlayers[i + 32].IGN });

                CopyTextF += "@" + RandomPlayers[i + 40].DCN + "-" + RandomPlayers[i + 40].IGN + "\n";
                TempLobby6.Add(new __Player() { IGN = RandomPlayers[i + 40].IGN });

                CopyTextG += "@" + RandomPlayers[i + 48].DCN + "-" + RandomPlayers[i + 48].IGN + "\n";
                TempLobby7.Add(new __Player() { IGN = RandomPlayers[i + 48].IGN });

                CopyTextH += "@" + RandomPlayers[i + 56].DCN + "-" + RandomPlayers[i + 56].IGN + "\n";
                TempLobby8.Add(new __Player() { IGN = RandomPlayers[i + 56].IGN });

                //

                CopyTextI += "@" + RandomPlayers[i + 64].DCN + "-" + RandomPlayers[i + 64].IGN + "\n";
                TempLobby9.Add(new __Player() { IGN = RandomPlayers[i + 64].IGN });

                CopyTextJ += "@" + RandomPlayers[i + 72].DCN + "-" + RandomPlayers[i + 72].IGN + "\n";
                TempLobby10.Add(new __Player() { IGN = RandomPlayers[i + 72].IGN });

                CopyTextK += "@" + RandomPlayers[i + 80].DCN + "-" + RandomPlayers[i + 80].IGN + "\n";
                TempLobby11.Add(new __Player() { IGN = RandomPlayers[i + 80].IGN });

                CopyTextL += "@" + RandomPlayers[i + 88].DCN + "-" + RandomPlayers[i + 88].IGN + "\n";
                TempLobby12.Add(new __Player() { IGN = RandomPlayers[i + 88].IGN });

                //

                CopyTextM += "@" + RandomPlayers[i + 96].DCN + "-" + RandomPlayers[i + 96].IGN + "\n";
                TempLobby13.Add(new __Player() { IGN = RandomPlayers[i + 96].IGN });

                CopyTextN += "@" + RandomPlayers[i + 104].DCN + "-" + RandomPlayers[i + 104].IGN + "\n";
                TempLobby14.Add(new __Player() { IGN = RandomPlayers[i + 104].IGN });

                CopyTextO += "@" + RandomPlayers[i + 112].DCN + "-" + RandomPlayers[i + 112].IGN + "\n";
                TempLobby15.Add(new __Player() { IGN = RandomPlayers[i + 112].IGN });

                CopyTextP += "@" + RandomPlayers[i + 120].DCN + "-" + RandomPlayers[i + 120].IGN + "\n";
                TempLobby16.Add(new __Player() { IGN = RandomPlayers[i + 120].IGN });
            }
        }





        public static string DiscordNames = "";
        public static string GameNames = "";
        public static List<string> checkedin = new List<string>();

        #region definitions


        public static void StartUp()
        {
            ListOfLobbies.Add(TempLobby1);
            ListOfLobbies.Add(TempLobby2);
            ListOfLobbies.Add(TempLobby3);
            ListOfLobbies.Add(TempLobby4);

            ListOfLobbies.Add(TempLobby5);
            ListOfLobbies.Add(TempLobby6);
            ListOfLobbies.Add(TempLobby7);
            ListOfLobbies.Add(TempLobby8);

            ListOfLobbies.Add(TempLobby9);
            ListOfLobbies.Add(TempLobby10);
            ListOfLobbies.Add(TempLobby11);
            ListOfLobbies.Add(TempLobby12);

            ListOfLobbies.Add(TempLobby13);
            ListOfLobbies.Add(TempLobby14);
            ListOfLobbies.Add(TempLobby15);
            ListOfLobbies.Add(TempLobby16);

            ListOfString.Add(CopyTextA);
            ListOfString.Add(CopyTextB);
            ListOfString.Add(CopyTextC);
            ListOfString.Add(CopyTextD);

            ListOfString.Add(CopyTextE);
            ListOfString.Add(CopyTextF);
            ListOfString.Add(CopyTextG);
            ListOfString.Add(CopyTextH);

            ListOfString.Add(CopyTextI);
            ListOfString.Add(CopyTextJ);
            ListOfString.Add(CopyTextK);
            ListOfString.Add(CopyTextL);

            ListOfString.Add(CopyTextM);
            ListOfString.Add(CopyTextN);
            ListOfString.Add(CopyTextO);
            ListOfString.Add(CopyTextP);
            int j = 1;
            for (int i = 0; i < 128 - playerscount; i+=0)
            {
                GeneratePlayer("Random#" + j, "RandomName" + j);
                j++;
            }

        }

        public static string x()
        {
            string txt = playerobject.PlayersList.Count().ToString() + " pcount  " + playerscount;
            return txt;
        }

        public static List<__Player> TempLobby1 = new List<__Player>();
        public static List<__Player> TempLobby2 = new List<__Player>();
        public static List<__Player> TempLobby3 = new List<__Player>();
        public static List<__Player> TempLobby4 = new List<__Player>();

        public static List<__Player> TempLobby5 = new List<__Player>();
        public static List<__Player> TempLobby6 = new List<__Player>();
        public static List<__Player> TempLobby7 = new List<__Player>();
        public static List<__Player> TempLobby8 = new List<__Player>();

        public static List<__Player> TempLobby9 = new List<__Player>();
        public static List<__Player> TempLobby10 = new List<__Player>();
        public static List<__Player> TempLobby11 = new List<__Player>();
        public static List<__Player> TempLobby12 = new List<__Player>();

        public static List<__Player> TempLobby13 = new List<__Player>();
        public static List<__Player> TempLobby14 = new List<__Player>();
        public static List<__Player> TempLobby15 = new List<__Player>();
        public static List<__Player> TempLobby16 = new List<__Player>();

        public static List<List<__Player>> ListOfLobbies = new List<List<__Player>>();

        public static string CopyTextA = "LOBBY 1 \r\n";
        public static string CopyTextB = "LOBBY 2 \r\n";
        public static string CopyTextC = "LOBBY 3 \r\n";
        public static string CopyTextD = "LOBBY 4 \r\n";

        public static string CopyTextE = "LOBBY 5 \r\n";
        public static string CopyTextF = "LOBBY 6 \r\n";
        public static string CopyTextG = "LOBBY 7 \r\n";
        public static string CopyTextH = "LOBBY 8 \r\n";

        public static string CopyTextI = "LOBBY 9 \r\n";
        public static string CopyTextJ = "LOBBY 10 \r\n";
        public static string CopyTextK = "LOBBY 11 \r\n";
        public static string CopyTextL = "LOBBY 12 \r\n";

        public static string CopyTextM = "LOBBY 13 \r\n";
        public static string CopyTextN = "LOBBY 14 \r\n";
        public static string CopyTextO = "LOBBY 15 \r\n";
        public static string CopyTextP = "LOBBY 16 \r\n";
        public static List<string> ListOfString = new List<string>();
        #endregion





    }
}
