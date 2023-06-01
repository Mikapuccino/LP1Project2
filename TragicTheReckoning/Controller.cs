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
            int giveUp;
            bool endGame = false;

            view.MainMenu();

            List<Card> field1 = new List<Card>();
            List<Card> field2 = new List<Card>();

            List<Player> players = model.Initialize();

            do
            {

                turn++;
                SetTurnMP(players, turn);
                DrawCard(players);
                giveUp = SpellPhase(players, field1, field2, view);
                AttackPhase(players, field1, field2);
                endGame = CheckEnd(players, giveUp);

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

        public void DrawCard(List<Player> players)
        {
            for (int i = 0; i < 2; i++)
            {
                if (players[i].Hand.Count <= 5)
                {
                    players[i].Hand.Add(players[i].Deck.Pop());
                }
            }
        }

        public int SpellPhase(List<Player> players,
        List<Card> field1, List<Card> field2, IView view)
        {
            int cardChosen;
            int actionResult;
            bool cardPlayed;
            bool invalid = false;

            for (int i = 0; i < 2; i++)
            {
                // When the player has 0 MP,
                // passes to other player or ends the phase
                while (players[i].MP > 0)
                {
                    // ReadLine should go to View, in here for testing
                    cardChosen = view.AskAction(players[i]);

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

                            actionResult = 1;
                            cardPlayed = true;
                        }

                        // WriteLine should go to View, in here for testing
                        else
                        {
                            actionResult = 2;
                            cardPlayed = false;
                        }

                    }

                    // WriteLine should go to View, in here for testing
                    else
                    {
                        actionResult = 3;
                        cardPlayed = false;
                        invalid = true;
                    }

                    if (cardPlayed)
                    {
                        if (i == 0)
                        {
                            view.DisplayAction(actionResult, players[i], field1[field1.Count() - 1]);
                        }
                        else
                        {
                            view.DisplayAction(actionResult, players[i], field2[field2.Count() - 1]);
                        }

                    }
                    else if (!cardPlayed)
                    {
                        view.DisplayAction(actionResult, players[i], players[i].Hand[0]);
                    }
                }
            }

            return 0;
        }

        public void AttackPhase(List<Player> players,
        List<Card> field1, List<Card> field2)
        {
            int cardCount1 = 0;
            int cardCount2 = 0;
            int leftoverDamage = 0;
            int result = 1;

            // While there are cards in each field
            while ((field1.Count != 0) || (field2.Count != 0))
            {
                // Resets cardCounts if they reach the
                // max index of one of the fields
                if (cardCount1 > field1.Count - 1)
                    cardCount1 = 0;

                if (cardCount2 > field2.Count - 1)
                    cardCount2 = 0;

                // Reduces a card's HP depending on
                // the result of the last battle
                switch (result)
                {
                    case 2:
                        field2[cardCount2].DP += leftoverDamage;
                        break;
                    case 3:
                        field1[cardCount1].DP += leftoverDamage;
                        break;
                    default:
                        break;
                }

                result = CardFight(field1[cardCount1], field2[cardCount2]);

                // Sets the leftover damage to be applied to the next
                // card battle and removes destroyed cards from their fields
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

                cardCount1++;
                cardCount2++;
            }

            FinalAttack(players, field1, field2);
        }

        // This method is used to reduce the DP of cards that fight
        // and returns the result of the battle as an int
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

        // This method is used to reduce a player's HP by the AP of all cards
        // left on the opponents field and then clears that field
        // Nothing happens if both fields are empty
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
                field2.Clear();
            }

            else if (field2.Count == 0)
            {
                for (int i = 0; i < field1.Count; i++)
                {
                    finalDamage += field1[i].AP;
                }

                players[1].HP -= finalDamage;
                field1.Clear();
            }
        }

        // This method checks if the game has ended, checking each players HP
        // to see if a player has died and returns true if the game ends
        public bool CheckEnd(List<Player> players, int giveUp)
        {
            // If Player 1 has died or given up, sets HP to 0 and ends the game
            if ((players[0].HP <= 0) || (giveUp == 1))
            {
                players[0].HP = 0;
                return true;
            }

            // If Player 2 has died or given up, sets HP to 0 and ends the game
            else if ((players[1].HP <= 0) || (giveUp == 2))
            {
                players[1].HP = 0;
                return true;
            }

            else if ((players[0].Deck.Count == 0) ||
            (players[1].Deck.Count == 0))
                return true;

            // If no player died, the game keeps going
            else
                return false;
        }
    }
}