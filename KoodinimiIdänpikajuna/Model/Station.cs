using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoodinimiIdänpikajuna.Model
{
    class Station
    {
        public bool passengerTraffic;//    passengerTraffic: boolean Onko liikennepaikalla kaupallista matkustajaliikennettä
        public string countryCode;//countryCode: string Liikennepaikan maatunnus
        public string stationName;//stationName: string Liikennepaikan nimi
        public string stationShortCode;//stationShortCode: string Liikennepaikan lyhenne
        public int stationUICCode;//stationUICCode: 1-9999   Liikennepaikan maakohtainen UIC-koodi
        public decimal latitude;//latitude: decimal Liikennepaikan latitude "WGS 84"-muodossa
        public decimal longitude;//longitude: decimal Liikennepaikan longitudi "WGS 84"-muodossa
        public string type;//type: string Liikennepaikan tyyppi.STATION = asema, STOPPING_POINT = seisake, TURNOUT_IN_THE_OPEN_LINE = linjavaihde
    }
}
