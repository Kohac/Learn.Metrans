using Learn.Metrans.API.Models;
using System.ComponentModel.DataAnnotations;

namespace Learn.Metrans.API.Validations;
[Obsolete("This class can be used only for controllers approach")]
public class FromToComparisonAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var employe = (EmployeesDto)validationContext.ObjectInstance;
        if (employe == null)
            return new ValidationResult("Employyes object is empty");
        if(employe.EmployedFrom == null)
            return new ValidationResult("EmployedFrom is mandatory.");
        if (employe.EmployedTo != null)
            if (employe.EmployedFrom > employe.EmployedTo)
                return new ValidationResult("EmployedFrom cannot have higher date than employedTo.");
        return ValidationResult.Success;
    }
}
