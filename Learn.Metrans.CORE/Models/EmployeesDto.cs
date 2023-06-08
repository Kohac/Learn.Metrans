using Learn.Metrans.API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Learn.Metrans.API.Models;

public class EmployeesDto
{
    /// <summary>
    /// unique identificator of Employee
    /// </summary>
    [Required]
    public Int32 Id { get; set; }
    /// <summary>
    /// First name of employee
    /// </summary>
    [MaxLength(20, ErrorMessage = "Length must be in range 0 - 20 characters!"), Required]
    public string? Name { get; set; }
    /// <summary>
    /// Last name of employee
    /// </summary>
    [MaxLength(20, ErrorMessage = "Length must be in range 0 - 20 characters!"), Required]
    public string? Surname { get; set; }
    /// <summary>
    /// Date of birth of Employee
    /// </summary>
    [Required]
    public DateTime? DateOfBirth { get; set; }
    /// <summary>
    /// Date the employee started working for our company
    /// </summary>
    [FromToComparisonAttribute, Required]
    public DateTime? EmployedFrom { get; set; }
    /// <summary>
    /// The date on which the employee is to be employed
    /// </summary>
    [FromToComparisonAttribute]
    public DateTime? EmployedTo { get; set; }
}
