using Learn.Metrans.API.Validations;
using System.ComponentModel.DataAnnotations;

namespace Learn.Metrans.API.Models;

public class EmployeesDto
{
    [Required]
    public Int32 Id { get; set; }
    [MaxLength(20, ErrorMessage = "Length must be in range 0 - 20 characters!"), Required]
    public string? Name { get; set; }
    [MaxLength(20, ErrorMessage = "Length must be in range 0 - 20 characters!"), Required]
    public string? Surname { get; set; }
    [Required]
    public DateOnly? DateOfBirth { get; set; }
    [FromToComparisonAttribute]
    public DateOnly? EmployedFrom { get; set; }
    [FromToComparisonAttribute]
    public DateOnly? EmployedTo { get; set; }
}
