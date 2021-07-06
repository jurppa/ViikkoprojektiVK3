using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoodinimiIdänpikajuna.Model
{
    public class Train
    {
        public int trainNumber; //; // 1-99999  Junan numero.Esim junan "IC 59" junanumero on 59
        public string trainType; // IC, P, S, ...
        public string trainCategory; // lähiliikenne, kaukoliikenne, tavaraliikenne, ...
        public string commuterLineID; // Z, K, N....
        public bool runningCurrently; // true/false  Onko juna tällä hetkellä kulussa
        public bool cancelled; // true/false    Totta, jos junan peruminen on tehty 10 vuorokauden sisällä.Yli 10 vuorokautta sitten peruttuja junia ei palauteta rajapinnassa laisinkaan.
        public List<TimeTable> timeTableRows; //integer Kuvaa saapumisia ja lähtöjä liikennepaikoilta.Järjestetty reitin mukaiseen järjestykseen.
        public List<JourneySection> journeySections;

    }
    public class TimeTable
    {
        public bool trainStopping;
        public string stationShortCode; // string Aseman lyhennekoodi
        public int stationcUICCode; // 1-9999  Aseman UIC-koodi
        public string type; // "ARRIVAL" tai "DEPARTURE"  Pysähdyksen tyyppi
        public bool commercialStop; // boolean Onko pysähdys kaupallinen. Annettu vain pysähdyksille, ei läpiajoille. Mikäli junalla on osaväliperumisia, saattaa viimeinen perumista edeltävä pysähdys jäädä virheellisesti ei-kaupalliseksi.
        public string commercialTrack; // string Suunniteltu raidenumero, jolla juna pysähtyy tai jolta se lähtee.Operatiivisissa häiriötilanteissa raide voi olla muu.
        public bool cancelled; // true/false   Totta, jos lähtö tai saapuminen on peruttu
        public DateTime scheduledTime; // datetime Aikataulun mukainen pysähtymis- tai lähtöaika
        public DateTime liveEstimateTime; // datetime Ennuste. Tyhjä jos juna ei ole matkalla
        public DateTime actualTime; // datetime Aika jolloin juna saapui tai lähti asemalta
        public int differenceInMinutes; // integer Vertaa aikataulun mukaista aikaa ennusteeseen tai toteutuneeseen aikaan ja kertoo erotuksen minuutteina
        public List<Reason> causes;
    }
    public class Reason
    {

        //           Syytiedot.Kuvaavat syitä miksi juna oli myöhässä tai etuajassa pysähdyksellä.Kaikkia syyluokkia ja -tietoja ei julkaista.
        public string categoryCodeId;
        public string categoryCode;
        public string detailedCategoryCodeId;
        public string detailedCategoryCode;

    }

    public class JourneySection
    {
        public List<Wagon> wagons;

    }
    public class Wagon
    {
        public bool catering;
        public bool luggage;
        public bool pet;
        public bool playground;
        public bool smoking;
    }
}
