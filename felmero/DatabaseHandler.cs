using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace felmero
{
    // Every SQLite operation of the app is in this class (ADO.NET: connection -> command -> execute).
    // All SQL with values uses @parameters, never string concatenation (protects against SQL injection).
    internal class DatabaseHandler
    {
        // agency.db is a relative path -> the file is next to felmero.exe (bin\Debug).
        // SQLite creates the file automatically the first time it is opened.
        private const string SqliteConnectionString = "Data Source=agency.db;Version=3;";

        // Opens a new connection. Callers put it in "using", so it is always closed afterwards.
        private SQLiteConnection OpenConnection()
        {
            var connection = new SQLiteConnection(SqliteConnectionString);
            connection.Open();
            // SQLite ignores REFERENCES (foreign keys) unless this is switched on for each connection
            using (var pragma = new SQLiteCommand("PRAGMA foreign_keys = ON;", connection))
                pragma.ExecuteNonQuery();
            return connection;
        }

        // Creates the 3 tables of the app. "IF NOT EXISTS" -> safe to call at every startup,
        // existing data is never deleted.
        //  - Employees.Seniority can be NULL: only programmers have one
        //  - Projects.Id is AUTOINCREMENT: SQLite gives the next Id
        //  - ProjectAssignments has PRIMARY KEY (ProjectId, EmployeeId): a developer only once per project
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
                command.ExecuteNonQuery();   // NonQuery = command that returns no rows (CREATE, INSERT...)
        }

        // How many employees are in the DB. ExecuteScalar = returns a single value.
        public int GetEmployeeCount()
        {
            using (var connection = OpenConnection())
            using (var command = new SQLiteCommand("SELECT COUNT(*) FROM Employees", connection))
                return Convert.ToInt32(command.ExecuteScalar());
        }

        // Inserts one employee. Returns true if the row was really inserted,
        // false if an employee with this Id already exists (so Generate twice = no duplicates).
        // Form1 writes the DB_INSERT trace line only when this returns true.
        // seniority = null for non-programmers -> stored as NULL.
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
                    // ExecuteNonQuery returns how many rows changed -> 1 = inserted
                    return command.ExecuteNonQuery() > 0;
                }
                catch (SQLiteException ex) when (ex.ResultCode == SQLiteErrorCode.Constraint)
                {
                    // Handle unique constraint violation (e.g., duplicate Id)
                    return false;
                }
            }
        }

        // Reads every employee and turns each row back into the right C# object
        // (Programmer / ProjectManager / Director / HrDirector), so the form can use LINQ on them.
        public List<Employee> GetAllEmployees()
        {
            var employees = new List<Employee>();
            const string sql = "SELECT Id, Name, Role, Seniority, MonthlySalary FROM Employees ORDER BY Id";

            using (var connection = OpenConnection())
            using (var command = new SQLiteCommand(sql, connection))
            using (var reader = command.ExecuteReader())   // reader = goes through the result row by row
            {
                while (reader.Read())   // false when there are no more rows
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

        // Role text from the DB -> the matching C# class.
        // "HR Director" is checked before "Director", so an HR director is not loaded as a plain Director.
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
                // Normally Seniority is "Junior"/"Mid"/"Senior" -> Enum.TryParse below handles it.
                // If it is empty, the level is worked out from the role text instead.
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

        // Saves an accepted project and its developers. Returns the new project Id.
        // Transaction = all or nothing: if one assignment fails, the project is not saved either.
        public long SaveProject(string name, decimal estimatedBudget, decimal calculatedCost,
                                int pmEmployeeId, decimal pmHours, List<TeamMember> team)
        {
            using (var connection = OpenConnection())
            using (var transaction = connection.BeginTransaction())
            {
                long projectId;

                // 1) the project row; last_insert_rowid() gives back the Id SQLite just generated
                using (var command = new SQLiteCommand(
                    "INSERT INTO Projects (Name, EstimatedBudget, CalculatedCost, PmEmployeeId, PmHours) " +
                    "VALUES (@Name, @EstimatedBudget, @CalculatedCost, @PmEmployeeId, @PmHours); " +
                    "SELECT last_insert_rowid();", connection, transaction))
                {
                    command.Parameters.AddWithValue("@Name", name);
                    command.Parameters.AddWithValue("@EstimatedBudget", estimatedBudget);
                    command.Parameters.AddWithValue("@CalculatedCost", calculatedCost);
                    command.Parameters.AddWithValue("@PmEmployeeId", pmEmployeeId);
                    command.Parameters.AddWithValue("@PmHours", pmHours);
                    projectId = Convert.ToInt64(command.ExecuteScalar());
                }

                // 2) one ProjectAssignments row per developer
                foreach (TeamMember member in team)
                {
                    using (var command = new SQLiteCommand(
                        "INSERT INTO ProjectAssignments (ProjectId, EmployeeId, AllocatedHours) " +
                        "VALUES (@ProjectId, @EmployeeId, @AllocatedHours)", connection, transaction))
                    {
                        command.Parameters.AddWithValue("@ProjectId", projectId);
                        command.Parameters.AddWithValue("@EmployeeId", member.Programmer.Id);
                        command.Parameters.AddWithValue("@AllocatedHours", member.AllocatedHours);
                        command.ExecuteNonQuery();
                    }
                }

                // 3) only now is everything really written to the file
                transaction.Commit();
                return projectId;
            }
        }

        // Loads the accepted projects for the grid (PM name + developers with hours joined in)
        public DataTable GetAcceptedProjects()
        {
            const string sql = @"
                SELECT p.Id,
                       p.Name,
                       p.EstimatedBudget,
                       p.CalculatedCost,
                       pm.Name AS ProjectManager,
                       p.PmHours,
                       (SELECT group_concat(e.Name || ' (' || a.AllocatedHours || 'h)', ', ')
                          FROM ProjectAssignments a
                          JOIN Employees e ON e.Id = a.EmployeeId
                         WHERE a.ProjectId = p.Id) AS Developers
                  FROM Projects p
                  JOIN Employees pm ON pm.Id = p.PmEmployeeId
                 ORDER BY p.Id";

            var table = new DataTable();
            using (var connection = OpenConnection())
            using (var adapter = new SQLiteDataAdapter(sql, connection))
                adapter.Fill(table);
            return table;
        }
    }
}
