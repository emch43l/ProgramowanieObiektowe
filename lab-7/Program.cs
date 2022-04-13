using System;
using System.Collections.Generic;

namespace lab_7
{
    class Student
    {
        public string Name { get; set; }
        public int Ects { get; set; }

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
            students.Add(new Student() { Name = "Ewelina", Ects = 170 });
            students.Add(new Student() { Name = "Jakub", Ects = 300 });
            foreach (Student student in students)
                Console.WriteLine(student);

            Student stu = new Student() { Name = "Ewelina", Ects = 170 };

            Console.WriteLine(students.Contains(stu));
            Console.WriteLine(students.Remove(stu));
            Console.WriteLine(students.Contains(stu));
        }
    }
}
