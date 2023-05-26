using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TragicTheReckoning
{
    public class Player
    {
        public string Name { get; }
        public int HP { get; }
        public int MP { get; }
        public Stack<Card> Deck { get; set; } = new Stack<Card>();
        public List<Card> Hand { get; set; } = new List<Card>();

        public Player(string name, int HP, int MP,
        Stack<Card> deck, List<Card> hand)
        {
            Name = name;
            this.HP = HP;
            this.MP = MP;
            Deck = deck;
            Hand = hand;
        }
    }
}