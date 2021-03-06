using System;
using System.Globalization;
using System.IO.Compression;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace lab_9_zad
{
    public record City(
        int Id, 
        string Name,
        double Latitude, 
        double Longitude,
        char FeatureClass, 
        string FeaturedCode,
        string CountryCode, 
        long Population,
        int Elevation, 
        string TimeZoneId
        );

    public record Country(
        string ISOCode, 
        string ISO3Code, 
        string ISONumeric, 
        string Fips,
        string CountryName,
        string Capital, 
        double Area, 
        int Population,
        string Continent, 
        string CurrencyCode, 
        string CurrencyName, 
        string Phone, 
        List<string> Neighbours
        );

    class Program
    {

        public static readonly string CitiesFileUrl = "http://download.geonames.org/export/dump/cities500.zip";
        public static readonly string CountryFileUrl = "http://download.geonames.org/export/dump/countryInfo.txt";
        public static readonly string PathToZipFile = Path.Combine(Path.GetTempPath(), "cities500.zip");
        public static readonly string CountriesTextFile = "countryInfo.txt";
        public static readonly string CitiesTextFile = "cities500.txt";
        public static readonly string CountriesPathToTextFile = Path.Combine(Path.GetTempPath(), CountriesTextFile);
        public static readonly string CitiesPathToTextFile = Path.Combine(Path.GetTempPath(), CitiesTextFile);
        static IEnumerable<City> cities = LoadCities();
        static IEnumerable<Country> countries = LoadCountries();

        public static void Main(string[] args)
        {
            Console.WriteLine();
            int points = 0;
            points += Test(() =>
            {
                var polishCities = PolishCities(cities, countries);
                if (polishCities.Count == 3677
                    && polishCities.Any(c => c.Name.Equals("Warsaw"))
                    && polishCities.Any(city => city.Name.Equals("Nowa Słupia") && city.Population == 1422)
                    && polishCities.Any(city => city.Name.Equals("Kraków"))
                    && polishCities.GroupBy(city => city.Name)
                        .Any(group => group.Key.Equals("Łaziska") && group.Count() == 5)
                    && polishCities.First().Name.Equals("Abramów")
                    && polishCities.Last().Name.Equals("Żywiec")
                   )
                {
                    return 1;
                }

                return 0;
            }, "Zadanie 1: ");
            points += Test(() =>
            {
                var result = CountryCities(cities, countries);
                if (result.Count() == 246
                    && result.Any(pair => pair.CountryCode.Equals("PL") && pair.CitiesCount == 3677)
                    && result.Any(pair => pair.CountryCode.Equals("US") && pair.CitiesCount == 21131)
                    && result.First().CitiesCount == 12 && result.First().CountryCode.Equals("AD")
                    && result.Last().CitiesCount == 66 && result.Last().CountryCode.Equals("ZW")
                   )
                {
                    return 1;
                }

                return 0;
            }, "Zadanie 2: ");
            points += Test(() =>
            {
                /// results 252
                Console.WriteLine("Liczba krajów: "+countries.Count());
                var result = Capitals(cities, countries);
                if (result.Count() == 241
                    && result.First().Equals(("Afghanistan", "Kabul", 3043532))
                    && result.Last().Equals(("Zimbabwe", "Harare", 1542813))
                    && result.Contains(("Poland", "Warsaw", 1702139))
                    && result.First() is (string CountryName, string Capital, long CapitalPopulation))
                {
                    return 1;
                }

                return 0;
            }, "Zadanie 3: ");
            points += Test(() =>
            {
                var result = EvenNumbers(7);
                if (result.Count() == 4
                    && result.All(n => n % 2 == 0)
                   )
                {
                    return 1;
                }

                return 0;
            }, "Zadanie 4: ");
            points += Test(() =>
            {
                var result = RandomNames(7);
                if (
                    result.Count() == 7
                    && result.All(n => n.Equals("Adam") || n.Equals("Ewa") || n.Equals("Karol"))
                    && result.GroupBy(n => n).Count() == 3
                )
                {
                    return 1;
                }

                return 0;
            }, "Zadanie 5: ");
            points += Test(() =>
            {
                var result = Ramp("*", 4);
                if (
                    result.Count() == 4
                    && result.Last().Length == 4 && result.Last().Equals("****")
                    && result.First().Length == 1 && result.First().Equals("*")
                )
                {
                    return 1;
                }

                return 0;
            }, "Zadanie 6: ");
            points += Test(() =>
            {
                var result = Tree(4);
                if (
                    result.Count() == 4
                    && result.Last().Length == 4 && result.Last().Equals("****")
                    && result.First().Length == 1 && result.First().Equals("*")
                )
                {
                    return 1;
                }

                return 0;
            }, "Zadanie 7: ");
            Console.WriteLine("Suma punktów " + points);
        }

        //Zadanie 1
        //Zaimplementuja metodę, aby zwracała listę polskich maist posortowanych alfabetycznie
        public static List<City> PolishCities(IEnumerable<City> cities, IEnumerable<Country> countries)
        {
            return cities.Where(c => c.CountryCode == "PL").OrderBy(c => c.Name).Select(c => c).ToList();
        }

        //Zadanie 2
        //Zaimplementuj metodę zwracająca IEnumerable z krotkami z kodem kraju oraz liczbą miast w tym kraju. Nazwy pól kortek poniżej, w sygnaturze metody.
        //Krotki posortuj wg kodu kraju
        public static IEnumerable<(string CountryCode, int CitiesCount)> CountryCities(IEnumerable<City> cities, IEnumerable<Country> countries)
        {
            return cities.GroupBy(c => c.CountryCode).OrderBy(c => c.Key).Select(c => (c.Key, c.Count()));
        }

        //Zadanie 3
        //Zaimplementuj metodę zwracająca IEnumerable z krotkami z nazwą kraju, nazwą jego stolicy oraz populacją stolicy posortowaną alfabetycznie wg nazwy kraju
        //Postać krotki poniżej w typie zwracanym
        public static IEnumerable<(string CountryName, string Capital, long CapitalPopulation)> Capitals(IEnumerable<City> cities, IEnumerable<Country> countries)
        {

            return countries.OrderBy(c => c.CountryName).Select(country => (country.CountryName, country.Capital, cities.Where(city => city.Name == country.Capital).Select(city => city.Population).FirstOrDefault()));

        }

        //Zadanie 4
        //Zaimplementuj metodę aby zwracała IEnumerable z liczbami parzystymi od 0 do max,
        //Zapisz wyrażenie z użyciem LINQ Fluent Api i klasy Enumerable
        public static IEnumerable<int> EvenNumbers(int max)
        {
            return from n in Enumerable.Range(0, max) where n % 2 == 0 select n;
        }

        //Zadanie 5
        //Zaimplementuj metodę, aby zwracała IEnumerable z losowym ciągiem składaącym się z imion: Ewa, Karol,Adam.
        //Imiona mogą się powtarzać, a liczba powinna być równa parametrowi count, które jest zawsze > 2. Ważne aby, każde imię w ciągu wystąpiło przynajmniej raz!
        //Przykład
        //dla wywołania RandomNames(5) przykładowy ciąg to: Ewa, Karol, Ewa, Adam, Adam
        //Zapisz wyrażenie z użyciem LINQ Fluent Api i klasy Enumerable
        public static IEnumerable<string> RandomNames(int count)
        {
            string[] names = { "Ewa", "Karol", "Adam" };
            return from rand in Enumerable.Range(0,count) select names[rand > names.Length - 1 ? new Random().Next(0,names.Length - 1) : rand];
        }

        //Zadanie 6
        //Zaiplementuj metodę zwracającą IEnumerable łańcuchów, złożonych z kolejnych powtórzeń łańcucha s od 1 do count.
        //Przykład
        //dla wywołania Ramp("A", 4) zwrócona kolekcja powinna zawierać łańcuchy:
        //"A", "AA", "AAA", "AAAA"
        public static IEnumerable<string> Ramp(string s, int count)
        {
            return Enumerable.Range(1, count).Select(c => {
                string str = s;
                for (int i = 1; i < c; i++)
                    str += s;
                return str;

            });
        }

        //Zadanie 7
        //Zaimplementuj metodę, aby zwracała ciąg łańcuchów o tej samej długości, które wyświetlone w kolejnych wierszach
        //utworzą trójkąt złożony z gwiazdek o wysokości height
        //Przykład
        //Wywołanie Tree(4) utworzy cztery poniższe łańcuchy:
        //"   *   "
        //"  ***  "
        //" ***** "
        //"*******"
        //Zastosuj wyłącznie LINQ i metody Prepend i Append 
        public static IEnumerable<string> Tree(int height)
        {
            string triangleRow = "*";
            return Enumerable.Range(1,height).Select(c => Enumerable.Range(1, c).Aggregate<int, string>(triangleRow, (first, next) =>
            {
                return next == 1 ? triangleRow : first+="*";
            }));

        }

        public static int Test(Func<int> testedCode, string message)
        {
            try
            {
                int points = testedCode.Invoke();
                if (points > 0)
                {
                    Console.WriteLine(message + points);
                    return points;
                }

                Console.WriteLine(message + 0);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(message + 0);
            }

            return 0;
        }


        public static List<City> LoadCities()
        {
            WebClient client = new WebClient();
            if (!File.Exists(PathToZipFile))
            {
                client.DownloadFile(CitiesFileUrl, PathToZipFile);
                ZipFile.ExtractToDirectory(PathToZipFile, Path.GetTempPath());
            }

            List<City> cities = new List<City>();
            Console.WriteLine("Starting loading cities ...");
            int count = 0;
            string message = "";
            CultureInfo specificCulture = CultureInfo.CreateSpecificCulture("en-EN");
            foreach (var line in File.ReadLines(CitiesPathToTextFile))
            {
                string[] tokens = line.Split("\t", StringSplitOptions.TrimEntries);
                City city = new City(
                    int.Parse(tokens[0]),
                    tokens[1],
                    double.Parse(tokens[4], NumberStyles.Float, specificCulture),
                    double.Parse(tokens[5], NumberStyles.Float, specificCulture),
                    tokens[6].ToCharArray()[0],
                    tokens[7],
                    tokens[8],
                    long.Parse(tokens[14]),
                    tokens[15].Length > 0 ? int.Parse(tokens[15]) : 0,
                    tokens[17]
                );
                cities.Add(city);
                count++;
                if (count % 1000 == 0)
                {
                    Console.Write(string.Join("", Enumerable.Repeat("\b", message.Length)));
                    message = $"Loading {count} cities";
                    Console.Write(message);
                }
            }

            Console.Write(string.Join("", Enumerable.Repeat("\b", message.Length)));
            message = $"Loading {count} cities";
            Console.Write(message);
            Console.WriteLine();
            return cities;
        }

        public static List<Country> LoadCountries()
        {
            WebClient client = new WebClient();
            if (!File.Exists(CountriesPathToTextFile))
            {
                client.DownloadFile(CountryFileUrl, CountriesPathToTextFile);
            }

            List<Country> countries = new List<Country>();
            Console.WriteLine("Starting loading countries ...");
            int count = 0;
            string message = "";
            CultureInfo specificCulture = CultureInfo.CreateSpecificCulture("en-EN");
            foreach (var line in File.ReadLines(CountriesPathToTextFile))
            {
                if (line.StartsWith("#"))
                {
                    continue;
                }

                string[] tokens = line.Split("\t", StringSplitOptions.TrimEntries);
                Country country = new Country(
                    tokens[0],
                    tokens[1],
                    tokens[2],
                    tokens[3],
                    tokens[4],
                    tokens[5],
                    tokens[6].Length != 0 ? double.Parse(tokens[6], NumberStyles.Float, specificCulture) : 0,
                    tokens[7].Length != 0 ? int.Parse(tokens[7]) : 0,
                    tokens[8],
                    tokens[9],
                    tokens[10],
                    tokens[11],
                    new List<string>(tokens[18].Split(", "))
                );
                countries.Add(country);
                count++;
                if (count % 10 == 0)
                {
                    Console.Write(string.Join("", Enumerable.Repeat("\b", message.Length)));
                    message = $"Loading {count} countries";
                    Console.Write(message);
                }
            }

            Console.Write(string.Join("", Enumerable.Repeat("\b", message.Length)));
            message = $"Loading {count} countries";
            Console.Write(message);
            Console.WriteLine();
            return countries;
        }

    }
}
