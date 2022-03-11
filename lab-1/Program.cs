using System;


namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            //Person person = Person.OfName("Adam");
            //Console.WriteLine(person.FirstName);
            //Money money = Money.Of(12, Currency.PLN) ?? Money.Of(0, Currency.PLN);
            //Money result = 0.22m * money;
            //Console.WriteLine(result.Value);
            //money = Money.Of(5, money.Currency) + Money.Of(10, money.Currency);
            //Console.WriteLine(money.Value);

            ////decimal price = money;
            //double cost = (double) money;
            //float c = (float) money;

            //Console.WriteLine(money.Equals(Money.Of(12, Currency.PLN)));

            //IEquatable<Money> ie = money;

            //Money[] prices =
            //{
            //    Money.Of(52, Currency.USD),
            //    Money.Of(100, Currency.PLN),
            //    Money.Of(51, Currency.EUR),
            //    Money.Of(30, Currency.EUR),
            //    Money.Of(99, Currency.PLN),
            //    Money.Of(45, Currency.USD)
            //};

            //Array.Sort(prices);

            //foreach(var p in prices)
            //{
            //    Console.WriteLine(p);
            //}

            Tank tank1 = new Tank(100);
            Tank tank2 = new Tank(200);

            tank1.refuel(100);
            tank2.refuel(100);

            Console.WriteLine(tank2);
            Console.WriteLine(tank1);

            Console.WriteLine(tank2.refuel(tank1, 100));

            Console.WriteLine(tank2);
            Console.WriteLine(tank1);

            Student[] students =
            {
                new Student("Adam","Achmed",1),
                new Student("Ceclila","Robertsoned",4),
                new Student("Adam","Robertsoned",2),
                new Student("Havier","Achmed",4),
                new Student("Michael","Robertson",3),
                new Student("Ewa","Robertson",3),
                new Student("Ewa","Robertson",2),
                new Student("Adam","Bigego",1),
                new Student("Adam","Robertsoned",3),
                new Student("Havier","Robertson",5),
                new Student("Michael","Bigego",4),
                new Student("Ciri","Robertson",2)
            };

            Array.Sort(students);

            foreach(var student in students)
            {
                Console.WriteLine(student);
            }

            Money moneyPLN = Money.Of(100, Currency.PLN);
            Money moneyUSD = moneyPLN.ToCurrency(Currency.USD, 0.23m);
            Console.WriteLine(moneyUSD);
            Console.WriteLine(moneyPLN);

        }
    }

    class Student: IComparable<Student>
    {
        public string Nazwisko { get; set; }
        public string Imie { get; set; }
        public decimal Srednia { get; set; }

        public Student(string imie, string nazwisko, decimal srednia)
        {
            Nazwisko = nazwisko;
            Imie = imie;
            Srednia = srednia;
        }

        public int CompareTo(Student other)
        {
            int curSurname = Nazwisko.CompareTo(other.Nazwisko);
            if (curSurname == 0)
            {
                int curName = Imie.CompareTo(other.Imie);
                if(curName == 0)
                    return Srednia.CompareTo(other.Srednia);
                else
                    return curName;
            }
            else
                return curSurname;
        }

        public override string ToString()
        {
            return $"Student: {Imie} {Nazwisko} {Srednia}";
        }
    }

    public class Tank
    {
        public readonly int Capacity;
        private int _level;
        public Tank(int capacity)
        {
            Capacity = capacity;
        }
        public int Level
        {
            get
            {
                return _level;
            }
            private set
            {
                if (value < 0 || value > Capacity)
                {
                    throw new ArgumentOutOfRangeException();
                }
                _level = value;
            }
        }

        public bool refuel(Tank sourceTank, int amount)
        {
            if (amount <= 0)
                return false;
            if(_level + amount <= Capacity && amount <= sourceTank.Level)
            {
                sourceTank.consume(amount);
                this.refuel(amount);
                return true;
            }
            return false;
        }

        public bool refuel(int amount)
        {
            if (amount < 0)
            {
                return false;
            }
            if (_level + amount > Capacity)
            {
                return false;
            }
            _level += amount;
            return true;
        }

        //public void refuel(int amount)
        //{
        //    if (amount < 0)
        //    {
        //        throw new ArgumentException("Argument cant be non positive!");
        //    }
        //    if (_level + amount > Capacity)
        //    {
        //        throw new ArgumentException("Argument is to large!");
        //    }
        //    _level += amount;
        //}

        //public int refuel(int amount)
        //{
        //    if (amount < 0 || _level == Capacity)
        //    {
        //        return 0;
        //    }
        //    if (_level + amount > Capacity)
        //    {
        //        int result = Capacity - _level;
        //        _level = Capacity;
        //        return result;
        //    }
        //    _level += amount;
        //    return amount;
        //}
        public bool consume(int amount)
        {
            if (amount <= 0)
                return false;
            if (amount > _level)
                return false;
            _level -= amount;
            return true;
        }

        public override string ToString()
        {
            return $"Tank: Capacity = {Capacity}, Level = {_level}";
        }
    }

    public class Person
    {
        private string firstName;

        private Person(string _firstName)
        {
            firstName = _firstName;
        }

        public static Person OfName(string name)
        {
            if(name != null && name.Length >= 2)
            {
                return new Person(name);
            }
            else
            {
                throw new ArgumentOutOfRangeException("Imię zbyt krótkie");
            }
        }

        public string FirstName
        { 
            get
            {
                return firstName;
            }
            set
            {
                if(value != null && value.Length >= 2)
                {
                    firstName = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Niepoprawne imię");
                }
            }
        }

        public override string ToString()
        {
            return $"Name: {firstName}";
        }
    }
    public enum Currency
    {
        PLN = 1,
        USD = 2,
        EUR = 3
    }

    public static class MoneyExtension
    {
        public static Money Percent(this Money money, decimal percent)
        {
            return Money.Of((money.Value * percent) / 100m, money.Currency) ?? throw new
            ArgumentException();
        }

        public static Money ToCurrency(this Money money, Currency currency, decimal exchange)
        {
            if (money.Currency == currency)
                return money;
            return Money.Of(Math.Round(money.Value / exchange,2), currency) ?? throw new ArgumentException();
        }
    }

    public class Money : IEquatable<Money>, IComparable<Money>
    {
        private readonly decimal _value;

        private readonly Currency _currency;

        private Money(decimal value, Currency currency)
        {
            _value = value;
            _currency = currency;
        }
        
        public decimal Value
        {
            get { return _value; }
        }

        public Currency Currency
        {
            get { return _currency; }
        }
        public static Money? Of(decimal value, Currency currency)
        {
            return value < 0 ? null : new Money(value, currency);
        }

        public static Money? OfWithExeption(decimal value, Currency currency)
        {
            if(value < 0)
            {
                throw new ArgumentException("Waluta nie może być mniejsza od 0");
            }
            else
            {
                return new Money(value, currency);
            }
        }
        // money *4 --> *(money,4)
        public static Money operator*(Money money, decimal factor)
        {
            return Money.Of(money.Value * factor, money.Currency);
        }

        public static Money operator *(decimal factor, Money money)
        {
            return Money.Of(money.Value * factor, money.Currency);
        }

        public static Money operator +(Money a, Money b)
        {
            IsSameCurrency(a, b);
            return Money.Of(a.Value + b.Value, a.Currency);
        }

        public static bool operator >(Money a, Money b)
        {
            IsSameCurrency(a, b);
            return a.Value > b.Value;
        }

        public static bool operator <(Money a, Money b)
        {
            IsSameCurrency(a, b);
            return a.Value < b.Value;
        }
        private static void IsSameCurrency(Money a, Money b)
        {
            if (a.Currency != b.Currency)
                throw new ArgumentException("Different currencies!");
        }

        // jawne

        //public static implicit operator decimal(Money money)
        //{
        //    return money.Value;
        //}

        // niejawne

        public static explicit operator double(Money money)
        {
            return (double)money.Value;
        }

        public override string ToString()
        {
            return $"Value: {_value}, Currency: {_currency}";
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Money);
        }

        public bool Equals(Money other)
        {
            return other != null &&
                   _value == other._value &&
                   _currency == other._currency;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_value, _currency);
        }

        public int CompareTo(Money other)
        {
            int curResult = _currency.CompareTo(other._currency);
            if (curResult == 0)
                return -_value.CompareTo(other._value);
            else
                return curResult;
        }

        
    }
}
