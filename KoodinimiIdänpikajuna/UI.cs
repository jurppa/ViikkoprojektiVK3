﻿using KoodinimiIdänpikajuna.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoodinimiIdänpikajuna
{
    class UI
    {
        
        // Kysyy mistä asemalta minne katsotaan junia =>  mitä tulostetaan vastauksesta
        // 
        public void FromTo()
        {
            Console.WriteLine("Lähtöasema: ");
            var asema1 = Console.ReadLine();
            Console.WriteLine("Pääteasema: ");
            var asema2 = Console.ReadLine();
            Console.WriteLine("Anna päivämäärä(dd.mm.yyyy): ");
            DateTime date = Convert.ToDateTime(Console.ReadLine());

            List<Train> trainsFromTo = APIUtil.TrainFromTo(asema1, asema2);
           
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
            var asema1 = Console.ReadLine();
            DateTime date = DateTime.Now;
        }
        public void TrainInfo()
        {
            Console.WriteLine("Anna junan numero: ");
            var info = Console.ReadLine();

        }
        public void IntermediateStation()
        {
            
        }

    }
}
