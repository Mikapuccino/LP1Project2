using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TragicTheReckoning
{
    public class View : IView
    {
        private readonly Controller controller;

        public View(Controller controller)
        {
            this.controller = controller;
        }

        public void MainMenu()
        {
            Console.WriteLine("Welcome to Tragic: The Reckoning!");
            Console.WriteLine("----");
            Console.WriteLine("Rules:");
            Console.WriteLine("- Each player starts with 10 HP and 0 MP.");
            Console.WriteLine("Each deck is comprised of 20 cards that are shuffled at the start of the game.");
            Console.WriteLine("- Players take turns playing cards and attacking.");
            Console.WriteLine("- Cards have a cost \u001b[33m(C)\u001b[37m, attack points \u001b[31m(AP)\u001b[37m, and defense points \u001b[34m(DP)\u001b[37m.");
            Console.WriteLine("You start the game with 6 cards in your hand.");
            Console.WriteLine("- Each turn there are 2 phases, the Spell Phase and the Attack Phase.");
            Console.WriteLine("In the first 4 turns, your amount of mana is correspondent to the turn(turn 1- mana=1, turn 2- mana=2,...) but after turn 5 your max mana will always be 5.");
            Console.WriteLine("- To play a card, enter the number corresponding to the card in your hand.");
            Console.WriteLine("- The card's cost must be less than or equal to your current MP (Mana Points).");
            Console.WriteLine("- After playing cards, you enter the attack phase.");
            Console.WriteLine("- Each player's cards on the field will fight against each other.");
            Console.WriteLine("- The card with higher AP will reduce the opponent card's DP.");
            Console.WriteLine("- If a card's DP reaches 0 or below, it's destroyed.");
            Console.WriteLine("- If a player has no cards on the field, the opponent's cards directly attack the player.");
            Console.WriteLine("- The game ends when a player's HP reaches 0 or all cards in the deck are used.\n");
            Console.WriteLine("Good luck, have fun!");
            Console.WriteLine();

        }

        public int AskAction(Player player)
        {
            Console.WriteLine($"{player.Name}, it's your turn.");
            Console.WriteLine($"Current HP: {player.HP}");
            Console.WriteLine($"Current mana: {player.MP}");
            Console.WriteLine("Your Hand:\n");

            // Display player's hand with card details
            for (int i = 0; i < player.Hand.Count; i++)
            {
                Card card = player.Hand[i];
                Console.WriteLine($"{i + 1}.{card.Name} |\u001b[33m{card.Cost}\u001b[37m|\u001b[31m{card.AP}\u001b[37m|\u001b[34m{card.DP}\u001b[37m|");
            }
            Console.WriteLine();
            Console.WriteLine("Choose a card to play (enter the corresponding number):\n");

            return int.Parse(Console.ReadLine());

        }
        public void DisplayAction(int actionResult, Player player, Card card)
        {
            Console.WriteLine();
            if (actionResult == 1)
            {
                
                Console.WriteLine($"{player.Name} played \u001b[32m{card.Name}\u001b[37m\n");
            }
            if (actionResult == 2)
            {
                Console.WriteLine($"{player.Name} has not enough Mana\n");
            }
            if (actionResult == 3)
            {
                Console.WriteLine($"\u001b[31mInvalid option\u001b[37m\n");
            }
        }

        public void Fight()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
