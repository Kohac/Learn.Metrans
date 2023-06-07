using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Learn.Metrans.PERSISTANCE.Entities;
public class Employees
{
    [Key]
    public Int32 Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public DateOnly? EmployedFrom { get; set; }
    [DefaultValue(typeof(DateOnly?),"2099-12-31")]
    public DateOnly? EmployedTo { get; set; }
}
