using System;
using System.Collections;
using System.Collections.Generic;

namespace lab_6
{
    record Ingredient
    {
        public double Calories { get; init; }
        public string Name { get; init; }
    }
    class Sandwitch: IEnumerable<Ingredient>
    {
        public Ingredient Bread { get; init; }
        public Ingredient Butter { get; init; }
        public Ingredient Salad { get; init; }
        public Ingredient Ham { get; init; }

        public IEnumerator<Ingredient> GetEnumerator()
        {

            //te 4 linijki zastępują klasę SandwitchEnumerator ale nie obsługują metody reset

            yield return Bread; //zwrócone w Current po pierwszym wywołaniu
            yield return Butter; // -||- po drugim wywołaniu
            yield return Salad;
            yield return Ham;

            //return new SandwitchEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }

    class Parking: IEnumerable<string>
    {
        private String[] _arr = {null,"GL789",null,"TK37898","KR6782",null,"WR66289",null};
        public string this[char slot]
        {
            get
            {
                //test poprawnosci, czy slot jest między 'A' a 'Z'
                return _arr[slot - 'A'];
            }
            set
            {
                _arr[slot - 'A'] = value;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            foreach(string car in _arr)
            {
                if(car != null)
                {
                    yield return car;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    class SandwitchEnumerator : IEnumerator<Ingredient>
    {
        private Sandwitch _sandwitch;
        int counter = -1;

        public SandwitchEnumerator(Sandwitch sandwitch)
        {
            _sandwitch = sandwitch;
        }

        public Ingredient Current
        {
            get 
            {
                return counter switch
                {
                    0 => _sandwitch.Bread,
                    1 => _sandwitch.Butter,
                    2 => _sandwitch.Ham,
                    3 => _sandwitch.Salad,
                    _ => null
                };
            }
        }

        object IEnumerator.Current => throw new NotImplementedException();

        public void Dispose()
        {
            
        }

        public bool MoveNext()
        {
            return ++counter < 4;
        }

        public void Reset()
        {
            
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Sandwitch sandwitch = new Sandwitch()
            {
                Bread = new Ingredient() { Calories = 100, Name = "Bułka wrocławska" },
                Ham = new Ingredient() { Calories = 400, Name = "Z kotła" },
                Salad = new Ingredient() { Calories = 10, Name = "Lodowa" },
                Butter = new Ingredient() { Calories = 120, Name = "Śmietankowe"}
            };

            IEnumerator<Ingredient> enumerator = sandwitch.GetEnumerator();
            while(enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }

            foreach(Ingredient ingredient in sandwitch)
            {
                Console.WriteLine(ingredient);
            }

            Parking parking = new Parking();
            foreach(string car in parking)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine(string.Join(", ", parking));
            Console.WriteLine(string.Join(", ", sandwitch));
            Console.WriteLine(parking['C']);
            parking['A'] = "TT23234";
            Console.WriteLine(string.Join(", ", parking));
        }
    }
}
