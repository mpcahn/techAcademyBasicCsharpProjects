using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwentyOne
{
    class TwentyOneGame : Game, IWalkAway
    {
        public TwentyOneDealer Dealer { get; set; }
        public override void Play()
        {
            //Make new hand and deck and ask for bet.
            Dealer = new TwentyOneDealer();
            foreach (Player player in Players)
            {
                player.Hand = new List<Card>();
                player.Stay = false;
            }
            Dealer.Deck = new Deck();
            Dealer.Deck.Shuffle();

            Console.WriteLine("Place your bet:");

            //Place Bets
            foreach (Player player in Players)
            {
                int bet = Convert.ToInt32(Console.ReadLine());
                bool BetSuccess = player.Bet(bet);

                if (!BetSuccess)
                {
                    return;
                }

                Bets[player] = bet;
            }     
            
            //Deal
            for (int i = 0; i < 2; i++)
            {
                Console.WriteLine("Dealing...");
                foreach (Player player in Players)
                {
                    Console.Write("{0}: ", player.Name);
                    Dealer.Deal(player.Hand);
                    if (i == 1)
                    {
                        //Check for blackjack
                        bool blackJack = TwentyOneRules.CheckForBlackJack(player.Hand);
                        if (blackJack)
                        {
                            Console.WriteLine("Blackjack! {0} Wins {1}!", player.Name, Bets[player]);
                            player.Balance += Convert.ToInt32((Bets[player] * 2) + Bets[player]);
                            return;
                        }
                    }
                }
                Console.Write("Dealer...");
                Dealer.Deal(Dealer.Hand);

                if (i == 1)
                {
                    bool blackJack = TwentyOneRules.CheckForBlackJack(Dealer.Hand);
                    if (blackJack)
                    {
                        Console.WriteLine("Dealer has Blackjack, all players lose.");
                        foreach (KeyValuePair<Player, int> entry in Bets)
                        {
                            Dealer.Balance += entry.Value;
                        }
                        return;
                    }
                }
            }
            
            foreach (Player player in Players)
            {
                while (!player.Stay)
                {
                    Console.WriteLine("You're cards are: ");
                    foreach (Card card in player.Hand)
                    {
                        Console.Write("{0} ", card.ToString());
                    }
                    Console.WriteLine("\n\nHit or stay?");
                    string answer = Console.ReadLine().ToLower();
                    if (answer == "stay")
                    {
                        player.Stay = true;
                        break;
                    }
                    else if (answer == "hit")
                    {
                        Dealer.Deal(player.Hand);
                    }
                    bool busted = TwentyOneRules.IsBusted(player.Hand);
                    if (busted)
                    {
                        Dealer.Balance += Bets[player];
                        Console.WriteLine("{0} Busted! You lose your bet of {1}. Your balance is now {2}.", player.Name, Bets[player], player.Balance);
                        Console.WriteLine("How about another round?");
                        answer = Console.ReadLine().ToLower();

                        if (answer == "yes" || answer == "y")
                        {
                            player.IsPlaying = true;
                        }
                        else
                        {
                            player.IsPlaying = false;
                        }
                    }
                }
            }

            Dealer.Bust = TwentyOneRules.IsBusted(Dealer.Hand);
            Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand);
            
            while(!Dealer.Stay && !Dealer.Bust)
            {
                Console.WriteLine("Dealer Hits!");
                Dealer.Deal(Dealer.Hand);
                Dealer.Bust = TwentyOneRules.IsBusted(Dealer.Hand);
                Dealer.Stay = TwentyOneRules.ShouldDealerStay(Dealer.Hand);
            }
            if (Dealer.Stay)
            {
                Console.WriteLine("Dealer is staying.");
            }
            if (Dealer.Bust)
            {
                Console.WriteLine("Dealer Busted!");
                foreach (KeyValuePair<Player, int> entry in Bets)
                {
                    Console.WriteLine("{0} won {1}!", entry.Key.Name, entry.Value);
                    Players.Where(x => x.Name == entry.Key.Name).First().Balance += (entry.Value * 2);
                    Dealer.Balance -= entry.Value;
                }
                return;
            }

            foreach (Player player in Players)
            {
                bool? playerWon = TwentyOneRules.CompareHands(player.Hand, Dealer.Hand); //bool? nullible bool
                if (playerWon == null)
                {
                    Console.WriteLine("Push! No one wins.");
                    player.Balance += Bets[player];                    
                }
                else if (playerWon == true)
                {
                    Console.WriteLine("{0} won {1}!", player.Name, Bets[player]);
                    player.Balance += (Bets[player] * 2);
                    Dealer.Balance -= Bets[player];
                }
                else
                {
                    Console.WriteLine("Dealer Wins {0}", Bets[player]);
                    Dealer.Balance += Bets[player];                    
                }
                Console.WriteLine("Play Again?");
                string answer = Console.ReadLine().ToLower();

                if (answer == "yes" || answer == "y")
                {
                    player.IsPlaying = true;
                    return;
                }
                else
                {
                    player.IsPlaying = false;
                    return;
                }
            }
        }

        public override void ListPlayers()
        {
            Console.WriteLine("Welcome to 21 Players");
            base.ListPlayers();
        }
        public void WalkAway(Player player)
        {
            throw new NotFiniteNumberException();
        }
    }
}
