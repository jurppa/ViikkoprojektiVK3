using KoodinimiIdänpikajuna;
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
            string[] actual = APIUtil.GetStationShortNames("Tampere", "Helsinki");


         Assert.AreEqual(expected[0], actual[0]);
         Assert.AreEqual(expected[1], actual[1]);
            ///<summary>
            ///Tarkistaa, toimiiko asemien koko nimen hakemisen.
            /// </summary>

        }

        [TestMethod]
        public void TrackLiveTrainLocationShouldReturnTrue()
        {
            var actual = APIUtil.TrackLiveTrainLocation(150);
            Assert.IsNotNull(actual);

        }
        [TestMethod]
        
        public void GoingThroughShouldReturnTrue()
        {           
            var actual = APIUtil.GoingThrough("Tampere");
            Assert.IsNotNull(actual);

        }

        [TestMethod]
        public void TrainFromTrueShouldntBeNull()
        {
          
         var actual = APIUtil.TrainFromTo("Helsinki" , "Tampere", DateTime.Now);
         
         Assert.IsNotNull(actual);
            ///<Summary>
            ///Tarkistaa, ettei lista ole tyhjä(eli kulkeeko välillä junia)
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


            //Lisää testi shortname to fullname


        }
        [TestMethod]
        public void isTrainReallyLate()
        {

            TimeSpan actual = new TimeSpan(4);
            TimeSpan scheduled = new TimeSpan(4);

            Assert.AreEqual(actual, scheduled);
            
    
            ///<summary
            ///Tämä mittaa, että järjestelmä osaa tunnistaa yhtäsuuruiset ajat(tässä long tickejä)
            ///</summary>
        }


        [TestMethod]
        public void ShortNameToFullNameShouldReturnTrue()
        {
            string actual = APIUtil.ShortNameToFullName("HKI");
            string expected = "Helsinki asema";

            Assert.AreEqual(expected, actual);
        }
    }
    }


