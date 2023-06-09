using Learn.Metrans.API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Learn.Metrans.API.Models;

public class EmployeesDto
{
    /// <summary>
    /// unique identificator of Employee
    /// </summary>
    public Int32 Id { get; set; }
    /// <summary>
    /// First name of employee
    /// </summary>
    public string? Name { get; set; }
    /// <summary>
    /// Last name of employee
    /// </summary>
    public string? Surname { get; set; }
    /// <summary>
    /// Date of birth of Employee
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    /// <summary>
    /// Date the employee started working for our company
    /// </summary>
    public DateTime? EmployedFrom { get; set; }
    /// <summary>
    /// The date on which the employee is to be employed
    /// </summary>
    public DateTime? EmployedTo { get; set; }
}
