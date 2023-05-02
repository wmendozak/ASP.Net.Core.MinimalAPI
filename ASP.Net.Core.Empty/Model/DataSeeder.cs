namespace ASP.Net.Core.Empty.Model
{
    public class DataSeeder
    {
        private readonly EmployeeDbContext employeeDbContext;

        public DataSeeder(EmployeeDbContext employeeDbContext)
        {
            this.employeeDbContext = employeeDbContext;
        }

        public void Seed()
        {
            if (!employeeDbContext.Employees.Any())
            {
                var employees = new List<Employee>() { 
                    new Employee()
                    {
                        EmployeeId = "1",
                        Name = "Camila",
                        Citizenship = "Canadian"
                    },
                    new Employee()
                    {
                        EmployeeId = "2",
                        Name = "Andrea",
                        Citizenship = "Peruvian"
                    },
                    new Employee()
                    {
                        EmployeeId = "3",
                        Name = "Walter",
                        Citizenship = "Canadian"
                    }
                };

                employeeDbContext.Employees.AddRange(employees);
                employeeDbContext.SaveChanges();
            }
        }
    }
}
