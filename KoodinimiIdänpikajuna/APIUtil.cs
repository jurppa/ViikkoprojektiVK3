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
        private const string APIURL = "https://rata.digitraffic.fi/api/v1";
        private const string allStations = "https://rata.digitraffic.fi/api/v1/metadata/stations";
        private const string WAGONURL = "https://rata.digitraffic.fi/api/v1/compositions/";
        private const string GOINGTHROUGHURL = "https://rata.digitraffic.fi/api/v1/live-trains/station";

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
                stationNames = GetStationFullNames(fromStation, toStation);
            }
            //string url = $"{APIURL}/schedules?departure_station={stationNames[0]}&arrival_station={stationNames[1]}";

            string url = @"https://rata.digitraffic.fi/api/v1/live-trains/station/" + stationNames[0] + @"/" + stationNames[1] + "?departure_date=" + dt.ToString("yyyy-MM-dd") + "&include_nonstopping=false";
            string json = CreateClient(url);
           

            var res = JsonConvert.DeserializeObject<List<Train>>(json);
            var trains = res.Where(x => x.timeTableRows[0].scheduledTime.ToLocalTime() > DateTime.Now.ToLocalTime())
            .OrderBy(x => x.timeTableRows[0].scheduledTime).ToList();
            return trains;

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

        public static string[] GetStationFullNames(string shortNameOne, string shortNameTwo = "Helsinki")
        {
            if (shortNameOne.Length < 2 || shortNameTwo.Length < 2)

            {
                Console.WriteLine("Tarkista asemien nimet.");
                return new string[0];
            }


            string[] nameOneSplitted = shortNameOne.Split(" ");
            string[] nameTwoSplitted = shortNameTwo.Split(" ");
            string json = CreateClient(allStations);


            var res = JsonConvert.DeserializeObject<List<Station>>(json);
            var stationOne = res.First(x => x.stationName.ToLower().Contains(nameOneSplitted[0].ToLower()));
            var stationTwo = res.First(x => x.stationName.ToLower().Contains(nameTwoSplitted[0].ToLower()));

            string[] shortNames = { stationOne.stationShortCode, stationTwo.stationShortCode };


            return shortNames;
            /// <summary>
            /// GetStationFullName palauttaa kahden aseman(lähtö-määränpäät) nimet jos ei tiedä lyhennettä. Koodi ottaa huomioon, että jos asema on esimerkiksi Helsinki Tavara, niin sitä voi hakea pelkällä Helsinki:llä.
            /// </summary>

        }
        public static List<Train> GoingThrough(string stationName)
        {
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
            /// <summary>
            /// servicesInWagons palauttaa käyttäjälle hänen hakuehtojen mukaisen junan jos käyttäjällä toiveissa esimerkiksi lemmikkivaunu.
            /// </summary>
        }

        public static Location TrackLiveTrainLocation(int trainNumber)

        {
            string url = @"https://rata.digitraffic.fi/api/v1/train-tracking/latest/" + trainNumber;
            string json = CreateClient(url);


            var res = JsonConvert.DeserializeObject<List<Location>>(json);
            var whereIsTrainAt = res.First();
            Console.WriteLine(whereIsTrainAt.station);
            return whereIsTrainAt;
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
        
        public static int IsTrainLate(DateTime actual, DateTime scheduled)
        
        
        {
            int minutesLate = 0;
            TimeSpan ts = new TimeSpan();
            if(actual > scheduled )
            { 
                ts = actual - scheduled; minutesLate = Convert.ToInt32(ts); return minutesLate;
            }
            else { return 0; }

        }







    }
}

