using Learn.Metrans.PERSISTANCE.Entities;

namespace Learn.Metrans.API.Services
{
    public interface IEmployeesRepository
    {
        void DeleteEmployees(int id);
        IList<Employees> GetEmployees();
        Employees? GetEmployees(int id);
        void InsertEmployyes(Employees employees);
        void InsertEmployyes(IList<Employees> employees);
        void UpdateEmployyes(Employees employees);
    }
}