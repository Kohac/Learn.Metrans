using Learn.Metrans.PERSISTANCE.Context;
using Learn.Metrans.PERSISTANCE.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata.Ecma335;

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
    /// <summary>
    /// Check if employee exists in db by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Return true if employee exists</returns>
    public bool IsEmployeeExists(Int32 id) => (_context.Employyes.Any(x => x.Id == id));
    /// <summary>
    /// Get employee by id
    /// </summary>
    /// <param name="id">Id</param>
    /// <returns>Return Employye found by id</returns>
    public Employees? GetEmployees(Int32 id)
    {
        return _context.Employyes.Find(id);
    }
    /// <summary>
    /// Get all employees
    /// </summary>
    /// <returns>Return all employees from db.</returns>
    public IList<Employees> GetEmployees()
    {
        return _context.Employyes.AsNoTracking().ToList();
    }
    /// <summary>
    /// Update employee from existing <see cref="Employees"/> object
    /// </summary>
    /// <param name="employees"><see cref="Employees"/></param>
    public void UpdateEmployyes(Employees employees)
    {
        _context.Update<Employees>(employees);
        _context.SaveChanges();
    }
    /// <summary>
    /// Insert employee by providing <see cref="Employees"/>
    /// </summary>
    /// <param name="employees"><see cref="Employees"/></param>
    public void InsertEmployyes(Employees employees)
    {
        _context.Add(employees);
        _context.SaveChanges();
    }
    /// <summary>
    /// Insert range of employyes providing <see cref="IList{Employees}"/>
    /// </summary>
    /// <param name="employees"><see cref="IList{Employees}"/></param>
    public void InsertEmployyes(IList<Employees> employees)
    {
        _context.AddRange(employees);
        _context.SaveChanges();
    }
    /// <summary>
    /// Delete employee from DB
    /// </summary>
    /// <param name="employee"></param>
    public void DeleteEmployees(Employees employee)
    {
        _context.Remove(employee);
        _context.SaveChanges();
    }
}
