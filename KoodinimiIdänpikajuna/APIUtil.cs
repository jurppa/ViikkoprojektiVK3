using KoodinimiIdänpikajuna.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace KoodinimiIdänpikajuna
{
    class APIUtil
    {
        private const string APIURL = "https://rata.digitraffic.fi/api/v1";
        private static readonly HttpClient client = new HttpClient();

        public static async Task ProcessRequests()
        {
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
            new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("accept-encoding", "gzip");
            var response = client.GetAsync($"{APIURL}/metadata/stations").Result;
            string json = response.Content.ReadAsStringAsync().Result;
            List<Station> res = JsonConvert.DeserializeObject<List<Liikennepaikka>>(json);
        }
            


    }
}
