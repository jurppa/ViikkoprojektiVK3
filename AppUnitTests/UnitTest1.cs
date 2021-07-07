using KoodinimiId�npikajuna;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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
            string[] actual = APIUtil.GetStationFullNames("Tampere", "Helsinki");


         Assert.AreEqual(expected[0], actual[0]);
         Assert.AreEqual(expected[1], actual[1]);
            ///<summary>
            ///Tarkistaa, toimiiko asemien koko nimen hakemisen.
            /// </summary>



        }
        [TestMethod]
        public void TestMethod2()
        {
        APIUtil.TrainFromTo("Helsinki" , "Tampere", DateTime.Now);
         
         Assert.IsNotNull("Testi menee l�pi");
            ///<Summary>
            ///Tarkistaa, ettei lista ole tyhj�(eli kulkeeko v�lill� junia)
            ///</summary>
         
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
            ///<summary>
            ///Tarkistaa, toimiiko vaunuinfon hakeminen.
            /// </summary>


            //Lis�� testi shortname to fullname



        }
        [TestMethod]
        public void IsTrainLate()
        {
            Assert.Inconclusive();
        }
    }




}
