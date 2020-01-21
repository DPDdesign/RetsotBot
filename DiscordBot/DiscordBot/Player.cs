ng System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBot
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class __Player
    {

        public List<__Player> PlayersList = new List<__Player>();
        public List<__Player> InputList = new List<__Player>();

        int IDCounter = 0;

        public string IGN;
        public string DCN;// { get; set; }
        public int Score;// { get; set; }
        public int Place;// { get; set; }
        public int ID;

        public void GeneratePlayer()
        {
            __Player p = new __Player();
            p.IGN = "KEK";
            p.DCN = "KEKW";
            p.ID = 0;
            PlayersList.Add(p);
        }

        public void GeneratePlayer(string DiscordName, string RiotName, int id)
        {
            __Player Player = new __Player();
            Player.IGN = RiotName;
            Player.DCN = DiscordName;
            Player.ID = id;
            PlayersList.Add(Player);
            InputList.Add(Player);
        }


        public void ChangeDiscordName(string OldDiscordName, string NewDiscordName)
        {
            foreach (__Player p in PlayersList)
                if (p.DCN == OldDiscordName)
                   p.DCN = NewDiscordName;
        }

        public void ChangeGameName(string OldGameName, string NewGameName)
        {
            foreach (__Player p in PlayersList)
                if (p.IGN == OldGameName)
                    p.IGN = NewGameName;
        }

        public void ChangePoints(string DiscordName, int points)
        {
            foreach (__Player p in PlayersList)
                if (p.DCN == DiscordName)
                    p.Score = points;
        }

        //ResultNick1

        public __Player FindPlayerByID(int id)
        {
            foreach (__Player p in PlayersList)
                if (p.ID == id)
                    return p;
                else return null;

            return null;
        }

        public __Player FindPlayerByIGN(string IGN)
        {
            foreach (__Player p in PlayersList)
                if (p.IGN == IGN)
                    return p;
                else return null;

            return null;
        }

        public __Player FindPlayerByDCN(string DCN)
        {
            foreach (__Player p in PlayersList)
                if (p.DCN == DCN)
                    return p;
                else return null;

            return null;
        }


        public void AssignScores(string nickname, int points)
        {
            foreach (__Player p in PlayersList)
                if (p.IGN == nickname)
                    p.Score += points;

        }

        public List<__Player> RandomizePlayers()
        {
            Shuffle(InputList);
            return InputList;
        }

        public void ClearLists()
        {
            InputList.Clear();
            PlayersList.Clear();
        }

        private System.Random rng = new System.Random();

        public void Shuffle(List<__Player> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                __Player value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public List<__Player> LeaderBoardList(List<__Player> Input)
        {
            List<__Player> target = new List<__Player>();

            foreach (__Player p in Input)
            {
                // pierwszy toster
                if (target.Count == 0)
                {
                    target.Add(p);
                }

                // Jezeli zadaje wiecej - daj go na poczatek
                else if (p.Score > target[0].Score)
                {
                    target.Insert(0, p);
                }

                // Jezeli zadaje mniej - sortuj
                else
                {
                    bool sorting = true;
                    int i = target.Count - 1;
                    while (sorting)
                    {
                        if (p.Score <= target[i].Score)
                        {
                            target.Insert(i + 1, p);
                            sorting = false;
                        }
                        i--;
                    }
                }
            }
            return target;
        }


        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }
    }
}
