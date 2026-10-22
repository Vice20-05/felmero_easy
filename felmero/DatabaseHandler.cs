using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace felmero
{
    internal class DatabaseHandler
    {
        private const string SqliteConnectionString = "Data Source=agency.db;Version=3;";

        private SQLiteConnection OpenConnection()
        {
            var connection = new SQLiteConnection(SqliteConnectionString);
            connection.Open();
            using (var pragma = new SQLiteCommand("PRAGMA foreign_keys = ON;", connection))
                pragma.ExecuteNonQuery();
            return connection;
        }

        public void EnsureCreated()
        {
            string sql = @"
                CREATE TABLE IF NOT EXISTS Employees (
                    Id            INTEGER PRIMARY KEY,
                    Name          TEXT    NOT NULL,
                    Role          TEXT    NOT NULL,
                    Seniority     TEXT    NULL,
                    MonthlySalary REAL    NOT NULL CHECK (MonthlySalary > 0)
                );
                CREATE TABLE IF NOT EXISTS Projects (
                    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name            TEXT    NOT NULL,
                    EstimatedBudget REAL    NOT NULL,
                    CalculatedCost  REAL    NOT NULL,
                    PmEmployeeId    INTEGER NOT NULL REFERENCES Employees(Id),
                    PmHours         REAL    NOT NULL
                );
                CREATE TABLE IF NOT EXISTS ProjectAssignments (
                    ProjectId      INTEGER NOT NULL REFERENCES Projects(Id),
                    EmployeeId     INTEGER NOT NULL REFERENCES Employees(Id),
                    AllocatedHours REAL    NOT NULL CHECK (AllocatedHours > 0),
                    PRIMARY KEY (ProjectId, EmployeeId)
                );";
            using (var connection = OpenConnection())
            using (var command = new SQLiteCommand(sql, connection))
                command.ExecuteNonQuery();
        }

        public int GetEmployeeCount()
        {
            using (var connection = OpenConnection())
            using (var command = new SQLiteCommand("SELECT COUNT(*) FROM Employees", connection))
                return Convert.ToInt32(command.ExecuteScalar());
        }

        public bool InsertEmployee(int id, string name, string role, string seniority, decimal monthlySalary)
        {
            using (var connection = OpenConnection())
            using (var command = new SQLiteCommand("INSERT INTO Employees (Id, Name, Role, Seniority, MonthlySalary) VALUES (@Id, @Name, @Role, @Seniority, @MonthlySalary)", connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@Role", role);
                command.Parameters.AddWithValue("@Seniority", seniority);
                command.Parameters.AddWithValue("@MonthlySalary", monthlySalary);
                try
                {
                    return command.ExecuteNonQuery() > 0;
                }
                catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint)
                {
                    // Handle unique constraint violation (e.g., duplicate Id)
                    return false;
                }
            }
        }
        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            const string sql = "SELECT Id, Name, Role, Seniority, MonthlySalary FROM Employees ORDER BY Id";

            using (var connection = OpenConnection())
            using (var command = new SQLiteCommand(sql, connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = Convert.ToInt32(reader["Id"]);
                    string name = Convert.ToString(reader["Name"]);
                    string role = Convert.ToString(reader["Role"]);
                    string seniority = reader.IsDBNull(reader.GetOrdinal("Seniority"))
                        ? null
                        : Convert.ToString(reader["Seniority"]);
                    decimal monthlySalary = Convert.ToDecimal(reader["MonthlySalary"]);

                    employees.Add(CreateEmployee(id, name, role, seniority, monthlySalary));
                }
            }

            return employees;
        }

        private static Employee CreateEmployee(int id, string name, string role, string seniority, decimal monthlySalary)
        {
            if (string.Equals(role, "HR Director", StringComparison.OrdinalIgnoreCase))
                return new HrDirector(id, name, monthlySalary);

            if (string.Equals(role, "Director", StringComparison.OrdinalIgnoreCase))
                return new Director(id, name, monthlySalary);

            if (string.Equals(role, "Project Manager", StringComparison.OrdinalIgnoreCase))
                return new ProjectManager(id, name, monthlySalary);

            if (string.Equals(role, "Programmer", StringComparison.OrdinalIgnoreCase) ||
                role.EndsWith(" Programmer", StringComparison.OrdinalIgnoreCase))
            {
                SeniorityLevel level;
                string value = string.IsNullOrWhiteSpace(seniority) ? role : seniority;

                if (string.Equals(value, "Junior Programmer", StringComparison.OrdinalIgnoreCase))
                    level = SeniorityLevel.Junior;
                else if (string.Equals(value, "Mid Programmer", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(value, "MidLevel", StringComparison.OrdinalIgnoreCase))
                    level = SeniorityLevel.Mid;
                else if (string.Equals(value, "Senior Programmer", StringComparison.OrdinalIgnoreCase))
                    level = SeniorityLevel.Senior;
                else if (!Enum.TryParse(value, true, out level) ||
                         !Enum.IsDefined(typeof(SeniorityLevel), level))
                    throw new InvalidOperationException("Unknown programmer seniority: " + value);

                return new Programmer(id, name, monthlySalary, level);
            }

            throw new InvalidOperationException("Unknown employee role: " + role);
        }
    }
}
