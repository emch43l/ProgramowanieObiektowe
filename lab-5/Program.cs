using System;
using System.Collections.Generic;

namespace lab_5
{
    enum Degree
    {
        BardzoDobry = 50,
        DobryPlus = 45,
        Dobry = 40,
        DostatecznyPlus = 35,
        Dostateczny = 30,
        Niedostateczny = 20
    }

    //public record Student(string Name, int Points, char Group);
    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        Degree ocena = Degree.Dostateczny;
    //        Console.WriteLine(ocena);
    //        Console.WriteLine((int)ocena);
    //        string[] names = Enum.GetNames<Degree>();
    //        Console.WriteLine(string.Join(", ", names));
    //        Degree[] degrees = Enum.GetValues<Degree>();
    //        foreach(Degree d in degrees)
    //        {
    //            Console.WriteLine($"{d} {(int)d}");
    //        }
    //        Console.WriteLine("Wpisz ocenę: ");
    //        string input = Console.ReadLine();
    //        try
    //        { 
    //            Degree degree = Enum.Parse<Degree>(input);
    //            Console.WriteLine($"Wpisałeś {degree} o wartości {(int)degree}");
    //        }
    //        catch (ArgumentException e)
    //        {
    //            Console.WriteLine("Wpisałeś nieznaną ocenę !");
    //        }
    //        string usDegree;
    //        switch(ocena)
    //        {
    //            case Degree.BardzoDobry:
    //                usDegree = "A";
    //                break;
    //            case Degree.DobryPlus:
    //                usDegree = "B";
    //                break;
    //            case Degree.Dobry:
    //                usDegree = "C";
    //                break;
    //            case Degree.DostatecznyPlus:
    //                usDegree = "D";
    //                break;
    //            case Degree.Dostateczny:
    //                usDegree = "E";
    //                break;
    //            default:
    //                usDegree = "F";
    //                break;
    //        }

    //        usDegree = ocena switch
    //        {
    //            Degree.BardzoDobry => "A",
    //            Degree.DobryPlus => "B",
    //            Degree.Dobry => "C",
    //            Degree.DostatecznyPlus => "D",
    //            Degree.Dostateczny => "E",
    //            _ => "F"
    //        };

    //        Console.WriteLine(usDegree);
    //        int points = 67;
    //        Degree ocenaPunkty = points switch
    //        {
    //            >= 50 and < 60 => Degree.Dostateczny,
    //            >= 60 and < 70 => Degree.DostatecznyPlus,
    //            >= 70 and < 80 => Degree.Dobry,
    //            >= 80 and < 90 => Degree.DobryPlus,
    //            >= 90 and < 100 => Degree.BardzoDobry,
    //            _ => Degree.Niedostateczny
    //        };

    //        Student[] students =
    //        {
    //            new Student(Name: "Karol", Points: 120, Group: 'E'),
    //            new Student(Name: "Ewa", Points: 121, Group: 'E'),
    //            new Student(Name: "Robert", Points: 50, Group: 'B'),
    //            new Student(Name: "Ania", Points: 30, Group: 'B'),
    //            new Student(Name: "Rupert", Points: 10, Group: 'E'),
    //        };

    //        Console.WriteLine(students[0] == new Student("Ewa", 121, 'E'));

    //        //foreach (Student st in students)
    //        //{
    //        //    Console.WriteLine(st);
    //        //}
    //        (string, bool)[] results = new (string, bool)[students.Length];
    //        for(int i = 0; i < students.Length; i++)
    //        {
    //            Student st = students[i];
    //            results[i] = (st.Name, st switch
    //            {
    //                { Points: >= 100, Group: 'E' } => true,
    //                { Points: >= 50, Group: 'B' } => true,
    //                _ => false
    //            });

    //            foreach(var s in results)
    //            {
    //                Console.WriteLine($"Student: {s.Item1}, czy zdał: {s.Item2}");
    //            }
    //        }

    //    }
    //}

