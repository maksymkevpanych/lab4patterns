using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4
{
    class Resident
    {
        public string FullName { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    
    class Building
    {
        public List<Resident> Residents { get; set; }

        public Building()
        {
            Residents = new List<Resident>();
        }
    }

    
    class PrivateHouse : Building
    {
        
    }

    
    class ApartmentBuilding : Building
    {
        public List<List<Resident>> Apartments { get; set; }

        public ApartmentBuilding()
        {
            Apartments = new List<List<Resident>>();
        }
    }

    
    class MilitaryService
    {
        public static void DisplayConscripts(List<Building> buildings)
        {
            Console.WriteLine("Список призовників:");
            foreach (var building in buildings)
            {
                foreach (var resident in building.Residents)
                {
                    
                    if (IsConscript(resident))
                    {
                        Console.WriteLine($"ПІБ: {resident.FullName}, Стать: {resident.Gender}, Дата народження: {resident.DateOfBirth.ToShortDateString()}");
                    }
                }

                if (building is ApartmentBuilding apartmentBuilding)
                {
                    foreach (var apartment in apartmentBuilding.Apartments)
                    {
                        foreach (var resident in apartment)
                        {
                            if (IsConscript(resident))
                            {
                                Console.WriteLine($"ПІБ: {resident.FullName}, Стать: {resident.Gender}, Дата народження: {resident.DateOfBirth.ToShortDateString()}");
                            }
                        }
                    }
                }
            }
        }

        private static bool IsConscript(Resident resident)
        {
            return resident.Gender.ToLower() == "male" &&
                   DateTime.Now.Year - resident.DateOfBirth.Year >= 18 &&
                   DateTime.Now.Year - resident.DateOfBirth.Year <= 27;
        }
    }

    
    class CensusService
    {
        public static void CountTotalResidents(List<Building> buildings)
        {
            int totalResidents = 0;

            foreach (var building in buildings)
            {
                totalResidents += building.Residents.Count;

                if (building is ApartmentBuilding apartmentBuilding)
                {
                    foreach (var apartment in apartmentBuilding.Apartments)
                    {
                        totalResidents += apartment.Count;
                    }
                }
            }

            Console.WriteLine($"Загальна кількість мешканців: {totalResidents}");
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            
            var buildings = new List<Building>
        {
            new PrivateHouse { Residents = { new Resident { FullName = "Іван Іванов", Gender = "Male", DateOfBirth = new DateTime(1999, 5, 15) } } },
            new ApartmentBuilding
            {
                Apartments =
                {
                    new List<Resident> { new Resident { FullName = "Марія Петренко", Gender = "Female", DateOfBirth = new DateTime(1985, 10, 3) } },
                    new List<Resident> { new Resident { FullName = "Петро Сидоренко", Gender = "Male", DateOfBirth = new DateTime(1995, 7, 20) } }
                }
            }
        };

            
            MilitaryService.DisplayConscripts(buildings);

            
            CensusService.CountTotalResidents(buildings);
            Console.ReadLine();
        }
    }
}
