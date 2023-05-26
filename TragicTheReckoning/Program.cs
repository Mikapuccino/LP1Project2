using System;

namespace TragicTheReckoning
{
    class Program
    {
        static void Main(string[] args)
        {
            Controller controller = new Controller();
            Model model = new Model();
            IView view = new View(controller);

            controller.Run(view, model);
        }
    }
}