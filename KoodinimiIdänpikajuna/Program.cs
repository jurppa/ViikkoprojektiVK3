using System;

namespace KoodinimiIdänpikajuna
{
    class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI();
            ui.startMenu();
            var testi = APIUtil.GoingThrough("PRI");

            foreach (var juna in testi)
            {
                Console.WriteLine(juna.trainNumber + " " + juna.timeTableRows[0].scheduledTime);
            }

        }

    
    }
}
