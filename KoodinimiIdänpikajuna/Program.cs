using System;

namespace KoodinimiIdänpikajuna
{
    class Program
    {
        static void Main(string[] args)
        {
            UI ui = new UI();
            //ui.TrainInfo();
            //ui.FromTo();
            // testing Ari
            //Console.WriteLine("Hello World!");
            //var testi = APIUtil.TrainFromTo("TPE", "HKI");

            //string date = "2021.07.05";
            // var testiWagon = APIUtil.WagonInfo(date, 151);

            //Console.WriteLine(testiWagon.catering);
            var testinen = APIUtil.NextDepartingTrain("TPE");
            for (int i = 0; i < testinen.Count; i++)
                Console.WriteLine(testinen[i].timeTableRows[i].scheduledTime);

        }

    
    }
        //testing testing Thien
    
}
