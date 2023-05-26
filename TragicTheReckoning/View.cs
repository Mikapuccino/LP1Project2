using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TragicTheReckoning
{
    public class View : IView
    {
        private readonly Controller controller;

        public View(Controller controller)
        {
            this.controller = controller;
        }
    }
}