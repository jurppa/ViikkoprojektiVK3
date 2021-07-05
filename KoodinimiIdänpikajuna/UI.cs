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
            Console.WriteLine("Mistä asemalta minne mennään? Käytä lyhenteitä, esim TPE = Tampere HKI = Helsinki");
            var asema1 = Console.ReadLine();
            var asema2 = Console.ReadLine();

            List<Train> junatASemaltaAemalle = APIUtil.TrainFromTo(asema1, asema2);


            
        }

    }
}
