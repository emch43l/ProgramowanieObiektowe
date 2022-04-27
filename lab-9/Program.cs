using System;
using System.Collections.Generic;
using System.Linq;

namespace lab_9
{
    record Student(string name, char group, int ects);
    class Program
    {
        static void Main(string[] args)
        {
            int[] ints = { 4, 6, 1, 4, 7, 8, 3, 4, 9 };

            Predicate<int> enumPredicate = n =>
            {
                Console.WriteLine("Obliczanie predykatu dla n " + n);
                return n % 2 == 1;
            };
            IEnumerable<int> enumerable = from n in ints // foreach
                             where enumPredicate.Invoke(n) // if
                             select n; // return

            enumerable = from n in enumerable
                         where n > 5
                         select n;

            int limit = 5;
            enumerable = from n in ints
                         where n % 2 == 1 && n > limit
                         select n;


            string[] words = { "aa", "bb", "ccc", "fff", "ee", "ggggg" };

            IEnumerable<string> filteredWords = from n in words
                                                where n.Length == 2
                                                select n;


            Console.WriteLine(string.Join(", ", filteredWords));

            Console.WriteLine(string.Join(", ", enumerable));

            Console.WriteLine(string.Join(", ",
                from s in words
                orderby s descending
                select s
                ));

            IEnumerable<int> power = from n in ints
                                     orderby n descending
                                     select n*n;

            Console.WriteLine(string.Join(", ", power));

            Student[] students =
            {
                new Student("Ewa",'A',54),
                new Student("Karol",'B',31),
                new Student("Ewa",'B',38),
                new Student("Tomek",'B',44),
                new Student("Kasia",'A',24),
                new Student("Karol",'A',34)
            };

            //IEnumerable<IGrouping<char, Student>> teams = from s in students
            //                                              group s by s.@group into team
            //                                              select (team.Key,team.Count());

            //foreach(var item in teams)
            //{
            //    Console.WriteLine(item.Key + " " + string.Join("\n", item));
            //}

            string sentense = "ala ma kota ala lubi koty tomek lubi psy";

            Console.WriteLine(string.Join(",", from s in sentense.Split(' ')
            group s by s into word
            select ((string)word.Key, word.Count())));

            String.Join(", ",ints.Where(n => n % 2 != 1).OrderBy(x => x).Select(y => y*y));
            Console.WriteLine(string.Join(", ", enumerable));
            students.GroupBy(student => student.@group).Select(gr => (gr.Key, gr.Count()));
            IOrderedEnumerable<Student> sorted = students.OrderBy(student => student.@name).ThenByDescending(student => student.@ects);
            Console.WriteLine(string.Join("\n", sorted));

            IOrderedEnumerable<string> sortedStrings = words.OrderBy(word => word.Length).ThenBy(word => word);
            Console.WriteLine(string.Join("\n", sortedStrings));

            Console.WriteLine(string.Join(", ",Enumerable.Range(0, 10).Where(n => n % 2 == 0).Sum()));
            Console.WriteLine(string.Join(", ", Enumerable.Range(0, 10).Where(n => n % 2 != 0).Select(n => (int)Math.Pow(n, 2))));

            Student second = students.OrderByDescending(s => s.@ects).ElementAtOrDefault(10);
            Console.WriteLine(second);
            Student studentA = students.FirstOrDefault(s => s.@name.StartsWith("A"));
            Console.WriteLine(studentA);

            Random random = new Random();
            random.Next(5);

            int[] randInt = Enumerable.Range(0, 99).Select(n => random.Next(10)).ToArray();

        }
    }
}
