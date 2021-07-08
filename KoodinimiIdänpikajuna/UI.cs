﻿using KoodinimiIdänpikajuna.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoodinimiIdänpikajuna
{
    class UI
    {
        /// <summary>
        /// StartMenu valikosta kaikki lähtee liikkeelle ja täällä käyttäjä voi valita mitä haluaa tehdä. 
        ///
        /// -Thien
        /// </summary>
        public void StartMenu()
        {
            Console.WriteLine("1. Juna aikataulut");
            Console.WriteLine("2. Tietoa junan palveluista");
            Console.WriteLine("3. Aseman kautta kulkevat junat");
            Console.WriteLine("4. Junan nykyinen sijainti");
            Console.WriteLine("0. Poistu sovelluksesta");
            Console.Write("Valitse toiminta syöttämällä oikea numero: ");

            var input = Console.ReadLine();
            UI ui = new UI();
            if (input == "")
            {
                {
                    hasFaultInINput();
                    ui.StartMenu();
                }
            }
            else
            {


                char firstChar = input[0];
                bool isNumber = Char.IsDigit(firstChar);

                if (isNumber != true || input.Length < 1)
                {
                    hasFaultInINput();
                    ui.StartMenu();
                }
                else
                {
                    int inputToInt = Int32.Parse(input);
                    while (input != null)
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
                            case 0:
                                Environment.Exit(0);
                                break;

                            default:
                                hasFaultInINput();
                                ui.StartMenu();
                                continue;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Tämä metodi kysytään lähtöaseman, pääteaseman ja päivämärän jonka jälkeen tulostetaan lista lähtevistä junista siltä päivältä.
        /// -Thien & Ari
        /// </summary>

        public void FromTo()
        {
            Console.WriteLine("Lähtöasema: ");
            var station1 = Console.ReadLine();
            Console.WriteLine("Pääteasema: ");
            var station2 = Console.ReadLine();

            if (!hasSpecialChar(station1) && !hasSpecialChar(station2))
            {
                Console.WriteLine("Anna päivämäärä(dd.mm.yyyy) tai paina 'enter' jos haluat nykyisen päivän: ");
                var input = Console.ReadLine();
                DateTime date = DateTime.Parse(DateTimeNow(input));



                List<Train> trainsFromTo = APIUtil.TrainFromTo(station1, station2, date);
                for (int i = 0; i < trainsFromTo.Count; i++)
                {
                    int lastIndex = trainsFromTo[i].timeTableRows.Count;
                    //if (trainsFromTo[i].timeTableRows[i].type == "ARRIVAL") { continue; }

                    Console.WriteLine();
                    Console.WriteLine(trainsFromTo[i].trainType + " " + trainsFromTo[i].trainNumber + " | " + trainsFromTo[i].timeTableRows[0].type + " | " + trainsFromTo[i].timeTableRows[i].scheduledTime.ToLocalTime());
                    Console.WriteLine(APIUtil.IsTrainLate(trainsFromTo[i].timeTableRows[i].liveEstimateTime, trainsFromTo[i].timeTableRows[i].scheduledTime.ToLocalTime()));
                    Console.WriteLine();
                    Console.WriteLine("Matkan kesto: " + (trainsFromTo[i].timeTableRows[lastIndex - 1].scheduledTime - trainsFromTo[i].timeTableRows[0].scheduledTime));
                    //Console.WriteLine(trainsFromTo[i].timeTableRows[lastIndex - 1].scheduledTime - trainsFromTo[i].timeTableRows[0].scheduledTime);

                }
                Console.WriteLine("Paina mitä tahansa näppäintä palataksesi menuun.");
                Console.ReadKey();
            }
            else
            {
                hasFaultInINput();
            }
        }
        /// <summary>
        /// Tämä metodi kertoo käyttäjälle junan palvelut junan numeron perusteella.
        /// -Thien
        /// </summary>
        public void TrainInfo()
        {
            Console.WriteLine("Anna junan numero: ");
            var input = Console.ReadLine();
            if (!hasLetterChar(input))
            {
                var trainNumber = Convert.ToInt32(input);
                Console.WriteLine("Anna lähtöpäivämäärä(dd.mm.yyyy) tai paina 'enter' jos haluat nykyisen päivän: ");
                input = Console.ReadLine();
                var date = DateTime.Parse(DateTimeNow(input));
                var wagon = APIUtil.GetWagonInfo(date, trainNumber);

                Console.WriteLine();
                Console.WriteLine("Vaunun sisältämät palvelut: ");
                Console.WriteLine();

                foreach (KeyValuePair<string, bool> item in wagon)
                {
                    Console.WriteLine(item.Key);
                    Console.WriteLine();
                }
                Console.WriteLine("Paina mitä tahansa näppäintä palataksesi menuun.");
                Console.ReadKey();
            }

            else
            {
                hasFaultInINput();
            }
        }
        /// <summary>
        /// Metodi katsoo aseman läpi 5 seuraavaa menevää junaa ja niiden pääteasemat.
        /// -Thien
        /// </summary>
        public void GoingThroughTrains()
        {
            Console.WriteLine("Anna aseman nimi: ");
            var station = Console.ReadLine();
            if(!hasSpecialChar(station))
            {
                string[] stn = APIUtil.GetStationShortNames(station);
                station = stn[0];
                var demTrains = APIUtil.GoingThrough(station);
                
                Console.WriteLine();
                for (int i = 0; i < demTrains.Count -1; i++)
                {
                    int lastIndex = demTrains[i].timeTableRows.Count;

                    Console.WriteLine(demTrains[i].trainType + " " + demTrains[i].trainNumber + " Pääteasema: " + APIUtil.ShortNameToFullName(demTrains[i].timeTableRows[lastIndex -1].stationShortCode));
                    Console.WriteLine(APIUtil.IsTrainLate(demTrains[i].timeTableRows[i].actualTime, demTrains[i].timeTableRows[0].scheduledTime));                        Console.WriteLine();

                }
                Console.WriteLine("Paina mitä tahansa näppäintä palataksesi menuun.");
                Console.ReadKey();
            }
            else
            {
                hasFaultInINput();
            }
        }
        //Junan "signaalin" live-seuranta. Haetaan nykyinen, seuraava ja edellinen asema junan numeron perusteella.
        //-Ari
        public void LiveTrain()
        {
            Console.WriteLine("Anna Junan numero: ");
            var input = Console.ReadLine();
            if(!hasLetterChar(input))
            {
                int tnumber = Convert.ToInt32(input);
                var live = APIUtil.TrackLiveTrainLocation(tnumber);

                Console.WriteLine("Haetun junan viimeinen tieto: " + live.timestamp.ToLocalTime());
                Console.WriteLine("Juna on tällä hetkellä asemalla: " + APIUtil.ShortNameToFullName(live.station));
       
                if (live.nextStation != "END")
                {
                    Console.WriteLine("Seuraava asema: " + APIUtil.ShortNameToFullName(live.nextStation));
                }
                else
                {
                    Console.WriteLine("Seuraava asema: Ei tiedossa vielä.");
                }
                if (live.previousStation != "END")
                {
                    Console.WriteLine("Edellinen asema: " + APIUtil.ShortNameToFullName(live.previousStation));
                }
                else
                {
                    Console.WriteLine("Edellinen asema: Ei tiedossa.");
                }
                Console.WriteLine();
                Console.WriteLine("Paina mitä tahansa näppäintä palataksesi menuun");
                Console.ReadKey();
            }
            else
            {
                hasFaultInINput();
            }
        }
        /// <summary>
        /// Tämä metodi Junan livesijainnin aseman mukaan, millon havainto on tehty sekä muuttaa asemien lyhenteet kokonimiksi.
        /// </summary>
        /// 

        /// <summary>
        /// Tämä metodi palauttaa nykyisen päivämäärän ja ajan jos käyttäjä ei sitä manuaalisesti syötä vaan painaa enteriä.
        /// </summary>
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
        public void hasFaultInINput()
        {
            Console.WriteLine();
            Console.WriteLine("Syöte on virheellinen! Yritä uudelleen, palaa menuun painamalla mitä tahansa näppäintä.");
            Console.WriteLine();
            Console.ReadKey();
            Console.Clear();
        }
        public static bool hasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,1234567890";
            foreach (var item in specialChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }
        public static bool hasLetterChar(string input)
        {
            string letterChar = @"\|!#$%&/()=?»«@£§€{}.-;'<>_,abcdefghijklmnopqrstyvxzåäö";
            foreach (var item in letterChar)
            {
                if (input.Contains(item)) return true;
            }

            return false;
        }
    }
}
