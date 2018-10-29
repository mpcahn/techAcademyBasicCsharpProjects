using System;
using System.Collections.Generic;
using System.IO;
using Casino;
using Casino.TwentyOne;

namespace TwentyOne
{
    class Program
    {
        static void Main(string[] args)
        {
            const string casinoName = "Cahniferous Casino"; //Can't be changed

            Console.WriteLine("Welcome to the {0}! What's your name?", casinoName);
            string playerName = Console.ReadLine();

            bool validAnswer = false;
            int bank = 0;
            while (!validAnswer)
            {
                Console.WriteLine("How much Money would you like to bet?");
                validAnswer = int.TryParse(Console.ReadLine(), out bank);
                if (!validAnswer) Console.WriteLine("Please enter an integer only");
            }
            
            Console.WriteLine("Hello, {0}. Would you like to join a game of 21?", playerName);
            string answer = Console.ReadLine().ToLower();

            if (answer == "yes" || answer == "y")
            {
                Player player = new Player(playerName, bank);
                player.Id = Guid.NewGuid();
                using (StreamWriter file = new StreamWriter(@"C:\Users\Matt\logs\logs.txt", true))
                {
                    file.WriteLine(DateTime.Now);
                    file.WriteLine(player.Id);
                }
                Game game = new TwentyOneGame();
                game += player;
                player.IsPlaying = true;

                while (player.IsPlaying && player.Balance > 0)
                {
                    try
                    {
                        game.Play();
                    }
                    catch (FraudException)
                    {
                        Console.WriteLine("Security! Kick this person out.");
                        Console.Read();
                        return;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("An error has occurred, please contact your SysAdmin");
                        Console.Read();
                        return;
                    }                    
                }

                game -= player;
                Console.WriteLine("Thank you for playing!");
            }

            Console.WriteLine("See you again soon!");
            Console.Read();
        }
    }
}
