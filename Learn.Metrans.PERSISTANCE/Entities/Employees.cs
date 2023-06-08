using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Learn.Metrans.PERSISTANCE.Entities;
public class Employees
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Int32 Id { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime? EmployedFrom { get; set; }
    [DefaultValue(typeof(DateTime?),"2099-12-31")]
    public DateTime? EmployedTo { get; set; }
}
