using System;

namespace lab_4
{
    class Program
    {
        static void Main(string[] args)
        {

            // ZAD ZROBIĆ ABSTRAKCYJNĄ KLASĘ SCOOTER (str 4)
            // ZAD ĆW 1,2 zrobic klasy na podstawie diagramu nr. 2

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

            IFly[] flyingObject = new IFly[2];
            Duck duck = new Duck();
            flyingObject[0] = duck;
            flyingObject[1] = new Hydroplane();
            ISwim[] swimmingObjects = new ISwim[2];
            swimmingObjects[0] = (ISwim)flyingObject[0];

            IAggregate aggregate;
            IIterator iterator = aggregate.CreateIterator();
            while(iterator.HasNext())
            {
                Console.WriteLine(iterator.GetNext());
            }
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

    interface IFly
    {
        void Fly();
    }

    interface ISwim
    {
        void Swim();
    }

    class Hydroplane : IFly, ISwim
    {
        public void Fly()
        {
            throw new NotImplementedException();
        }

        public void Swim()
        {
            throw new NotImplementedException();
        }
    }

    class Duck : ISwim, IFly
    {
        public void Fly()
        {
            throw new NotImplementedException();
        }

        public void Swim()
        {
            throw new NotImplementedException();
        }
    }

    interface IAggregate
    {
        IIterator CreateIterator();
    }

    interface IIterator
    {
        bool HasNext();
        int GetNext();

    }

}
