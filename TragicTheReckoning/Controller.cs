using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TragicTheReckoning
{
    public class Controller
    {
        /// <summary>
        /// Main method of Controller class, runs the whole game logic
        /// </summary>
        /// <param name="view"> IView object, used to show what is happening
        /// to the user</param>
        /// <param name="model">Model object, used to get the players and their
        /// respective decks and hands</param>
        public void Run(IView view, Model model)
        {
            int turn = 0;
            int giveUp;
            int endGame = 0;

            // Shows the rules and explains how to use the program
            view.MainMenu();

            List<Card> field1 = new List<Card>();
            List<Card> field2 = new List<Card>();

            // Creates players, decks and hands for use in the game
            List<Player> players = model.Initialize();

            do
            {
                turn++;
                SetTurnMP(players, turn);
                DrawCard(players);
                giveUp = SpellPhase(players, field1, field2, view);
                AttackPhase(players, field1, field2, view);
                endGame = CheckEnd(players, giveUp);
            }
            while (endGame > 3);

            // Displays a message saying which player won
            // based on the value of endGame
            view.PlayerWin(players, endGame);
        }

        /// <summary>
        /// Sets the MP for each player based on the turn of the game
        /// If the turn number is lower than 5, MP is equal to the turn number
        /// Otherwise, MP is always 5
        /// </summary>
        /// <param name="players">The players of the game, 
        /// including their hands and decks</param>
        /// <param name="turn">The current turn of the game</param>
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

        /// <summary>
        /// Draws a card to each player's hand
        /// if they have less than 6 cards in hand
        /// </summary>
        /// <param name="players">The players of the game, 
        /// including their hands and decks</param>
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

        /// <summary>
        /// During each turn, the player's take turns playing their cards
        /// until they don't have enough mana to play anything else or they pass
        /// A player can also give up, making the other player win the game
        /// </summary>
        /// <param name="players">The players of the game, 
        /// including their hands and decks</param>
        /// <param name="field1">Field of Player 1, 
        /// where the cards they play go to</param>
        /// <param name="field2">Field of Player 2, 
        /// where the cards they play go to</param>
        /// <param name="view">IView object, used to show what is happening
        /// to the user</param>
        /// <returns>Whether a player gave up or not</returns>
        public int SpellPhase(List<Player> players,
        List<Card> field1, List<Card> field2, IView view)
        {
            int cardChosen;
            int actionResult;
            bool cardPlayed;
            bool validCards;

            for (int i = 0; i < 2; i++)
            {
                validCards = true;
                
                // When the player has 0 MP,
                // passes to other player or ends the phase
                while ((players[i].MP > 0) && validCards)
                {
                    int minimumMana = 5;
                    
                    // Checks every card in the hand for their cost
                    // and saves the lowest cost
                    foreach (Card c in players[i].Hand)
                    {
                        if (minimumMana > c.Cost)
                        {
                            minimumMana = c.Cost;
                        }
                    }

                    // If the lowest card cost in the players hand
                    // is higher than the current MP of the player, 
                    // skips the turn automatically
                    if (players[i].MP < minimumMana)
                    {
                        validCards = false;
                    }
                    
                    // Calls method to check the card the player wants to play
                    cardChosen = view.AskAction(players[i]);

                    // If player choose a card in a valid position in his hand
                    if (cardChosen <= players[i].Hand.Count())
                    {
                        // If the card cost is less or equal
                        // to the player current MP
                        if (players[i].Hand[cardChosen - 1].Cost 
                        <= players[i].MP)
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

                        // The player doesn't have enough MP
                        // to play the chosen card
                        else
                        {
                            actionResult = 2;
                            cardPlayed = false;
                        }
                    }

                    // The player chooses to pass the turn
                    else if (cardChosen == players[i].Hand.Count() + 1)
                    {
                        validCards = false;
                        actionResult = 4;
                        cardPlayed = false;
                    }

                    // The player chooses to give up
                    else if (cardChosen == players[i].Hand.Count() + 2)
                    {
                        validCards = false;
                        actionResult = 5;
                        cardPlayed = false;
                    }

                    // The player chooses a non valid option
                    else
                    {
                        actionResult = 3;
                        cardPlayed = false;
                    }

                    // If a card was played, displays a message saying which
                    // player played the chosen card to their field
                    if (cardPlayed)
                    {
                        if (i == 0)
                        {
                            view.DisplayAction(actionResult, players[i], 
                            field1[field1.Count() - 1]);
                        }
                        
                        else
                        {
                            view.DisplayAction(actionResult, players[i], 
                            field2[field2.Count() - 1]);
                        }

                    }

                    // If no card was played, displays message saying if the
                    // player passed the turn or gave up
                    else if (!cardPlayed)
                    {
                        // If a player gave up, returns int indicating which
                        // player gave up
                        if (actionResult == 5)
                        {
                            view.DisplayAction(actionResult, players[i], 
                            players[i].Hand[0]);
                            
                            if (players[i].Name == "Player 1")
                                return 1;

                            else return 2;
                        }
                        
                        view.DisplayAction(actionResult, players[i], 
                        players[i].Hand[0]);
                    }
                }
            }
            return 0;
        }

        /// <summary>
        /// Cards from both players fight each other, dealing damage and 
        /// destroying the other player's cards
        /// The first card played from one player fights against the first card
        /// played from the other player, repeating until at least one field
        /// is empty
        /// If cards remain in one field while the other is empty, they deal
        /// damage to the other player based on their attack
        /// </summary>
        /// <param name="players">The players of the game, 
        /// including their hands and decks</param>
        /// <param name="field1">Field of Player 1, 
        /// where the cards they play go to</param>
        /// <param name="field2">Field of Player 2, 
        /// where the cards they play go to</param>
        /// <param name="view">IView object, used to show what is happening
        /// to the user</param>
        public void AttackPhase(List<Player> players,
        List<Card> field1, List<Card> field2, IView view)
        {
            int cardCount1 = 0;
            int cardCount2 = 0;
            int leftoverDamage = 0;
            int result = 1;

            // While there are cards in each field
            while ((field1.Count != 0) && (field2.Count != 0))
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

                        if (field2[cardCount2].DP <= 0)
                        {
                            view.FightResult(result, field1[cardCount1], 
                            field2[cardCount2]);
                            field2.RemoveAt(cardCount2);
                        }
                        break;
                    case 3:
                        field1[cardCount1].DP += leftoverDamage;

                        if (field1[cardCount1].DP <= 0)
                        {
                            view.FightResult(result, field1[cardCount1], 
                            field2[cardCount2]);
                            field1.RemoveAt(cardCount1);
                        }
                        break;
                    default:
                        break;
                }

                // If there are still cards left after applying the leftover
                // damage, the two cards next to fight do so
                if ((field1.Count != 0) && (field2.Count != 0))
                {
                    // Gets the result of the fight
                    result = CardFight(field1[cardCount1], field2[cardCount2]);

                    // Shows the cards that fought and the damage they each took
                    view.Fight(field1[cardCount1], field2[cardCount2]);

                    // Shows if one or both cards died in the fight, 
                    // or if both lived
                    view.FightResult(result, field1[cardCount1], 
                    field2[cardCount2]);

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
            }

            // Deals damage to a player if their field is empty and their
            // opponent's isn't
            // Deals no damage if both fields are empty
            FinalAttack(players, field1, field2, view);
        }

        /// <summary>
        /// The two cards given fight, reducing the DP of the other card
        /// by the value of their AP
        /// </summary>
        /// <param name="card1">Card from Player 1</param>
        /// <param name="card2">Card from Player 2</param>
        /// <returns>Result of the fight</returns>
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

        /// <summary>
        /// Reduces a player's HP by the AP of all cards
        /// left on the opponents field and then clears that field
        /// Nothing happens if both fields are empty
        /// </summary>
        /// <param name="players">The players of the game, 
        /// including their hands and decks</param>
        /// <param name="field1">Field of Player 1, 
        /// where the cards they play go to</param>
        /// <param name="field2">Field of Player 2, 
        /// where the cards they play go to</param>
        /// <param name="view">IView object, used to show what is happening
        /// to the user</param>
        public void FinalAttack(List<Player> players,
        List<Card> field1, List<Card> field2, IView view)
        {
            int finalDamage = 0;
            int playerDamaged = 0;

            if (field1.Count == 0)
            {
                playerDamaged = 1;
                for (int i = 0; i < field2.Count; i++)
                {
                    finalDamage += field2[i].AP;
                }

                players[0].HP -= finalDamage;
                if (players[0].HP < 0)
                {
                    players[0].HP = 0;
                }
                field2.Clear();
            }

            else if (field2.Count == 0)
            {
                playerDamaged = 2;
                for (int i = 0; i < field1.Count; i++)
                {
                    finalDamage += field1[i].AP;
                }

                players[1].HP -= finalDamage;
                if (players[1].HP < 0)
                {
                    players[1].HP = 0;
                }
                field1.Clear();
            }
            if (finalDamage == 0)
            {
                playerDamaged = 0;
            }
            
            // Displays the damage each player took, if they took any
            // and shows the current hp of each player
            view.FinalHP(finalDamage, playerDamaged, players);            
        }

        /// <summary>
        /// Checks if the game has ended, checking each players HP
        /// to see if a player has died or if a player gave up
        /// </summary>
        /// <param name="players">The players of the game, 
        /// including their hands and decks</param>
        /// <param name="giveUp">Says if a player gave up
        /// and which one it is</param>
        /// <returns>If the game as ended or not</returns>
        public int CheckEnd(List<Player> players, int giveUp)
        {
            // If Player 1 has died or given up, sets HP to 0 and ends the game
            if ((players[0].HP <= 0) || (giveUp == 1))
            {
                players[0].HP = 0;
                return 1;
            }

            // If Player 2 has died or given up, sets HP to 0 and ends the game
            else if ((players[1].HP <= 0) || (giveUp == 2))
            {
                players[1].HP = 0;
                return 2;
            }

            // If a deck has no cards, the player with the highest HP wins
            else if ((players[0].Deck.Count == 0) ||
            (players[1].Deck.Count == 0))
                return 3;

            // If no player died, the game keeps going
            else
                return 4;
        }
    }
}