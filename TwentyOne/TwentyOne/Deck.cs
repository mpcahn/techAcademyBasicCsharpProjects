﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    public class Deck
    {
        public Deck()
        {
            Cards = new List<Card>();
            List<string> Suits = new List<string>() { "Hearts", "Diamonds", "Clubs", "Spades" };
            List<string> Faces = new List<string>()
            {
                "2", "3", "4", "5", "6", "7", "8", "9", "10", "J", "Q", "K", "A"
            };

            foreach (string suit in Suits)
            {
                foreach (string face in Faces)
                {
                    Card card = new Card();
                    card.Face = face;
                    card.Suit = suit;
                    Cards.Add(card);
                }
            }
        }

        public List<Card> Cards { get; set; }
    }
}
