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
        public List<Card> Hand { get; set; } = new List<Card>();

        public Player(string name, Stack<Card> deck)
        {
            Name = name;
            Deck = deck;
        }
    }
}