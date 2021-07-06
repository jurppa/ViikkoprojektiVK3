using System;

namespace KoodinimiIdänpikajuna
{
    class Program
    {
        static void Main(string[] args)
        {
          
            UI ui = new UI();
            // ui.startMenu();
           // ui.LiveTrain();

            
            var testi = APIUtil.GoingThrough("PRI");
            ui.StartMenu();
            //var testi = APIUtil.TrackLiveTrainLocation(150);
            //Console.WriteLine(testi.nextStation);
            //Console.WriteLine(testi.station);
            //Console.WriteLine(testi.previousStation);
        }

    
    }
}
