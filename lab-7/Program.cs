using System;
using System.Collections.Generic;

namespace lab_7
{
    class Student
    {
        public string Name { get; set; }
        public int Ects { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Student student &&
                   Name == student.Name &&
                   Ects == student.Ects;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Ects);
        }

        public override string ToString()
        {
            return $"{Name} {Ects}";
        }



    }
    class Program
    {
        static void Main(string[] args)
        {
            ICollection<string> names = new List<string>();
            names.Add("ewa");
            names.Add("karol");
            names.Add("adam");
            names.Add("adam");
            foreach (string name in names)
                Console.WriteLine(name);

            ICollection<Student> students = new List<Student>();
            students.Add(new Student() { Name = "Adam", Ects = 200});
            students.Add(new Student() { Name = "Ewa", Ects = 100 });
            students.Add(new Student() { Name = "Karol", Ects = 50 });
            students.Add(new Student() { Name = "Jacek", Ects = 300 });
            students.Add(new Student() { Name = "Karolina", Ects = 150 });
            students.Add(new Student() { Name = "Karolina", Ects = 150 });
            students.Add(new Student() { Name = "Ewelina", Ects = 170 });
            students.Add(new Student() { Name = "Jakub", Ects = 300 });
            foreach (Student student in students)
                Console.WriteLine(student);

            Student stu = new Student() { Name = "Ewelina", Ects = 170 };

            Console.WriteLine(students.Contains(stu));
            Console.WriteLine(students.Remove(stu));
            Console.WriteLine(students.Contains(stu));

            List<Student> list = (List<Student>)students;
            list.Insert(1, new Student() { Name = "Ania", Ects = 44 });
            list.RemoveAt(2);
            for(int i = 0; i < list.Count; i++)
                Console.WriteLine(list[i]);

            ISet<string> set = new HashSet<string>();
            Console.WriteLine("-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-");
            set.Add("ewa");
            set.Add("karol");
            set.Add("ania");
            Console.WriteLine(set.Contains("ewa"));
            Console.WriteLine(set.Remove("ewa"));
            foreach (string name in set)
                Console.WriteLine(name);
            ISet<Student> group = new HashSet<Student>(list);
            foreach (Student student in group)
                Console.WriteLine(student);
            ISet<int> s1 = new HashSet<int>(new int[] { 1, 2, 5, 6, 7 });
            ISet<int> s2 = new HashSet<int>(new int[] { 4, 7, 9, 8, 3 });
            s1.IntersectWith(s2);
            Console.WriteLine(string.Join(",", s1));
            Dictionary<string, string> phoneBook = new Dictionary<string, string>();
            phoneBook.Add("Adam", "234875345");
            phoneBook["ewa"] = "857097245";
            phoneBook["karol"] = "432098403";
            foreach (KeyValuePair<string, string> item in phoneBook)
                Console.WriteLine(item);

            string[] arr = { "ewa", "adam", "ewa", "karol", "ania", "ewa", "adam" };
            //podaj ile razy wystepuje każde imię w tabeli arr;
            Dictionary<string, int> pplCount = new Dictionary<string, int>();
            foreach(string name in arr)
            {
                if (pplCount.ContainsKey(name))
                    pplCount[name]++;
                else
                    pplCount[name] = 1;
            }

            foreach (KeyValuePair<string, int> ppl in pplCount)
                Console.WriteLine(ppl.Key + " " + ppl.Value);
        }
    }
}
