using System;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = Person.OfName("Adam");
            Console.WriteLine(person.FirstName);
            Money money = Money.Of(12, Currency.PLN) ?? Money.Of(0, Currency.PLN);
            Money result = 0.22m * money;
            Console.WriteLine(result.Value);
            money = Money.Of(5, money.Currency) + Money.Of(10, money.Currency);
            Console.WriteLine(money.Value);

            decimal price = money;
            double cost = (double) money;
            float c = (float) money;

            Console.WriteLine(money.Equals(Money.Of(12, Currency.PLN)));
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

    public class Money : IEquatable<Money>
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

        public static implicit operator decimal(Money money)
        {
            return money.Value;
        }

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
    }
}
