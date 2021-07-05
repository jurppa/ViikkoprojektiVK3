using System;

namespace KoodinimiIdänpikajuna
{
    class Program
    {
        static void Main(string[] args)
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
                    Console.WriteLine("Cannot be found");
                    break;
            }
            ui.TrainInfo();
           

        }

    
    }
        //testing testing Thien
    
}
