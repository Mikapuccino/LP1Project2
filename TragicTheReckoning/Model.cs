using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TragicTheReckoning
{
    public class Model
    {
        /// <summary>
        /// Main method of the class, creates the players, their decks
        /// and their hands
        /// </summary>
        /// <returns>The players ready to play the game</returns>
        public List<Player> Initialize()
        {
            Stack<Card> deck1 = GetDeck();
            Stack<Card> deck2 = GetDeck();

            List<Card> hand1 = InitialHand(deck1);
            List<Card> hand2 = InitialHand(deck2);

            Player player1 = new Player("Player 1", deck1, hand1);
            Player player2 = new Player("Player 2", deck2, hand2);

            List<Player> players = new List<Player>();
            players.Add(player1);
            players.Add(player2);

            return players;
        }

        /// <summary>
        /// Creates every card that will be in each player's deck
        /// and shuffles them
        /// </summary>
        /// <returns>Deck of a player</returns>
        public Stack<Card> GetDeck()
        {
            Stack<Card> newDeck = new Stack<Card>();

            for (int i = 0; i < 4; i++)
            {
                newDeck.Push(new Card("Flying Wand", 1, 1, 1));
                newDeck.Push(new Card("Severed Monkey Head", 1, 2, 1));
            }

            for (int i = 0; i < 2; i++)
            {
                newDeck.Push(new Card("Mystical Rock Wall", 2, 0, 5));
                newDeck.Push(new Card("Lobster McCrabs", 2, 1, 3));
                newDeck.Push(new Card("Goblin Troll", 3, 3, 2));
                newDeck.Push(new Card("Blue Steel", 2, 2, 2));
            }

            newDeck.Push(new Card("Scorching Heatwave", 4, 5, 3));
            newDeck.Push(new Card("Blind Minotaur", 3, 1, 3));
            newDeck.Push(new Card("Tim, The Wizard", 5, 6, 4));
            newDeck.Push(new Card("Sharply Dressed", 4, 3, 3));

            newDeck = ShuffleDeck(newDeck);

            return newDeck;
        }

        /// <summary>
        /// Shuffles the given deck
        /// </summary>
        /// <param name="deck">Deck of a player</param>
        /// <typeparam name="Card">Card class that every card uses</typeparam>
        /// <returns>Shuffled given deck</returns>
        public Stack<Card> ShuffleDeck<Card>(Stack<Card> deck)
        {
            Random rnd = new Random();
            return new Stack<Card>(deck.OrderBy(x => rnd.Next()));
        }

        /// <summary>
        /// Draws six cards from the given deck and adds them to the
        /// hand of a player
        /// </summary>
        /// <param name="deck">Deck of a player</param>
        /// <returns>The initial hand for a player</returns>
        public List<Card> InitialHand(Stack<Card> deck)
        {
            List<Card> initialHand = new List<Card>();
            
            for (int i = 0; i < 6; i++)
            {
                initialHand.Add(deck.Pop());
            }

            return initialHand;
        }
    }
}