using System;

namespace KoodinimiIdänpikajuna
{
    class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI();

            ui.FromTo();
            // testing Ari
            //Console.WriteLine("Hello World!");
            var testi = APIUtil.TrainFromTo("TPE", "HKI");

            //for (int i = 0; i < testi.Count; i++)
            //{
            //    if (testi[i].timeTableRows[i].type == "ARRIVAL") { continue; }

            //    Console.WriteLine(testi[i].timeTableRows[i].type);
            //    Console.WriteLine(testi[i].timeTableRows[i].scheduledTime);
            //    Console.WriteLine(testi[i].runningCurrently);
            //    Console.WriteLine(testi[i].trainNumber);

            //}

            //Console.WriteLine( APIUtil.GetStationFullNames("Tampere", "Helsinki"));




        }
        //testing testing Thien
    }
}
