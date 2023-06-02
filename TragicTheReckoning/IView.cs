using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TragicTheReckoning
{
    public interface IView
    {
        void MainMenu();
        int AskAction(Player player);
        void DisplayAction(int actionResult,Player player, Card card);
        void Fight(Card card1, Card card2);
        void FightResult(int result, Card card1, Card card2);
        void FinalHP(int damage, int playerDamaged, List<Player> players);

    }
}