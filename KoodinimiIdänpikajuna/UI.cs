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
        
        // Kysyy mistä asemalta minne katsotaan junia =>  mitä tulostetaan vastauksesta
        // 
        public void MistäMinne()
        {
            Console.WriteLine("Lähtöasema: ");
            var asema1 = Console.ReadLine();
            Console.WriteLine("Pääteasema: ");
            var asema2 = Console.ReadLine();

            List<Train> junatASemaltaAemalle = APIUtil.TrainFromTo(asema1, asema2);


            
        }

    }
}
