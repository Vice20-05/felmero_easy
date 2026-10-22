using System;
using System.Collections.Generic;

namespace felmero
{
    internal static class EmployeeGenerator
    {
        private static readonly string[] FirstNames =
            { "Anna", "Bence", "Csaba", "Dora", "Erik", "Flora", "Gabor", "Hanna", "Istvan", "Julia","Mark","Istvan", "Alex", "Ben", "Cathy" };

        private static readonly string[] LastNames =
            { "Kovacs", "Szabo", "Nagy", "Toth", "Varga", "Kiss", "Molnar", "Farkas", "Balogh", "Papp","Pop","Iulian","Vargyas","Csiki" };

        public const int TotalEmployees = 100;

        // Ids are always 1..100, so generating twice cannot create duplicates.
        public static List<Employee> GenerateEmployees()
        {
            var random = new Random(42);
            var employees = new List<Employee>();
            int nextId = 1;

            for (int i = 0; i < 30; i++, nextId++)
                employees.Add(new Programmer(nextId, MakeName(nextId), random.Next(6000, 9001), SeniorityLevel.Junior));

            for (int i = 0; i < 20; i++, nextId++)
                employees.Add(new Programmer(nextId, MakeName(nextId), random.Next(10000, 14001), SeniorityLevel.Mid));

            for (int i = 0; i < 10; i++, nextId++)
                employees.Add(new Programmer(nextId, MakeName(nextId), random.Next(15000, 20001), SeniorityLevel.Senior));

            for (int i = 0; i < 10; i++, nextId++)
                employees.Add(new ProjectManager(nextId, MakeName(nextId), random.Next(14000, 18001)));

            for (int i = 0; i < 2; i++, nextId++)
                employees.Add(new Director(nextId, MakeName(nextId), random.Next(20000, 30001)));

            for (int i = 0; i < 2; i++, nextId++)
                employees.Add(new HrDirector(nextId, MakeName(nextId), random.Next(20000, 30001)));

            // Fill up to 100 with programmers of mixed seniority
            while (nextId <= TotalEmployees)
            {
                var seniority = (SeniorityLevel)(nextId % 3);
                employees.Add(new Programmer(nextId, MakeName(nextId), random.Next(6000, 20001), seniority));
                nextId++;
            }

            return employees;
        }

        private static string MakeName(int id)
        {
            return LastNames[(id / 10) % LastNames.Length] + " " + FirstNames[id % FirstNames.Length];
        }
    }
}
