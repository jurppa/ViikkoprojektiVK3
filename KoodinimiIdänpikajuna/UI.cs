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
        
        /// <summary>
        /// FromTo() Metodissa kysytään lähtö, pääteasema, ja päivämäärä jonka jälkeen tulostetaan lista lähtevistä junista.
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
                Console.WriteLine(trainsFromTo[i].trainType + " " + trainsFromTo[i].trainNumber);
                Console.WriteLine(trainsFromTo[i].timeTableRows[i].type);
                Console.WriteLine(trainsFromTo[i].timeTableRows[i].scheduledTime);


            }

        }
        public void NextTrain()
        {
            

            Console.WriteLine("Lähtöasema: ");
            var station1 = Console.ReadLine();
          //  List<Train> trainsFrom = APIUtil.TrainFromTo(station1);
            DateTime date = DateTime.Now;
        }
        public void TrainInfo()
        {
            Console.WriteLine("Anna junan numero: ");
            var trainNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Lähtöpäivämäärä: ");
            var date = Console.ReadLine();

            APIUtil.WagonInfo(date, trainNumber);


        }
        public void IntermediateStation()
        {
            
        }

    }
}
