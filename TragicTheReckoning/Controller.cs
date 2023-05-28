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
                AttackPhase(players, field1, field2);
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

        public void AttackPhase(List<Player> players,
        List<Card> field1, List<Card> field2)
        {
            int cardCount1 = 0;
            int cardCount2 = 0;
            int leftoverDamage = 0;
            int result = 1;
            
            while ((field1.Count != 0) || (field2.Count != 0))
            {
                if (cardCount1 > field1.Count)
                    cardCount1 = 0;

                if (cardCount2 > field2.Count)
                    cardCount2 = 0;

                switch(result)
                {
                    case 1:
                        break;
                    case 2:
                        field2[cardCount2].DP += leftoverDamage;
                        break;
                    case 3:
                        field1[cardCount1].DP += leftoverDamage;
                        break;
                    case 4:
                        break;
                }

                result = CardFight(field1[cardCount1], field2[cardCount2]);

                switch (result)
                {
                    case 1:
                        break;
                    case 2:
                        leftoverDamage = field2[cardCount2].DP;
                        field2.RemoveAt(cardCount2);
                        break;
                    case 3:
                        leftoverDamage = field1[cardCount1].DP;
                        field1.RemoveAt(cardCount1);
                        break;
                    case 4:
                        field1.RemoveAt(cardCount1);
                        field2.RemoveAt(cardCount2);
                        break;
                }

                cardCount1 ++;
                cardCount2 ++;
            }

            FinalAttack(players, field1, field2);
        }

        public int CardFight(Card card1, Card card2)
        {
            card1.DP -= card2.AP;
            card2.DP -= card1.AP;

            if ((card1.DP > 0) && (card2.DP > 0))
                return 1;

            else if ((card1.DP > 0) && (card2.DP <= 0))
                return 2;

            else if ((card1.DP <= 0) && (card2.DP > 0))
                return 3;

            else
                return 4;
        }

        public void FinalAttack(List<Player> players,
        List<Card> field1, List<Card> field2)
        {
            int finalDamage = 0;
            
            if (field1.Count == 0)
            {
                for (int i = 0; i < field2.Count; i++)
                {
                    finalDamage += field2[i].AP;
                }

                players[0].HP -= finalDamage;
            }

            else if (field2.Count == 0)
            {
                for (int i = 0; i < field1.Count; i++)
                {
                    finalDamage += field1[i].AP;
                }

                players[1].HP -= finalDamage;
            }
        }
    }
}