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
                while (players[i].MP > 0)
                {
                    // ReadLine should go to View, in here for testing
                    cardChosen = int.Parse(Console.ReadLine());

                    if (players[i].Hand[cardChosen - 1].Cost <= players[i].MP)
                    {
                        if (i == 0)
                        {
                            field1.Add(players[i].Hand[cardChosen - 1]);
                        }
                        else field2.Add(players[i].Hand[cardChosen - 1]);

                        players[i].Hand.RemoveAt(cardChosen - 1);
                        players[i].MP -= players[i].Hand[cardChosen - 1].Cost;
                    }
                }
            }
        }
    }
}