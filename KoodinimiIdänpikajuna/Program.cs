using System;

namespace KoodinimiIdänpikajuna
{
    class Program
    {//testi matias
        static void Main(string[] args)
        {
            // testing Ari
            Console.WriteLine("Hello World!");
            var testi = APIUtil.ProcessRequests("Tampere", "Helsinki");
            Console.WriteLine(testi.Count);
            foreach (var item in testi)
            {
                Console.WriteLine(testi);
            }

        }
        //testing testing Thien
    }
}
