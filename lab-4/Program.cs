using System;

namespace lab_4
{
    class Program
    {
        static void Main(string[] args)
        {
            Product[] shop = new Product[4];
            shop[0] = new Computer() { Price = 2000m, Vat = 23, };
            shop[1] = new Paint() { PriceUnit = 12, Capacity = 5, Vat = 8 };
            shop[2] = new Computer() { Price = 5000m, Vat = 23 };
            shop[3] = new Paint() { PriceUnit = 7, Capacity = 10, Vat = 8 };

            decimal sumVat = 0;
            decimal sumPrice = 0;
            int numOfComputers = 0;

            foreach (var product in shop)
            {
                sumVat += product.GetVatPrice();
                sumPrice += product.Price;
                // starsza wersja testowania czy jest instancją
                if (product is Computer)
                {
                    Computer comp = (Computer)product;
                }
                // nowsza wersja testowania czy jest instacją
                Computer computer = product as Computer;
                Console.WriteLine(computer?.Vat);

            }

            Console.WriteLine(numOfComputers);
            Console.WriteLine(sumVat);

        }
    }

    abstract class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }

    class Student : Person
    {
        public int StudentID { get; set; }
    }

    class Lecturer : Person
    {
        public string AcademicDegree { get; set; }
    }

    class StudentLectureGroup 
    {
        public string Name { get; set; }

    }

    abstract class Product
    {
        public virtual decimal Price { get; init; }

        public abstract decimal GetVatPrice();
    }

    class Computer : Product
    {
        public decimal Vat { get; init; }

        public override decimal GetVatPrice()
        {
            return Price * Vat / 100m;
        }
    }

    class Paint : Product
    {
        public decimal Vat { get; init; }
        public decimal Capacity { get; init; }
        public decimal PriceUnit { get; init; }

        public override decimal GetVatPrice()
        {
            return Price * Capacity * Vat / 100m;
        }

        public override decimal Price
        {
            get
            {
                return PriceUnit * Capacity;
            }
        }
    }
}
