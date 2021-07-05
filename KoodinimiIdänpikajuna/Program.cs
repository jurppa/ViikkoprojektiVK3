using System;

namespace KoodinimiIdänpikajuna
{
    class Program
    {
        static void Main(string[] args)
        {
            // testing Ari
            Console.WriteLine("Hello World!");
            var testi = APIUtil.TrainFromTo("TPE", "HKI");
            for(int i = 0; i < testi.Count; i++)
            {
                Console.WriteLine(testi[i].timeTableRows[i].type);
                Console.WriteLine(testi[i].runningCurrently);
                Console.WriteLine(testi[i].trainNumber);

            }





        }
        //testing testing Thien
    }
}
