using KoodinimiIdänpikajuna.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoodinimiIdänpikajuna
{
    class UI
    {

        public void StartMenu()
        {
            Console.WriteLine("1. Juna aikataulut");
            Console.WriteLine("2. Tietoa junan palveluista");
            Console.WriteLine("3. Junan välipysäkit");
            Console.WriteLine("4. Junan nykyinen sijainti");
            Console.Write("Valitse toiminta syöttämällä oikea numero: ");

            var input = Console.ReadLine();
            int inputToInt = Int32.Parse(input);
            UI ui = new UI();

            while (input != null)
            {
                switch (inputToInt)
                {
                    case 1:
                        ui.FromTo();
                        break;
                    case 2:
                        ui.TrainInfo();
                        break;
                    case 3:
                        ui.IntermediateStation();
                        break;
                    case 4:
                        ui.LiveTrain();
                        break;
                }
            }
        }
        
        /// <summary>
        /// Tämä metodi kysytään lähtöaseman, pääteaseman, ja päivämäärän jonka jälkeen tulostetaan lista lähtevistä junista.
        /// </summary>
        
        public void FromTo()
        {
            Console.WriteLine("Lähtöasema: ");
            var station1 = Console.ReadLine();
            Console.WriteLine("Pääteasema: ");
            var station2 = Console.ReadLine();
            Console.WriteLine("Anna päivämäärä(dd.mm.yyyy): ");
            DateTime date = DateTime.Parse(Console.ReadLine());

            List<Train> trainsFromTo = APIUtil.TrainFromTo(station1, station2);
            for (int i = 0; i < trainsFromTo.Count; i++)
            {
                if (trainsFromTo[i].timeTableRows[i].type == "ARRIVAL") { continue; }

                Console.WriteLine();
                Console.WriteLine(trainsFromTo[i].trainType + " " + trainsFromTo[i].trainNumber + " | " + trainsFromTo[i].timeTableRows[i].type + " | " + trainsFromTo[i].timeTableRows[i].scheduledTime);
                                
            }

        }
        public void NextTrain()
        {
            Console.WriteLine("Lähtöasema: ");
            var station1 = Console.ReadLine();
          
            DateTime date = DateTime.Now;
            var train = APIUtil.NextDepartingTrain(station1);
            Console.WriteLine();
        }
        public void TrainInfo()
        {
            Console.WriteLine("Anna junan numero: ");
            var trainNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Lähtöpäivämäärä: ");
            var date = Convert.ToDateTime( Console.ReadLine());
            var wagon = APIUtil.GetWagonInfo(date, trainNumber);
            Console.WriteLine();
            Console.WriteLine("Vaunun sisältämät palvelut: ");
            Console.WriteLine();
            foreach (KeyValuePair<string, bool>item in wagon)
            {
                Console.WriteLine(item.Key);
            }

        }
        public void IntermediateStation()
        {
            
        }
        public void LiveTrain()
        {
            Console.WriteLine("Anna Junan numero: ");
            int tnumber = Convert.ToInt32(Console.ReadLine());

            var live = APIUtil.TrackLiveTrainLocation(tnumber);
            Console.WriteLine(live.station);
        }
        

    }
}
