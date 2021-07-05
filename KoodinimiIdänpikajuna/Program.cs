using System;

namespace KoodinimiIdänpikajuna
{
    class Program
    {//testi matias
        static void Main(string[] args)
        {
            // testing Ari
            Console.WriteLine("Hello World!");
            var testi = APIUtil.TrainFromTo("TPE", "HKI");
            foreach (var item in testi)
            {
                Console.WriteLine(item.trainNumber);
            }
            
           

        }
        //testing testing Thien
    }
}
