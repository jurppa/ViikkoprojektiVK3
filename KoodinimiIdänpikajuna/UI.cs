﻿using KoodinimiIdänpikajuna.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoodinimiIdänpikajuna
{
    class UI
    {
        /// <summary>
        /// StartMenu valikosta kaikki lähtee liikkeelle ja täällä käyttäjä voi valita mitä haluaa tehdä. 
        /// -Thien
        /// </summary>
        public void StartMenu()
        {
            Console.WriteLine("1. Juna aikataulut");
            Console.WriteLine("2. Tietoa junan palveluista");
            Console.WriteLine("3. Aseman kautta kulkevat junat");
            Console.WriteLine("4. Junan nykyinen sijainti");
            Console.Write("Valitse toiminta syöttämällä oikea numero: ");

            var input = Console.ReadLine();
            int inputToInt = Int32.Parse(input);
            UI ui = new UI();

            while(input != null)
            {
                switch (inputToInt)
                {
                    case 1:
                        Console.WriteLine();
                        ui.FromTo();
                        Console.Clear();
                        ui.StartMenu();
                        break;
                    case 2:
                        Console.WriteLine();
                        ui.TrainInfo();
                        Console.Clear();
                        ui.StartMenu();
                        break;
                    case 3:
                        Console.WriteLine();
                        ui.GoingThroughTrains();
                        Console.Clear();
                        ui.StartMenu();
                        break;
                    case 4:
                        Console.WriteLine();
                        ui.LiveTrain();
                        Console.Clear();
                        ui.StartMenu();
                        break;
                }
            }
        }
        
        /// <summary>
        /// Tämä metodi kysytään lähtöaseman, pääteaseman, ja päivämäärän jonka jälkeen tulostetaan lista lähtevistä junista.
        /// -Thien & Ari
        /// </summary>
        
        public void FromTo()
        {
            Console.WriteLine("Lähtöasema: ");
            var station1 = Console.ReadLine();
            Console.WriteLine("Pääteasema: ");
            var station2 = Console.ReadLine();
            Console.WriteLine("Anna päivämäärä(dd.mm.yyyy): ");
            string input = Console.ReadLine();
            DateTimeNow(input);
            

            List<Train> trainsFromTo = APIUtil.TrainFromTo(station1, station2);
            for (int i = 0; i < trainsFromTo.Count; i++)
            {
                if (trainsFromTo[i].timeTableRows[i].type == "ARRIVAL") { continue; }

                Console.WriteLine();
                Console.WriteLine(trainsFromTo[i].trainType + " " + trainsFromTo[i].trainNumber + " | " + trainsFromTo[i].timeTableRows[i].type + " | " + trainsFromTo[i].timeTableRows[i].scheduledTime.ToLocalTime());
                                
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Tämä metodi kertoo käyttäjälle junan palvelut junan numeron perusteella.
        /// -Thien
        /// </summary>
        public void TrainInfo()
        {
            Console.WriteLine("Anna junan numero: ");
            var trainNumber = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Lähtöpäivämäärä: ");
            var date = Convert.ToDateTime( Console.ReadLine());
            var wagon = APIUtil.GetWagonInfo(date, trainNumber);
            Console.WriteLine();
            Console.WriteLine("Vaunun sisältämät palvelut: ");
            Console.WriteLine();
            foreach (KeyValuePair<string, bool>item in wagon)
            {
                Console.WriteLine(item.Key);
            }
            Console.ReadKey();
        }
        /// <summary>
        /// Metodi katsoo aseman läpi 5 seuraavaa menevää junaa ja niiden pääteasemat.
        /// -Thien
        /// </summary>
        public void GoingThroughTrains()
        {
            Console.WriteLine("Anna aseman nimi: ");
            var station = Console.ReadLine();
            string[] stn = APIUtil.GetStationFullNames(station);
            station = stn[0];
            var demTrains = APIUtil.GoingThrough(station);
            for (int i = 0; i < demTrains.Count; i++)
            {
                Console.WriteLine(demTrains[i].trainType + " " + demTrains[i].trainNumber + " Pääteasema: " + APIUtil.ShortNameToFullName(demTrains[i].timeTableRows[i].stationShortCode));
            }
            Console.ReadKey();
        }
        //Junan "signaalin" live-seuranta. Haetaan nykyinen, seuraava ja edellinen asema junan numeron perusteella.
        //-Ari
        public void LiveTrain()
        {   
            Console.WriteLine("Anna Junan numero: ");
            int tnumber = Convert.ToInt32(Console.ReadLine());

            var live = APIUtil.TrackLiveTrainLocation(tnumber);
          
            Console.WriteLine("Haetun junan viimeinen tieto: " + live.timestamp);
            Console.WriteLine("Juna on tällä hetkellä asemalla: " + live.station);
            Console.WriteLine("Seuraava asema: " + live.nextStation);
            Console.WriteLine("Edellinen asema: " + live.previousStation);
            Console.ReadKey();
        }

        public string DateTimeNow(string input)
        {
            DateTime date;
            if (input == "")
            {
                date = DateTime.Now;
            }
            else
            {
                date = DateTime.Parse(input);
            }
            return date.ToString();
        }
    }
}
