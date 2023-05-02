namespace ASP.Net.Core.Empty.Model
{
    public class DataRepository : IDataRepository
    {
        private readonly EmployeeDbContext db;

        public DataRepository(EmployeeDbContext employeeDbContext)
        {
            this.db = employeeDbContext;
        }

        public List<Employee> GetEmployees() => db.Employees.ToList();

        public Employee GetEmployee(string id) => db.Employees.Where(employee => employee.EmployeeId == id).FirstOrDefault();

        public void PutEmployee(Employee employee)
        {
            db.Employees.Update(employee);
            db.SaveChanges();
        }

        public void PostEmployee(Employee employee)
        {
            db.Employees.Add(employee);
            db.SaveChanges();
        }
    }
}
