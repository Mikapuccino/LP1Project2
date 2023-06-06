using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TragicTheReckoning
{
    public class View : IView
    {
        private readonly Controller controller;

        /// <summary>
        /// Initializes a new instance of the View class.
        /// </summary>
        /// <param name="controller">The controller object.</param>
        public View(Controller controller)
        {
            this.controller = controller;
        }

        /// <summary>
        /// Displays the main menu of the game.
        /// </summary>
        public void MainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Welcome to Tragic: \u001b[31mThe Reckoning!\u001b[37m");
            Console.WriteLine("----");
            Console.WriteLine("Rules:\n");
            Console.WriteLine("- Each player starts with 10 \u001b[32m HP\u001b[37m and 0\u001b[36m MP\u001b[37m.");
            Console.WriteLine("Each deck is comprised of 20 cards that are shuffled at the start of the game.");
            Console.WriteLine("- Players take turns playing cards and attacking.");
            Console.WriteLine("- Cards have a cost \u001b[33m(C)\u001b[37m, attack points \u001b[31m(AP)\u001b[37m, and defense points \u001b[34m(DP)\u001b[37m.");
            Console.WriteLine("You start the game with 6 cards in your hand.");
            Console.WriteLine("- Each turn there are 2 phases, the Spell Phase and the Attack Phase.");
            Console.WriteLine("In the first 4 turns, your amount of mana is correspondent to the turn(turn 1-\u001b[36m mana\u001b[37m = 1, turn 2-\u001b[36m mana\u001b[37m = 2,...) but after turn 5 your max mana will always be 5.");
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

        /// <summary>
        /// Asks the player for their action and returns the chosen action.
        /// </summary>
        /// <param name="player">The current player.</param>
        /// <returns>The chosen action.</returns>
        public int AskAction(Player player)
        {
            Console.WriteLine($"{player.Name}, it's your turn.");
            Console.WriteLine($"Current \u001b[32mHP\u001b[37m: {player.HP}");
            Console.WriteLine($"Current\u001b[36m mana\u001b[37m: {player.MP}");
            Console.WriteLine("Your Hand:\n");

            // Display player's hand with card details
            for (int i = 0; i < player.Hand.Count; i++)
            {
                Card card = player.Hand[i];
                Console.WriteLine($"{i + 1}.{card.Name} |\u001b[33m{card.Cost}\u001b[37m|\u001b[31m{card.AP}\u001b[37m|\u001b[34m{card.DP}\u001b[37m|");
            }
            Console.WriteLine();
            Console.WriteLine($"{player.Hand.Count + 1} Skip turn");
            Console.WriteLine($"{player.Hand.Count + 2} Give up");
            Console.WriteLine();
            Console.WriteLine("Choose a card to play (enter the corresponding number):\n");

            return int.Parse(Console.ReadLine());

        }

        /// <summary>
        /// Displays the result of the player's action.
        /// </summary>
        /// <param name="actionResult">The result of the action.</param>
        /// <param name="player">The current player.</param>
        /// <param name="card">The card played by the player.</param>
        public void DisplayAction(int actionResult, Player player, Card card)
        {
            Console.WriteLine();
            if (actionResult == 1)
            {

                Console.WriteLine($"{player.Name} played \u001b[31;1m{card.Name}\u001b[37m\n");
            }
            if (actionResult == 2)
            {
                Console.WriteLine($"{player.Name} has not enough Mana\n");
            }
            if (actionResult == 3)
            {
                Console.WriteLine($"\u001b[31mInvalid option\u001b[37m\n");
            }
            if (actionResult == 4)
            {
                Console.WriteLine($"{player.Name} skipped a turn\n");
            }
            if (actionResult == 5)
            {
                Console.WriteLine($"{player.Name} has given up\n");
            }
        }

        /// <summary>
        /// Displays the fight between two cards.
        /// </summary>
        /// <param name="card1">The first card.</param>
        /// <param name="card2">The second card.</param>
        public void Fight(Card card1, Card card2)
        {
            Console.WriteLine($"P1: {card1.Name} Fights P2: {card2.Name}");
            Console.WriteLine($"P1 card took {card2.AP} damage and P2 card took {card1.AP} damage");
        }

        /// <summary>
        /// Displays the result of a fight between two cards.
        /// </summary>
        /// <param name="result">The result of the fight.</param>
        /// <param name="card1">The first card.</param>
        /// <param name="card2">The second card.</param>
        public void FightResult(int result, Card card1, Card card2)
        {
            switch (result)
            {
                case 1:
                    Console.WriteLine($"Both cards survived");
                    break;
                case 2:
                    Console.WriteLine($"{card2.Name} has died");
                    break;
                case 3:
                    Console.WriteLine($"{card1.Name} has died");
                    break;
                case 4:
                    Console.WriteLine($"Both cards have died\n");
                    break;
            }

        }

        /// <summary>
        /// Displays the final HP of the players after a fight.
        /// </summary>
        /// <param name="damage">The damage dealt.</param>
        /// <param name="playerDamaged">The player who took damage.</param>
        /// <param name="players">The list of players.</param>
        public void FinalHP(int damage, int playerDamaged, List<Player> players)
        {
            if (playerDamaged == 1)
            {
                Console.WriteLine($"{players[0].Name} took {damage} damage");

            }
            else if (playerDamaged == 2)
            {
                Console.WriteLine($"{players[1].Name} took {damage} damage");
            }
            else
            {
                Console.WriteLine($"No player took damage");
            }
            Console.WriteLine($"{players[0].Name} has {players[0].HP} HP.");
            Console.WriteLine($"{players[1].Name} has {players[1].HP} HP.\n");

        }

        /// <summary>
        /// Displays the winner of the game.
        /// </summary>
        /// <param name="players">The list of players.</param>
        /// <param name="endGame">The end game condition.</param>
        public void PlayerWin(List<Player> players, int endGame)
        {
            if (endGame == 1)
            {
                Console.WriteLine($"\u001b[32m{players[1].Name} won!\u001b[37m");
            }
            if (endGame == 2)
            {
                Console.WriteLine($"{players[0].Name} won!");
            }
            if (endGame == 3)
            {
                if (players[0].HP > players[1].HP)
                {
                    Console.WriteLine($"{players[0].Name} won!");
                }
                else
                {
                    Console.WriteLine($"{players[1].Name} won!");
                }

            }


        }

    }
}
