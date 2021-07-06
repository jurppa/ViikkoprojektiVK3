using KoodinimiIdänpikajuna.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KoodinimiIdänpikajuna
{
    class APIUtil
    {
        private const string APIURL = "https://rata.digitraffic.fi/api/v1";
        private const string allStations = "https://rata.digitraffic.fi/api/v1/metadata/stations";
        private const string WAGONURL = "https://rata.digitraffic.fi/api/v1/compositions/";
        private const string GOINGTHROUGHURL = "https://rata.digitraffic.fi/api/v1/live-trains/station";

        public static List<Train> TrainFromTo(string fromStation, string toStation)
        {
            string[] stationNames = new string[2];
            if (fromStation.Length == 3 && toStation.Length == 3) { stationNames[0] = fromStation; stationNames[1] = toStation; }
            else { 
            stationNames = GetStationFullNames(fromStation, toStation);
            }
            string json = "";
            string url = $"{APIURL}/schedules?departure_station={stationNames[0]}&arrival_station={stationNames[1]}";

            using (var client = new HttpClient(GetZipHandler()))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(url).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                json = responseString;
            }
            var res = JsonConvert.DeserializeObject<List<Train>>(json);

            return res;

            /// <summary>
            ///TrainFromTo palauttaa Junan reitin asemalta A, asemalle B.
            /// <summary>




        }

        private static HttpClientHandler GetZipHandler()
        {
            return new HttpClientHandler()
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate
            };
        }

        public static string[] GetStationFullNames(string shortNameOne, string shortNameTwo)
        {
            string json = "";
            string[] nameOneSplitted = shortNameOne.Split(" ");
            string[] nameTwoSplitted = shortNameTwo.Split(" ");


            using (var client = new HttpClient(GetZipHandler()))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(allStations).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                json = responseString;
            }
            var res = JsonConvert.DeserializeObject<List<Station>>(json);
            var stationOne = res.First(x => x.stationName.Contains(nameOneSplitted[0]));
            var stationTwo = res.First(x => x.stationName.Contains(nameTwoSplitted[0]));

            string[] shortNames = { stationOne.stationShortCode, stationTwo.stationShortCode };


            return shortNames;
            /// <summary>
            /// GetStationFullName palauttaa kahden aseman(lähtö-määränpäät) nimet jos ei tiedä lyhennettä. Koodi ottaa huomioon, että jos asema on esimerkiksi Helsinki Tavara, niin sitä voi hakea pelkällä Helsinki:llä.
            /// <summary>

        }
        public static List<Train> GoingThrough(string stationName)
        {
            string json = "";
            string goingThroughUrl = "https://rata.digitraffic.fi/api/v1/live-trains/station/"+stationName + "?departing_trains=5";
            using (var client = new HttpClient(GetZipHandler()))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(goingThroughUrl).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                json = responseString;
            }
            var res = JsonConvert.DeserializeObject<List<Train>>(json);
            var nextTrainsGoingThrough = res.OrderByDescending(x =>x.timeTableRows[0].scheduledTime).Take(2).ToList();   //.Where(x => x.timeTableRows[0].scheduledTime > DateTime.Now).ToList(); 

            return nextTrainsGoingThrough;
            /// <summary>
            /// GoingThroughissa haetaan jonkin tietyn aseman kautta kulkevat junat, palauttaa kaksi junaa.
            /// <summary>
        }


        public static List<Train> NextDepartingTrain(string stationName)
        {
            string json = "";
            string nextDepartureUrl = @"https://rata.digitraffic.fi/api/v1/live-trains/station/" + stationName + "?arrived_trains=5&arriving_trains=5";
            using (var client = new HttpClient(GetZipHandler()))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(nextDepartureUrl).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                        json = responseString;
            }

            var res = JsonConvert.DeserializeObject<List<Train>>(json);
            DateTime now = DateTime.Now.ToLocalTime();

            var nextDepartingTrain = res.Where(x => x.timeTableRows[0].scheduledTime > now);

            return res;
            /// <summary>
            /// NextDepartingtrain palauttaa seuraavan tulevan junan kun syöttää aseman nimen.
            /// <summary>
        }

        public static Dictionary<string, bool> GetWagonInfo(DateTime date, int trainNumber)
        {
            Dictionary<string, bool> servicesInWagons = new Dictionary<string, bool>();
            string json = "";
            string url = @"https://rata.digitraffic.fi/api/v1/compositions/" + date.ToString("yyyy-MM-dd") + @"/" + trainNumber;

            using (var client = new HttpClient(GetZipHandler()))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(url).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                json = responseString;
            }
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
            /// <summary>
        }

        public static Location TrackLiveTrainLocation(int trainNumber)

        {
            string json = "";
            string url = @"https://rata.digitraffic.fi/api/v1/train-tracking/latest/" + trainNumber;

            using (var client = new HttpClient(GetZipHandler()))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(url).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                json = responseString;
            }
            var res = JsonConvert.DeserializeObject<List<Location>>(json);
            var whereIsTrainAt = res.First();

                return whereIsTrainAt;
        }
       







    }
}
    
