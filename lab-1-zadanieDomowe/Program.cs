using System;
using System.Collections.Generic;

namespace lab_1_zadanieDomowe
{
    class Program
    {
        static void Main(string[] args)
        {
            Coin one = Coin.one();
            Coin two = Coin.two();
            Coin five = Coin.five();

            Console.WriteLine(Coin.five() + Coin.two());

            Coin[] tab = Coin.Of(0);

            DumpCoins(tab);
        }

        static void DumpCoins(Coin[] coins)
        {
            foreach(var coin in coins)
            {
                Console.Write($"{coin.number} ");
            }
        }
    }

    public class Coin
    {
        /// <summary>
        /// Creates new Coin object with value of 1
        /// </summary>
        /// <returns>New Coin object with value of 1</returns>
        public static Coin one()
        {
            return new Coin(1);
        }

        /// <summary>
        /// Creates new Coin object with value of 2
        /// </summary>
        /// <returns>New Coin object with value of 2</returns>
        public static Coin two()
        {
            return new Coin(2);
        }

        /// <summary>
        /// Creates new Coin object with value of 5
        /// </summary>
        /// <returns>New Coin object with value of 5</returns>
        public static Coin five()
        {
            return new Coin(5);
        }

        private int _number;

        public int number
        {
            get { return _number; }
        }
        private Coin(int number)
        {
            _number = number;
        }

        /// <summary>
        /// Sums two coin object and returns addition result
        /// </summary>
        /// <param name="a">Coin object</param>
        /// <param name="b">Coin object</param>
        /// <returns>Addidion result</returns>
        public static int operator +(Coin a, Coin b)
        {
            return (int)a.number + b.number;
        }

        /// <summary>
        /// Returns table of coin objects whose sum of denominations is equal to the parameter with total value
        /// </summary>
        /// <param name="value">integer total value</param>
        /// <returns>Table of coin objects</returns>
        /// <exception cref="ArgumentException">throw for argument less than 0</exception>

        public static Coin[] Of(int value)
        {
            if (value < 0)
                throw new ArgumentException("Given value cannot be lower than 0");

            List<Coin> coinList = new List<Coin>();

            while(value >= 0)
            {
                if (value >= 5)
                {
                    coinList.Add(Coin.five());
                    value -= 5;
                    continue;
                }

                if(value >= 2)
                {
                    coinList.Add(Coin.two());
                    value -= 2;
                    continue;
                }

                if(value >= 1)
                {
                    coinList.Add(Coin.one());
                    value -= 1;
                    continue;
                }
            }

            return coinList.ToArray();
        }
    }
}
