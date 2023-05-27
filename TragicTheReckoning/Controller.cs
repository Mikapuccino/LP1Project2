using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TragicTheReckoning
{
    public class Controller
    {   
        public void Run(IView view, Model model)
        {   
            int turn = 0;
            bool endGame = false;
            List<Card> field1 = new List<Card>();
            List<Card> field2 = new List<Card>();
            
            List<Player> players = model.Initialize();

            do
            {
                turn++;
                SetTurnMP(players, turn);
                SpellPhase(players, field1, field2);
            }
            while (endGame != true);
        }

        public void SetTurnMP(List<Player> players, int turn)
        {
            if (turn < 5)
            {
                for (int i = 0; i < 2; i++)
                {
                    players[i].MP = turn;
                }
            }
            else
            {
                for (int i = 0; i < 2; i++)
                {
                    players[i].MP = 5;
                }
            }
        }

        public void SpellPhase(List<Player> players,
        List<Card> field1, List<Card> field2)
        {
            int cardChosen;
            
            for (int i = 0; i < 2; i++)
            {
                // When the player has 0 MP,
                // passes to other player or ends the phase
                while (players[i].MP > 0)
                {
                    // WriteLine should go to View, in here for testing
                    Console.WriteLine(i + 1);
                    Console.WriteLine(players[i].MP);
                    Console.WriteLine(players[i].Hand.Count());
                    
                    // ReadLine should go to View, in here for testing
                    cardChosen = int.Parse(Console.ReadLine());

                    // If player choose a card in a valid position in his hand
                    if (cardChosen <= players[i].Hand.Count())
                    {
                        // If the card cost is less or equal
                        // to the player current MP
                        if (players[i].Hand[cardChosen - 1].Cost <= players[i].MP)
                        {
                            // Puts the card in the correct field
                            if (i == 0)
                            {
                                field1.Add(players[i].Hand[cardChosen - 1]);
                            }
                            else field2.Add(players[i].Hand[cardChosen - 1]);

                            // Reduces player MP by the cost of the card played
                            players[i].MP -= 
                            players[i].Hand[cardChosen - 1].Cost;

                            // Removes card from the hand
                            players[i].Hand.RemoveAt(cardChosen - 1);
                        }

                        // WriteLine should go to View, in here for testing
                        else Console.WriteLine("Not enough mana");
                    }

                    // WriteLine should go to View, in here for testing
                    else Console.WriteLine("Not a valid option");
                }
            }
        }
    }
}