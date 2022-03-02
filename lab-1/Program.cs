using System;

namespace lab_1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person person = Person.OfName("Adam");
            Console.WriteLine(person.FirstName);
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
    }
}
