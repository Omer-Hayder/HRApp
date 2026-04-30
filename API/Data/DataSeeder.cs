using API.Entities;

namespace API.Data
{
    public static class DataSeeder
    {
        public static void Seed(AppDbContext context)
        {
            if (context.Employees.Any()) return;

            var random = new Random();

            var departments = new List<Department>
        {
            new() { Name = "IT", Location = "Riyadh" },
            new() { Name = "HR", Location = "Jeddah" },
            new() { Name = "Finance", Location = "Dammam" }
        };

            context.Departments.AddRange(departments);

            var projects = new List<Project>
        {
            new() { Name = "ERP" },
            new() { Name = "E-Commerce" },
            new() { Name = "Mobile App" }
        };

            context.Projects.AddRange(projects);

            var employees = new List<Employee>();

            for (int i = 1; i <= 100; i++)
            {
                Employee emp;

                if (i % 2 == 0)
                {
                    emp = new PermanentEmployee
                    {
                        Name = $"Employee {i}",
                        Salary = random.Next(5000, 15000),
                        AnnualSalary = random.Next(60000, 200000),
                        DepartmentId = random.Next(1, 4)
                    };
                }
                else
                {
                    emp = new ContractEmployee
                    {
                        Name = $"Employee {i}",
                        Salary = random.Next(3000, 10000),
                        HoursWorked = random.Next(80, 200),
                        HourlyPay = random.Next(20, 100),
                        DepartmentId = random.Next(1, 4)
                    };
                }

                employees.Add(emp);
            }

            context.Employees.AddRange(employees);
            context.SaveChanges();

            // 🔥 Assign Projects
            var employeeProjects = new List<EmployeeProject>();

            foreach (var emp in context.Employees.ToList())
            {
                int projectCount = random.Next(1, 3);
                var assignedProjects = new HashSet<int>();

                for (int j = 0; j < projectCount; j++)
                {
                    int projectId;

                    do
                    {
                        projectId = random.Next(1, 4);
                    }
                    while (!assignedProjects.Add(projectId)); // يمنع التكرار
                    employeeProjects.Add(new EmployeeProject
                    {
                        EmployeeId = emp.Id,
                        ProjectId = random.Next(1, 4),
                        HoursWorked = random.Next(20, 200)
                    });
                }
            }

            context.EmployeeProjects.AddRange(employeeProjects);
            context.SaveChanges();
        }
    }
}
