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
     

        public static List<Train> TrainFromTo(string fromStation, string toStation)
        {


            string json = "";
            string url = $"{APIURL}/schedules?departure_station={fromStation}&arrival_station={toStation}";

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


    }
}
