using KoodinimiIdänpikajuna.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;

namespace KoodinimiIdänpikajuna
{
    public class APIUtil
    {
        private const string allStations = "https://rata.digitraffic.fi/api/v1/metadata/stations";

        public static string CreateClient(string url)
        {
            string json = "";
            using (var client = new HttpClient(GetZipHandler()))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(url).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                json = responseString;
            }

            return json;
        }

        public static List<Train> TrainFromTo(string fromStation, string toStation, DateTime dt)
        {
            string[] stationNames = new string[2];
            if (fromStation.Length == 3 && toStation.Length == 3) { stationNames[0] = fromStation.ToUpper(); stationNames[1] = toStation.ToUpper(); }
            else
            {
                stationNames = GetStationShortNames(fromStation, toStation);
            }

            string url = @"https://rata.digitraffic.fi/api/v1/live-trains/station/" + stationNames[0] + @"/" + stationNames[1] + "?departure_date=" + dt.ToString("yyyy-MM-dd") + "&include_nonstopping=false";
            string json = CreateClient(url);
            


            try
            {
                var res = JsonConvert.DeserializeObject<List<Train>>(json);

                var trains = res.Where(x => x.timeTableRows[0].scheduledTime.ToLocalTime() > DateTime.Now.ToLocalTime())
          .OrderBy(x => x.timeTableRows[0].scheduledTime).ToList();

                return trains;
            }
            catch (Exception)
            {

                UI ui = new UI();

                ui.hasFaultInINput();

                ui.StartMenu();
            }

            return new List<Train>();
            // trains[i].timetablerow[vika].scheduled time - trains[i].timetablerow[0].Scheduled time
            /// <summary>
            ///TrainFromTo palauttaa Junan reitin asemalta A, asemalle B.
            /// </summary>




        }

        private static HttpClientHandler GetZipHandler()
        {
            return new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
        }

        public static string[] GetStationShortNames(string shortNameOne, string shortNameTwo = "Helsinki")
        {
            if (shortNameOne.Length < 2 || shortNameTwo.Length < 2)

            {
                Console.WriteLine("Tarkista asemien nimet.");
                return new string[0];
            }
            if(shortNameOne.Length == 3)
            {
                return new string[] { shortNameOne };
            }

            string[] nameOneSplitted = shortNameOne.Split(" ");
            string[] nameTwoSplitted = shortNameTwo.Split(" ");
            string json = CreateClient(allStations);


            var res = JsonConvert.DeserializeObject<List<Station>>(json);
            try
            {
                var stationOne = res.First(x => x.stationName.ToLower().Contains(nameOneSplitted[0].ToLower()));
                var stationTwo = res.First(x => x.stationName.ToLower().Contains(nameTwoSplitted[0].ToLower()));
                string[] shortNames = { stationOne.stationShortCode, stationTwo.stationShortCode };
                return shortNames;

            }
            catch (Exception e)
            {

                UI ui = new UI();
               
                ui.hasFaultInINput();
               
                ui.StartMenu();
            }
           
           
            return new string[] { "0"};


            

            /// <summary>
            /// GetStationFullName palauttaa kahden aseman(lähtö-määränpäät) nimet jos ei tiedä lyhennettä. Koodi ottaa huomioon, että jos asema on esimerkiksi Helsinki Tavara, niin sitä voi hakea pelkällä Helsinki:llä.
            /// </summary>

        }
        public static List<Train> GoingThrough(string stationName)
        {
            if(stationName.Length < 3)
            {
                stationName = GetStationShortNames(stationName).First();
            }
            string goingThroughUrl = "https://rata.digitraffic.fi/api/v1/live-trains/station/" + stationName + "?departing_trains=5";
            string json = CreateClient(goingThroughUrl);

            
            var res = JsonConvert.DeserializeObject<List<Train>>(json);
            var nextTrainsGoingThrough = res.OrderByDescending(x => x.timeTableRows[0].scheduledTime).Take(5).ToList();   //.Where(x => x.timeTableRows[0].scheduledTime > DateTime.Now).ToList(); 
            return nextTrainsGoingThrough;
            /// <summary>
            /// GoingThroughissa haetaan jonkin tietyn aseman kautta kulkevat junat, palauttaa kaksi junaa.
            /// </summary>
        }


        public static Dictionary<string, bool> GetWagonInfo(DateTime date, int trainNumber)
        {
            Dictionary<string, bool> servicesInWagons = new Dictionary<string, bool>();
            string url = @"https://rata.digitraffic.fi/api/v1/compositions/" + date.ToString("yyyy-MM-dd") + @"/" + trainNumber;
            string json = CreateClient(url);


            var res = JsonConvert.DeserializeObject<Train>(json);
            try
            {
                var serviceWagons = res.journeySections[0].wagons;

                foreach (var item in serviceWagons)
                {
                    if (item.catering == true) { servicesInWagons["Catering"] = true; }
                    if (item.luggage == true) { servicesInWagons["Matkatavarat"] = true; }
                    if (item.playground == true) { servicesInWagons["Lasten leikkialue"] = true; }
                    if (item.smoking == true) { servicesInWagons["Tupakointi"] = true; }
                    if (item.pet == true) { servicesInWagons["Lemmikkipaikka"] = true; }

                }
                return servicesInWagons;
            }
            catch (Exception)
            {

                UI ui = new UI();

                ui.hasFaultInINput();

                ui.StartMenu();
            }

            return new Dictionary<string, bool>();
            /// <summary>
            /// servicesInWagons palauttaa käyttäjälle hänen hakuehtojen mukaisen junan jos käyttäjällä toiveissa esimerkiksi lemmikkivaunu.
            /// </summary>
        }

        public static Location TrackLiveTrainLocation(int trainNumber)

        {
            string url = @"https://rata.digitraffic.fi/api/v1/train-tracking/latest/" + trainNumber;
            string json = CreateClient(url);


            var res = JsonConvert.DeserializeObject<List<Location>>(json);
            try
            {
            var whereIsTrainAt = res.First();
            Console.WriteLine(whereIsTrainAt.station);
            return whereIsTrainAt;

            }
            catch (Exception)
            {
                UI ui = new UI();

                ui.hasFaultInINput();

                ui.StartMenu();
                
            }

            return new Location();
        
            /// <summary>
            /// TracLiveTrainLocation palauttaa junan numerolla junan sijainnin.
            /// </summary>
        }


        public static string ShortNameToFullName(string shortNameOne)
        {
            if (shortNameOne.Length < 2)

            {
                Console.WriteLine("Tarkista asemien nimet.");
                return "";
            }

            string json = CreateClient(allStations);


            var res = JsonConvert.DeserializeObject<List<Station>>(json);
            var stationOne = res.First(x => x.stationShortCode.Contains(shortNameOne));
          

            string fullNames = stationOne.stationName;


            return fullNames;
            ///<summary>
            ///Palauttaa lyhenteen nimen kokonimenä.
            ///</summary>
        }
       
        public static string IsTrainLate(DateTime actual, DateTime scheduled)
        
        
        {
            string minutesLate = "";
            TimeSpan ts = new TimeSpan();
            if(actual > scheduled )
            {
                Console.WriteLine("Minuuttia myöhässä:");
                ts = actual - scheduled; minutesLate = ts.ToString(); return minutesLate;
            }
            else { return "Aikataulussa"; }

        }
        /// <summary>
        ///  Tarkistaa onko juna ollut myöhässä, vähentää todellisesta ajasta ajastetun ajan
        /// </summary>
        /// <param name="actual"></param>
        /// <param name="scheduled"></param>
        /// <returns></returns>



    }
}

