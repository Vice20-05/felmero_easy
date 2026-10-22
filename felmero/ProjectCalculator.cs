using System.Collections.Generic;
using System.Linq;

namespace felmero
{
    // One developer in the team + how many hours he/she works on the project
    internal class TeamMember
    {
        public Programmer Programmer { get; private set; }
        public decimal AllocatedHours { get; private set; }

        public TeamMember(Programmer programmer, decimal allocatedHours)
        {
            Programmer = programmer;
            AllocatedHours = allocatedHours;
        }

        // This text is what the ListBox shows
        public override string ToString()
        {
            return Programmer.Name + " (" + Programmer.GetRoleName() + ") - " + AllocatedHours + " óra";
        }
    }

    // All cost rules in one place (LINQ runs on the C# list, not in SQL)
    internal static class ProjectCalculator
    {
        // PM works 20% of the developer hours
        public const decimal PmHourRatio = 0.20m;

        // TotalDeveloperHours = sum of AllocatedHours
        public static decimal GetTotalDeveloperHours(List<TeamMember> team)
        {
            return team.Sum(member => member.AllocatedHours);
        }

        // PmHours = TotalDeveloperHours * 0.20
        public static decimal GetPmHours(List<TeamMember> team)
        {
            return GetTotalDeveloperHours(team) * PmHourRatio;
        }

        // CalculatedCost = sum(HourlyRate * AllocatedHours) + PM HourlyRate * PmHours
        public static decimal CalculateCost(List<TeamMember> team, ProjectManager projectManager)
        {
            decimal developerCost = team.Sum(member => member.Programmer.GetHourlyRate() * member.AllocatedHours);
            decimal pmCost = projectManager == null ? 0 : projectManager.GetHourlyRate() * GetPmHours(team);
            return developerCost + pmCost;
        }

        // Acceptable only if: there is a PM, at least 1 developer, and the cost fits in the budget
        public static bool CanAccept(List<TeamMember> team, ProjectManager projectManager, decimal estimatedBudget)
        {
            return projectManager != null
                && team.Count > 0
                && CalculateCost(team, projectManager) <= estimatedBudget;
        }
    }
}
