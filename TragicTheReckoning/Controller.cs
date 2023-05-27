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
    }
}