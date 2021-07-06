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

        public void startMenu()
        {
            Console.WriteLine("1. Juna aikataulut");
            Console.WriteLine("2. Seuraavaksi lähtevä juna");
            Console.WriteLine("3. Tietoa junan palveluista");
            Console.WriteLine("4. Junan välipysäkit: ");
            Console.Write("Valitse toiminta syöttämällä oikea numero: ");

            int input = Int32.Parse(Console.ReadLine());
            UI ui = new UI();

            switch (input)
            {
                case 1:
                    ui.FromTo();
                    break;
                case 2:
                    ui.NextTrain();
                    break;
                case 3:
                    ui.TrainInfo();
                    break;
                case 4:
                    ui.IntermediateStation();
                    break;
                default:
                    Console.WriteLine("Error 404");
                    break;
            }
        }
        
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
          
            DateTime date = DateTime.Now;
            var train = APIUtil.NextDepartingTrain(station1);
            Console.WriteLine();
        }
        public void TrainInfo()
        {
            Console.WriteLine("Anna junan numero: ");
            var trainNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Lähtöpäivämäärä: ");
            var date = Console.ReadLine();
            var wagon = APIUtil.GetWagonInfo(date, trainNumber);

            Console.WriteLine("Vaunun sisältämät palvelut: ");
            Console.WriteLine();
            foreach (KeyValuePair<string, bool>item in wagon)
            {
                Console.WriteLine(item.Key + " " + item.Value);
            }

        }
        public void IntermediateStation()
        {
            
        }
        

    }
}
