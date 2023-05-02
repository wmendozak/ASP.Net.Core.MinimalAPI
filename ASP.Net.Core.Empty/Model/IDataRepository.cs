namespace ASP.Net.Core.Empty.Model
{
    public interface IDataRepository
    {
        Employee GetEmployee(string id);
        List<Employee> GetEmployees();
        void PostEmployee(Employee employee);
        void PutEmployee(Employee employee);
    }
}