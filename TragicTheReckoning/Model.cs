using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TragicTheReckoning
{
    public class Model
    {
        public void Initialize()
        {
            Stack<Card> deck1 = GetDeck();
            Stack<Card> deck2 = GetDeck();
        }

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
            }

            newDeck.Push(new Card("Scorching Heatwave", 4, 5, 3));
            newDeck.Push(new Card("Blind Minotaur", 3, 1, 3));
            newDeck.Push(new Card("Tim, The Wizard", 5, 6, 4));
            newDeck.Push(new Card("Sharply Dressed", 4, 3, 3));

            newDeck = ShuffleDeck(newDeck);

            return newDeck;
        }

        public Stack<Card> ShuffleDeck<Card>(Stack<Card> deck)
        {
            Random rnd = new Random();
            return new Stack<Card>(deck.OrderBy(x => rnd.Next()));
        }
    }
}