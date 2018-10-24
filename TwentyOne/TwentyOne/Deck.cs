using System;
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
            
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < 13; i++)
                {
                    Card card = new Card();
                    card.Suit = (Suit)j;
                    card.Face = (Face)i;
                    Cards.Add(card);
                }
            }
        }

        public List<Card> Cards { get; set; }

        public void Shuffle(int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                List<Card> TempList = new List<Card>();
                Random rand = new Random();

                while (Cards.Count > 0)
                {
                    int randI = rand.Next(0,Cards.Count);
                    TempList.Add(Cards[randI]);
                    Cards.RemoveAt(randI);
                }
                Cards = TempList;
            }            
        }
    }
}
