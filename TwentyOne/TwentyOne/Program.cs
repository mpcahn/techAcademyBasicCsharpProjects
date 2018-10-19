using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    class Program
    {
        static void Main(string[] args)
        {
            Deck deck = new Deck();
            deck = Shuffle(deck, out int timesShuffled, 3);
            
            foreach (Card card in deck.Cards)
            {
                Console.WriteLine(card.Face + " of " + card.Suit);
            }
            Console.WriteLine(deck.Cards.Count);
            Console.WriteLine("Times shuffled: {0}", timesShuffled);
            Console.ReadLine();
        }

        public static Deck Shuffle(Deck deck, out int timesShuffled, int times = 1)
        {
            timesShuffled = 0;
            for (int i = 0; i < times; i++)
            {
                timesShuffled++;                
                List<Card> TempList = new List<Card>();
                Random rand = new Random();

                while (deck.Cards.Count > 0)
                {
                    int randI = rand.Next(0, deck.Cards.Count);
                    TempList.Add(deck.Cards[randI]);
                    deck.Cards.RemoveAt(randI);
                }
                deck.Cards = TempList;
                
            }
            return deck;
        }        
    }
}