    class Program
    {
        public static void Main(string[] args)
        {
            (int, int) point1 = (2, 4);
            Direction4 dir = Direction4.UP;
            var point2 = Exercise1.NextPoint(dir, point1, (2, 4));
            Console.WriteLine(point2);

            Student[] students = {
              new Student("Kowal","Adam", 'A'),
              new Student("Nowak","Ewa", 'A'),
              new Student("Nowak","Karol",'B')
            };
            Exercise4.AssignStudentId(students);

            int[,] screen =
            {
                {1, 0, 0},
                {0, 0, 0},
                {0, 0, 0}
            };

            (int, int) point = (1, 1);

            Console.WriteLine(Exercise2.DirectionTo(screen, point, 1));

            Car[] cars = new Car[]
            {
                new Car(),
                new Car(Model: "Fiat", true),
                new Car(),
                new Car(Model: "Fiat", true),
                new Car(Power: 125),
            };
            Console.WriteLine(Exercise3.CarCounter(cars));
        }
    }

    enum Direction8
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        UP_LEFT,
        DOWN_LEFT,
        UP_RIGHT,
        DOWN_RIGHT
    }

    enum Direction4
    {
        UP,
        DOWN,
        LEFT,
        RIGHT
    }

    //Cwiczenie 1
    //Zdefiniuj metodę NextPoint, która powinna zwracać krotkę ze współrzędnymi piksela przesuniętego jednostkowo w danym kierunku względem piksela point.
    //Krotka screenSize zawiera rozmiary ekranu (width, height)
    //Przyjmij, że początek układu współrzednych (0,0) jest w lewym górnym rogu ekranu, a prawy dolny ma współrzęne (witdh, height) 
    //Pzzykład
    //dla danych wejściowych 
    //(int, int) point1 = (2, 4);
    //Direction4 dir = Direction4.UP;
    //var point2 = NextPoint(dir, point1);
    //w point2 powinny być wartości (2, 3);
    //Jeśli nowe położenie jest poza ekranem to metoda powinna zwrócić krotkę z point
    class Exercise1
    {
        public static (int, int) NextPoint(Direction4 direction, (int, int) point, (int, int) screenSize)
        {

            (int, int) pointReturn = direction switch
            {
                Direction4.UP => (point.Item1, point.Item2 - 1),
                Direction4.DOWN => (point.Item1, point.Item2 + 1),
                Direction4.LEFT => (point.Item1 + 1, point.Item2),
                Direction4.RIGHT => (point.Item1 - 1 , point.Item2),
            };

            if(pointReturn.Item1 > screenSize.Item1 || pointReturn.Item2 > screenSize.Item2 || pointReturn.Item1 < 0 || pointReturn.Item2 < 0)
                return point;

            return pointReturn;


        }
    }
    //Cwiczenie 2
    //Zdefiniuj metodę DirectionTo, która zwraca kierunek do piksela o wartości value z punktu point. W tablicy screen są wartości
    //pikseli. Początek układu współrzędnych (0,0) to lewy górny róg, więc punkt o współrzęnych (1,1) jest powyżej punktu (1,2) 
    //Przykład
    // Dla danych weejsciowych
    // static int[,] screen =
    // {
    //    {1, 0, 0},
    //    {0, 0, 0},
    //    {0, 0, 0}
    // };
    // (int, int) point = (1, 1);
    // po wywołaniu - Direction8 direction = DirectionTo(screen, point, 1);
    // w direction powinna znaleźć się stała UP_LEFT
    class Exercise2
    {
        static int[,] screen =
        {
            {1, 0, 0},
            {0, 0, 0},
            {0, 0, 0}
        };

        private static (int, int) point = (1, 1);

        private Direction8 direction = DirectionTo(screen, point, 1);

        public static Direction8 DirectionTo(int[,] screen, (int, int) point, int value)
        {

            (int, int) screenPoint = (0, 0);
            for (int i = 0; i < screen.GetLength(0); i++)
                for (int j = 0; j < screen.GetLength(1); j++)
                {
                    if (screen[i, j] == value)
                        screenPoint = (j, i);
                }

            if (screenPoint == point)
                throw new ArgumentException("Point cannot be placed in the same x,y co-ordinates as screen point");

            return point switch
            {
                var (x, y) when x == screenPoint.Item1 && y > screenPoint.Item2 => Direction8.UP,
                var (x, y) when x == screenPoint.Item1 && y < screenPoint.Item2 => Direction8.DOWN,
                var (x, y) when x > screenPoint.Item1 && y == screenPoint.Item2 => Direction8.LEFT,
                var (x, y) when x < screenPoint.Item1 && y == screenPoint.Item2 => Direction8.RIGHT,
                var (x, y) when x > screenPoint.Item1 && y > screenPoint.Item2 => Direction8.UP_LEFT,
                var (x, y) when x > screenPoint.Item1 && y < screenPoint.Item2 => Direction8.DOWN_LEFT,
                var (x, y) when x < screenPoint.Item1 && y > screenPoint.Item2 => Direction8.UP_RIGHT,
                var (x, y) when x < screenPoint.Item1 && y < screenPoint.Item2 => Direction8.DOWN_RIGHT
            };
        }
    }

    //Cwiczenie 3
    //Zdefiniuj metodę obliczającą liczbę najczęściej występujących aut w tablicy cars
    //Przykład
    //dla danych wejściowych
    // Car[] _cars = new Car[]
    // {
    //     new Car(),
    //     new Car(Model: "Fiat", true),
    //     new Car(),
    //     new Car(Power: 100),
    //     new Car(Model: "Fiat", true),
    //     new Car(Power: 125),
    //     new Car()
    // };
    //wywołanie CarCounter(Car[] cars) powinno zwrócić 3
    record Car(string Model = "Audi", bool HasPlateNumber = false, int Power = 100);

    class Exercise3
    {
        public static int CarCounter(Car[] cars)
        {
            Dictionary<Car,int> list = new Dictionary<Car, int>();
            foreach(Car car in cars)
            {
                if (list.ContainsKey(car))
                    list[car]++;
                else
                    list.Add(car,1);
            }

            foreach(var car in list)
            {
                Console.WriteLine(car);
            }

            int commonCount = 0;

            foreach(KeyValuePair<Car,int> car in list)
            {
                if (commonCount < car.Value)
                    commonCount = car.Value;
            }

            return commonCount;
        }
    }

    record Student(string LastName, string FirstName, char Group, string StudentId = "");

    //Cwiczenie 4
    //Zdefiniuj metodę AssignStudentId, która każdemu studentowi nadaje pole StudentId wg wzoru znak_grupy-trzycyfrowy-numer.
    //Przykład
    //dla danych wejściowych
    //Student[] students = {
    //  new Student("Kowal","Adam", 'A'),
    //  new Student("Nowak","Ewa", 'A')
    //};
    //po wywołaniu metody AssignStudentId(students);
    //w tablicy students otrzymamy:
    // Kowal Adam 'A' - 'A001'
    // Nowal Ewa 'A'  - 'A002'
    //Należy przyjąc, że są tylko trzy możliwe grupy: A, B i C
    class Exercise4
    {

        //posortować
        public static void AssignStudentId(Student[] students)
        {
            int A = 0;
            int B = 0;
            int C = 0;
            int iterator = 0;
            char group = 'A';

            Array.Sort(students,(a,b) => a.Group.CompareTo(b.Group));

            for(int i = 0; i < students.Length; i++)
            {

                switch (students[i].Group)
                {

                    case 'A':
                        A++;
                        iterator = A;
                        group = 'A';
                        break;
                    case 'B':
                        B++;
                        iterator = B;
                        group = 'B';
                        break;
                    case 'C':
                        C++;
                        iterator = C;
                        group = 'C';
                        break;
                    default:
                        throw new ArgumentException("Unknown group");
                        break;
                }

                students[i] = new Student(students[i].LastName, students[i].FirstName, students[i].Group, Char.ToString(group) + iterator.ToString().PadLeft(3, '0'));

            }

        }
    }
}
