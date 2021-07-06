using Microsoft.VisualStudio.TestTools.UnitTesting;
using KoodinimiIdänpikajuna;
using System;
using System.IO;
using System.Collections.Generic;
namespace AppUnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AreNamesEqualShouldReturnTrue()
        {

         string[] expected = { "TPE", "HKI" };
         string[] actual = APIUtil.GetStationFullNames("Tampere","Helsinki");
        

         Assert.AreEqual(expected[0], actual[0]);
         Assert.AreEqual(expected[1], actual[1]);
    


        }
        [TestMethod]
        public void TestMethod2()
        {
        APIUtil.TrainFromTo("Helsinki" , "Tampere");
        

        }

        [TestMethod]
        public void WagonInfoShouldReturnTrue()
        {
            Dictionary<string, bool> servicesInWagons = new Dictionary<string, bool>();
            servicesInWagons.Add("Catering", true);
            servicesInWagons.Add("Lasten leikkialue", true);
       
            servicesInWagons.Add("Tupakointi", false);
            servicesInWagons.Add("Lemmikkipaikka", true);


            Dictionary<string, bool> actualServicesInWagons = APIUtil.GetWagonInfo(DateTime.Now.Date, 150);

            Assert.AreEqual(servicesInWagons["Catering"], actualServicesInWagons["Catering"]);
            Assert.AreEqual(servicesInWagons["Lasten leikkialue"], actualServicesInWagons["Lasten leikkialue"]);
         
            
            Assert.AreEqual(servicesInWagons["Lemmikkipaikka"], actualServicesInWagons["Lemmikkipaikka"]);






        }
    }

}
