using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RataDigiTraffic.Model
{
    public class Juna
    {
        public int trainNumber;//; // 1-99999  Junan numero.Esim junan "IC 59" junanumero on 59
        public DateTime departureDate; // date Junan ensimmäisen lähdön päivämäärä
        public int operatorUICCode; // 1-9999   Junan operoiman operaattorin UIC-koodi
        public string operatorShortCode; // vr, vr-track, destia, ...  Lista operaattoreista löytyy täältä.
        public string trainType; // IC, P, S, ...
        public string timetableType; // REGULAR tai ADHOC.Kertoo onko junan aikataulun ratakapasiteetti haettu säännöllisenä (REGULAR) vai kiireellisenä yksittäistä päivää koskevana(ADHOC).
        public string trainCategory; // lähiliikenne, kaukoliikenne, tavaraliikenne, ...
        public string commuterLineID; // Z, K, N....
        public bool runningCurrently; // true/false  Onko juna tällä hetkellä kulussa
        public bool cancelled; // true/false    Totta, jos junan peruminen on tehty 10 vuorokauden sisällä.Yli 10 vuorokautta sitten peruttuja junia ei palauteta rajapinnassa laisinkaan.
        public long version; // positive integer   Versionumero, jossa juna on viimeksi muuttunut
        public List<Aikataulurivi> timeTableRows;//  Kuvaa saapumisia ja lähtöjä liikennepaikoilta.Järjestetty reitin mukaiseen järjestykseen.
    }

    public class Aikataulurivi
    {
        public bool trainStopping;
        public string stationShortCode; // string Aseman lyhennekoodi
        public int stationcUICCode; // 1-9999  Aseman UIC-koodi
        public string countryCode; // "FI" tai "RU"
        public string type; // "ARRIVAL" tai "DEPARTURE"  Pysähdyksen tyyppi
        public bool commercialStop; // boolean Onko pysähdys kaupallinen. Annettu vain pysähdyksille, ei läpiajoille. Mikäli junalla on osaväliperumisia, saattaa viimeinen perumista edeltävä pysähdys jäädä virheellisesti ei-kaupalliseksi.
        public string commercialTrack; // string Suunniteltu raidenumero, jolla juna pysähtyy tai jolta se lähtee.Operatiivisissa häiriötilanteissa raide voi olla muu.
        public bool cancelled; // true/false   Totta, jos lähtö tai saapuminen on peruttu
        public DateTime scheduledTime; // datetime Aikataulun mukainen pysähtymis- tai lähtöaika
        public DateTime liveEstimateTime; // datetime Ennuste. Tyhjä jos juna ei ole matkalla
        public DateTime actualTime; // datetime Aika jolloin juna saapui tai lähti asemalta
        public int differenceInMinutes; // integer Vertaa aikataulun mukaista aikaa ennusteeseen tai toteutuneeseen aikaan ja kertoo erotuksen minuutteina
        public List<Syy> causes;
    }

    public class Syy
    {

        //           Syytiedot.Kuvaavat syitä miksi juna oli myöhässä tai etuajassa pysähdyksellä.Kaikkia syyluokkia ja -tietoja ei julkaista.
        public string categoryCodeId;
        public string categoryCode;
        public string detailedCategoryCodeId;
        public string detailedCategoryCode;

    }
}
