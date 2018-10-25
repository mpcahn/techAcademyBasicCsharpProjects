﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    public abstract class Game
    {
        private List<Player> _players = new List<Player>();
        private Dictionary<Player, int> _bets = new Dictionary<Player, int>();

        public List<Player> Players { get { return _players; } set { _players = value; } }
        public Dictionary<Player, int> Bets { get { return _bets; }  set { _bets = value; } }
        public string Name { get; set; }
        

        public abstract void Play(); //Any class inherited must implement Play        
        public virtual void ListPlayers() //virtual means inherited classes can override
        {
            foreach (Player player in Players)
            {
                Console.WriteLine(player.Name);
            }
        }
    }
}
