using System;
using Casino;
using Casino.TwentyOne;

namespace TwentyOne
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to C# Twenty One! What's your name?");
            string playerName = Console.ReadLine();
            Console.WriteLine("How much Money would you like to bet?");
            int bank = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Hello, {0}. Would you like to join a game of 21?", playerName);
            string answer = Console.ReadLine().ToLower();

            if (answer == "yes" || answer == "y")
            {
                Player player = new Player(playerName, bank);
                Game game = new TwentyOneGame();
                game += player;
                player.IsPlaying = true;

                while (player.IsPlaying && player.Balance > 0)
                {
                    game.Play();
                }

                game -= player;
                Console.WriteLine("Thank you for playing!");
            }

            Console.WriteLine("Then what are you doing here? Get out before I call security.");
            Console.Read();
        }
    }
}
