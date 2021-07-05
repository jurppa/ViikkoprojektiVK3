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

        public static List<Train> TrainFromTo(string fromStation, string toStation)
        {

            string[] statioNames = GetStationFullNames(fromStation, toStation);
            string json = "";
            string url = $"{APIURL}/schedules?departure_station={statioNames[0]}&arrival_station={statioNames[1]}";

            using (var client = new HttpClient(GetZipHandler()))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(url).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                json = responseString;
            }
            var res = JsonConvert.DeserializeObject<List<Train>>(json);

            return res;



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
            
        }
        public static Wagon WagonInfo(DateTime date, int trainNumber)
        {
            string json = "";
            string wagonUrl = WAGONURL + date + @"\" + trainNumber;
            using (var client = new HttpClient(GetZipHandler()))
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(wagonUrl).Result;
                var responseString = response.Content.ReadAsStringAsync().Result;
                json = responseString;
            }

            var res = JsonConvert.DeserializeObject<Wagon>(json);

            return res;
        }






    }
}
