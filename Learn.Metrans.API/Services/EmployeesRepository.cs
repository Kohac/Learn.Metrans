using Learn.Metrans.API.Controllers;
using Learn.Metrans.PERSISTANCE.Context;
using Learn.Metrans.PERSISTANCE.Entities;
using Microsoft.EntityFrameworkCore;

namespace Learn.Metrans.API.Services;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly IConfiguration _config;
    private readonly MetransDbContext _context;

    public EmployeesRepository(IConfiguration config, MetransDbContext context)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public Employees? GetEmployees(Int32 id)
    {
        return _context.Employyes.Find(id);
    }
    public IList<Employees> GetEmployees()
    {
        return _context.Employyes.AsNoTracking().ToList();
    }
    public void UpdateEmployyes(Employees employees)
    {
        _context.Update<Employees>(employees);
        _context.SaveChanges();
    }
    public void InsertEmployyes(Employees employees)
    {
        _context.Add(employees);
        _context.SaveChanges();
    }
    public void InsertEmployyes(IList<Employees> employees)
    {
        _context.AddRange(employees);
        _context.SaveChanges();
    }
    public void DeleteEmployees(Int32 id)
    {
        _context.Remove(id);
        _context.SaveChanges();
    }
}
