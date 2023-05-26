using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TragicTheReckoning
{
    public class Card
    {
        public string Name { get; }
        public int Cost { get; }
        public int AP { get; }
        public int DP { get; set; }

        public Card(string name, int cost, int AP, int DP)
        {
            Name = name;
            Cost = cost;
            this.AP = AP;
            this.DP = DP;
        }
    }
}