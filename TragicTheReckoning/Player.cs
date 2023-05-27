using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TragicTheReckoning
{
    public class Player
    {
        public string Name { get; }
        public Stack<Card> Deck { get; set; }
        public int HP { get; } = 10;
        public int MP { get; } = 0;
        public List<Card> Hand { get; set; }

        public Player(string name, Stack<Card> deck, List<Card> hand)
        {
            Name = name;
            Deck = deck;
            Hand = hand;
        }
    }
}