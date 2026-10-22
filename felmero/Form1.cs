using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace felmero
{
    public partial class Form1 : Form
    {
        private readonly DatabaseHandler db = new DatabaseHandler();

        // Everyone loaded from the DB (C# objects -> LINQ works on this list)
        private List<Employee> employees = new List<Employee>();

        // The developers of the project being planned right now (not saved until Accept)
        private readonly List<TeamMember> team = new List<TeamMember>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Creates agency.db and the 3 tables if they don't exist yet
            db.EnsureCreated();

            LoadEmployees();
            LoadAcceptedProjects();
            Recalculate();
        }

        // ---------------------------------------------------------------
        // Employees tab
        // ---------------------------------------------------------------

        private void buttonGenerateEmployees_Click(object sender, EventArgs e)
        {
            int insertedCount = 0;

            foreach (Employee employee in EmployeeGenerator.GenerateEmployees())
            {
                // Only programmers have seniority, everyone else gets NULL in the DB
                var programmer = employee as Programmer;
                string seniority = programmer != null ? programmer.Seniority.ToString() : null;

                // false = this Id is already in the DB -> no duplicate, no log line
                bool inserted = db.InsertEmployee(employee.Id, employee.Name, employee.GetRoleName(),
                                                  seniority, employee.MonthlySalary);
                if (inserted)
                {
                    insertedCount++;
                    // Log only AFTER the insert succeeded
                    Trace.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} DB_INSERT table=Employees id={employee.Id}");
                }
            }

            LoadEmployees();

            if (insertedCount == 0)
                MessageBox.Show("A munkatársak már le vannak generálva, nem készült duplikátum.");
            else
                MessageBox.Show(insertedCount + " új munkatárs mentve.");
        }

        // Reads everyone from the DB and fills the grid + the two combo boxes
        private void LoadEmployees()
        {
            employees = db.GetAllEmployees();

            // LINQ Select: pick only the columns we want to show (property names stay in English)
            dataGridViewEmployees.DataSource = employees.Select(employee => new
            {
                employee.Id,
                employee.Name,
                Role = employee.GetRoleName(),
                Seniority = employee is Programmer ? ((Programmer)employee).Seniority.ToString() : "-",
                employee.MonthlySalary
            }).ToList();

            // Hungarian captions are allowed on the UI, so only the column headers are translated
            dataGridViewEmployees.Columns["Name"].HeaderText = "Név";
            dataGridViewEmployees.Columns["Role"].HeaderText = "Munkakör";
            dataGridViewEmployees.Columns["Seniority"].HeaderText = "Senioritás";
            dataGridViewEmployees.Columns["MonthlySalary"].HeaderText = "Havi bruttó bér (RON)";

            labelEmployeeCount.Text = "Munkatársak: " + employees.Count;

            // LINQ OfType: only ProjectManager objects -> Director/HrDirector cannot be chosen as PM
            comboBoxProjectManager.DataSource = employees.OfType<ProjectManager>().ToList();
            comboBoxProjectManager.DisplayMember = "Name";

            // Only programmers can be developers in the team.
            // LINQ Where: keeps only the employees that are Programmer objects,
            // Cast: turns them from Employee into Programmer,
            // OrderBy / ThenBy: Junior -> Mid -> Senior, and alphabetically inside each level.
            comboBoxProgrammer.DataSource = employees
                .Where(employee => employee is Programmer)
                .Cast<Programmer>()
                .OrderBy(programmer => programmer.Seniority)
                .ThenBy(programmer => programmer.Name)
                .ToList();
            comboBoxProgrammer.DisplayMember = "Name";
        }

        // ---------------------------------------------------------------
        // Projects tab
        // ---------------------------------------------------------------

        private void buttonAddMember_Click(object sender, EventArgs e)
        {
            var programmer = comboBoxProgrammer.SelectedItem as Programmer;
            if (programmer == null)
                return;

            // A developer can be in the project only once (LINQ Any)
            if (team.Any(member => member.Programmer.Id == programmer.Id))
            {
                MessageBox.Show("Ez a fejlesztő már a csapatban van.");
                return;
            }

            decimal hours = numericUpDownHours.Value;
            if (hours <= 0)
            {
                MessageBox.Show("Az óraszám legyen pozitív.");
                return;
            }

            team.Add(new TeamMember(programmer, hours));
            RefreshTeamList();
        }

        private void buttonRemoveMember_Click(object sender, EventArgs e)
        {
            var member = listBoxTeam.SelectedItem as TeamMember;
            if (member == null)
                return;

            team.Remove(member);
            RefreshTeamList();
        }

        // Automatic recalculation when the budget or the PM changes
        private void numericUpDownBudget_ValueChanged(object sender, EventArgs e)
        {
            Recalculate();
        }

        private void comboBoxProjectManager_SelectedIndexChanged(object sender, EventArgs e)
        {
            Recalculate();
        }

        private void RefreshTeamList()
        {
            // Rebind so the ListBox shows the current team (TeamMember.ToString() is the text)
            listBoxTeam.DataSource = null;
            listBoxTeam.DataSource = team;
            Recalculate();
        }

        // Shows the cost, the red/green result, and enables Accept only if the project is acceptable
        private void Recalculate()
        {
            var projectManager = comboBoxProjectManager.SelectedItem as ProjectManager;
            decimal budget = numericUpDownBudget.Value;

            decimal developerHours = ProjectCalculator.GetTotalDeveloperHours(team);
            decimal pmHours = ProjectCalculator.GetPmHours(team);
            decimal cost = ProjectCalculator.CalculateCost(team, projectManager);

            labelCost.Text = "Fejlesztői órák: " + developerHours + "   PM órák: " + pmHours +
                             "\nSzámított költség: " + cost.ToString("N2") + " RON";

            if (projectManager == null || team.Count == 0)
            {
                labelResult.ForeColor = Color.Red;
                labelResult.Text = "Hiányos csapat: kell egy PM és legalább egy fejlesztő.";
            }
            else if (cost > budget)
            {
                labelResult.ForeColor = Color.Red;
                labelResult.Text = "A projekt nem fér bele a költségkeretbe.\nKülönbség: " +
                                   (cost - budget).ToString("N2") + " RON";
            }
            else
            {
                labelResult.ForeColor = Color.Green;
                labelResult.Text = "A projekt belefér a költségkeretbe.\nMaradék: " +
                                   (budget - cost).ToString("N2") + " RON";
            }

            buttonAcceptProject.Enabled = ProjectCalculator.CanAccept(team, projectManager, budget);
        }

        private void buttonAcceptProject_Click(object sender, EventArgs e)
        {
            string projectName = textBoxProjectName.Text.Trim();
            var projectManager = comboBoxProjectManager.SelectedItem as ProjectManager;
            decimal budget = numericUpDownBudget.Value;

            if (projectName.Length == 0)
            {
                MessageBox.Show("Add meg a projekt nevét.");
                return;
            }

            // Check again here - an over-budget or incomplete project must never be saved/logged
            if (!ProjectCalculator.CanAccept(team, projectManager, budget))
            {
                MessageBox.Show("A projekt nem fogadható el.");
                return;
            }

            decimal cost = ProjectCalculator.CalculateCost(team, projectManager);
            decimal pmHours = ProjectCalculator.GetPmHours(team);

            long projectId = db.SaveProject(projectName, budget, cost, projectManager.Id, pmHours, team);

            // Logged only after SaveProject succeeded (it throws on error, so we never get here then)
            string developers = string.Join(", ", team.Select(member => member.Programmer.Id + ":" + member.AllocatedHours + "h"));
            Trace.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} DB_INSERT table=Projects id={projectId}");
            Trace.WriteLine($"{DateTime.Now:yyyy-MM-dd HH:mm:ss} PROJECT_ACCEPTED id={projectId} name={projectName} " +
                            $"budget={budget} cost={cost:0.00} pm={projectManager.Id} pmHours={pmHours} developers=[{developers}]");

            MessageBox.Show("Projekt elfogadva és mentve (Id: " + projectId + ").");

            // Reset the form for the next project
            team.Clear();
            textBoxProjectName.Clear();
            RefreshTeamList();
            LoadAcceptedProjects();
        }

        private void LoadAcceptedProjects()
        {
            dataGridViewProjects.DataSource = db.GetAcceptedProjects();
        }
    }
}
