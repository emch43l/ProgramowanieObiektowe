using System;

namespace lab_8
{
 
    // stara forma delegata
    delegate double Operator(double a, double b);
    /// to samo co wyżej - delegate double Calc(double a, double b);
    class Program
    {
        public static double Addition(double x, double y)
        {
            return x + y;
        }

        public static double Mul(double x, double y)
        {
            return x * y;
        }

        public static void PrintIntArray(int[] a, Func<int, string> formatter)
        {
            foreach(var item in a)
            {
                Console.WriteLine(formatter.Invoke(item));
            }
        }

        static void Main(string[] args)
        {
            Operator operation = Addition;
            double result = operation.Invoke(4, 6);
            operation = Mul;
            double mulResult = operation.Invoke(5, 5);
            

            Console.WriteLine(result+" "+mulResult);
            /// \/ inna forma delegata *nowa* \/
            Func<double, double, double> op = Mul;
            op = Addition;
            //delegat anonimowy
            Func<int, string> Formatter = delegate (int number)
            {
                return string.Format("0x{0:x}", number);
            };

            Func<int, string> DecFormat = delegate (int number)
            {
                return string.Format("{0}", number);
            };

            Console.WriteLine(Formatter.Invoke(2));
            /// przyjmuje tylko jedną wartość. w przypadku wymogu użycia więcej niż 1 parametru należy zastosować delegata, zwraca zawsze wartość bool
            Predicate<string> OnlyThree = delegate (string str)
            {
                return str.Length == 3;
            };

            // pełni rolę predyktatu przyjmującego więcej niż 1 argument
            Func<int, int, int, bool> InRange = delegate (int value, int min, int max)
            {
                return value > min && value < max;
            };

            // jest tzw "konsumentem" czyli delegatem który nie zwraca żadnej wartości
            Action<string> Print = delegate (string str)
            {
                Console.WriteLine(str);
            };

            // przy lambdzie nie stosuje się delegatów, podobnie z typami zmiennych
            Operator AddLambda = (a, b) => a + b;
            Action<string> PrintLabmda = (str) => Console.WriteLine(str);

            PrintIntArray(new int[] { 1, 5, 6, 15, 333, 53 }, Formatter);
            PrintIntArray(new int[] { 1, 5, 6, 15, 333, 53 }, DecFormat);

        }
    }
}
