using System;

namespace felmero
{
    public enum SeniorityLevel
    {
        Junior,
        Mid,
        Senior
    }

    internal abstract class Person
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        protected Person(int id, string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Name cannot be empty.", nameof(name));
            }

            Id = id;
            Name = name;
        }
    }

    internal abstract class Employee : Person
    {
        public decimal MonthlySalary { get; private set; }

        protected Employee(int id, string name, decimal monthlySalary)
            : base(id, name)
        {
            if (monthlySalary <= 0)
            {
                throw new ArgumentException("Salary must be positive.", nameof(monthlySalary));
            }

            MonthlySalary = monthlySalary;
        }

        public decimal GetHourlyRate()
        {
            return MonthlySalary / 160m;
        }

        public abstract string GetRoleName();
    }

    internal sealed class Programmer : Employee
    {
        public SeniorityLevel Seniority { get; private set; }

        public Programmer(int id, string name, decimal monthlySalary, SeniorityLevel seniority)
            : base(id, name, monthlySalary)
        {
            Seniority = seniority;
        }

        public override string GetRoleName()
        {
            switch (Seniority)
            {
                case SeniorityLevel.Junior:
                    return "Junior Programmer";
                case SeniorityLevel.Mid:
                    return "Mid Programmer";
                case SeniorityLevel.Senior:
                    return "Senior Programmer";
                default:
                    throw new ArgumentOutOfRangeException(nameof(Seniority));
            }
        }
    }

    internal class Director : Employee
    {
        public Director(int id, string name, decimal monthlySalary)
            : base(id, name, monthlySalary)
        {
        }

        public override string GetRoleName()
        {
            return "Director";
        }
    }

    internal sealed class HrDirector : Director
    {
        public HrDirector(int id, string name, decimal monthlySalary)
            : base(id, name, monthlySalary)
        {
        }

        public override string GetRoleName()
        {
            return "HR Director";
        }
    }

    internal sealed class ProjectManager : Employee
    {
        public ProjectManager(int id, string name, decimal monthlySalary)
            : base(id, name, monthlySalary)
        {
        }

        public override string GetRoleName()
        {
            return "Project Manager";
        }
    }
}
