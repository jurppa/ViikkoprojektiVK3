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

           



        }
        //testing testing Thien
    }
}
