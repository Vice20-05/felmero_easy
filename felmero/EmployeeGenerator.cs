using System;
using System.Collections.Generic;

namespace felmero
{
    // Creates the 100 employees for the "Generate employees" button.
    // static class = no object needed, call it as EmployeeGenerator.GenerateEmployees().
    internal static class EmployeeGenerator
    {
        // Names are built from these two lists (last name + first name)
        private static readonly string[] FirstNames =
            { "Anna", "Bence", "Csaba", "Dora", "Erik", "Flora", "Gabor", "Hanna", "Istvan", "Julia","Mark","Istvan", "Alex", "Ben", "Cathy" };

        private static readonly string[] LastNames =
            { "Kovacs", "Szabo", "Nagy", "Toth", "Varga", "Kiss", "Molnar", "Farkas", "Balogh", "Papp","Pop","Iulian","Vargyas","Csiki" };

        public const int TotalEmployees = 100;

        // Ids are always 1..100, so generating twice cannot create duplicates.
        // The role counts are FIXED (not random), so the 100  minimums are always met:
        // 30 Junior, 20 Mid, 10 Senior, 10 PM, 2 Director, 2 HR Director = 74, + 26 programmers = 100.
        public static List<Employee> GenerateEmployees()
        {
            // Fixed seed (42) -> the same "random" salaries every time
            var random = new Random(42);
            var employees = new List<Employee>();
            int nextId = 1;

            // random.Next(min, max): max is exclusive, so 9001 means up to 9000 RON
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
                // nextId % 3 is 0, 1 or 2 -> Junior, Mid, Senior in turn
                var seniority = (SeniorityLevel)(nextId % 3);
                employees.Add(new Programmer(nextId, MakeName(nextId), random.Next(6000, 20001), seniority));
                nextId++;
            }

            return employees;
        }

        // Picks a last name and a first name based on the Id, e.g. Id 1 -> "Kovacs Bence"
        private static string MakeName(int id)
        {
            return LastNames[(id / 10) % LastNames.Length] + " " + FirstNames[id % FirstNames.Length];
        }
    }
}
