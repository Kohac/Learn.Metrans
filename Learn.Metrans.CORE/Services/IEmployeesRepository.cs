using Learn.Metrans.PERSISTANCE.Entities;

namespace Learn.Metrans.API.Services
{
    public interface IEmployeesRepository
    {
        bool IsEmployeeExists(Int32 id);
        void DeleteEmployees(Employees employee);
        IList<Employees> GetEmployees();
        Employees? GetEmployees(int id);
        void InsertEmployyes(Employees employees);
        void InsertEmployyes(IList<Employees> employees);
        Employees InsertEmployyesAndReturn(Employees employees);
        void UpdateEmployyes(Employees employees);
    }
}