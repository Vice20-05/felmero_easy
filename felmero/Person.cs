using System;

namespace felmero
{
    // Programmer level (Junior, Mid or Senior). Saved in the DB as text: "Junior" / "Mid" / "Senior".
    public enum SeniorityLevel
    {
        Junior,
        Mid,
        Senior
    }

    // Base of everyone. "abstract" = it can't be created directly (no new Person(...)),
    // only through a child class like Programmer.
    internal abstract class Person
    {
        // public get, private set -> anyone can read it, but only this class can change it (encapsulation)
        public int Id { get; private set; }
        public string Name { get; private set; }

        // protected constructor -> only child classes can call it (through ": base(id, name)")
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

    // Everyone who works at the company and gets a salary. Inherits Id + Name from Person.
    internal abstract class Employee : Person
    {
        // Monthly gross salary in RON
        public decimal MonthlySalary { get; private set; }

        protected Employee(int id, string name, decimal monthlySalary)
            : base(id, name)
        {
            // Salaries must be positive
            if (monthlySalary <= 0)
            {
                throw new ArgumentException("Salary must be positive.", nameof(monthlySalary));
            }

            MonthlySalary = monthlySalary;
        }

        // HourlyRate = MonthlySalary / 160  (160 working hours in a month)
        public decimal GetHourlyRate()
        {
            return MonthlySalary / 160m;
        }

        // Polymorphism: every child class gives its own answer.
        // e.g. Employee e = new Director(...);  e.GetRoleName() -> "Director"
        public abstract string GetRoleName();
    }

    // Developer. The only kind of employee that can be added to a project team.
    // sealed = no class can inherit from it.
    internal sealed class Programmer : Employee
    {
        // Every programmer must have a seniority
        public SeniorityLevel Seniority { get; private set; }

        public Programmer(int id, string name, decimal monthlySalary, SeniorityLevel seniority)
            : base(id, name, monthlySalary)
        {
            Seniority = seniority;
        }

        // The role text depends on the seniority. This same text goes into the Role column of the DB.
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

    // NOT sealed, because HrDirector inherits from it.
    // Can be listed, but cannot be put into a project team.
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

    // HrDirector -> Director -> Employee -> Person.
    // It overrides GetRoleName() again, so it says "HR Director" instead of "Director".
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

    // The only kind of employee that can be chosen as PM of a project.
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
