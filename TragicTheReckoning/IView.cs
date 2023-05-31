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
        public void DisplayAction(int actionResult,Player player, Card card);
    }
}