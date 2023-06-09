using FluentValidation;
using Learn.Metrans.API.Models;
using Learn.Metrans.PERSISTANCE.Entities;
using System.ComponentModel.DataAnnotations;

namespace Learn.Metrans.CORE.Validations;

public class EmployeesValidator : AbstractValidator<EmployeesDto>
{
	public EmployeesValidator()
	{
        RuleFor(e => e.Id)
            .NotEmpty();
		RuleFor(e => e.Name)
			.NotEmpty()
			.Length(1, 20).WithMessage("Employee name should be in range 1 - 20 charactes.");
        RuleFor(e => e.Surname)
            .NotEmpty()
            .Length(1, 20).WithMessage("Employee surname should be in range 1 - 20 charactes.");
        RuleFor(e => e.DateOfBirth)
            .NotEmpty();
        RuleFor(e => e.EmployedFrom)
            .NotEmpty();
		RuleFor(e => e.EmployedTo)
			.Must((employee, employedTo) => IsEmployedFromLessThanTo(employedTo, employee.EmployedFrom)).WithMessage("Parameter EmployedTo should be later than EmployedFrom!");

    }
    #region Private utils
    /// <summary>
    /// Check if EmployedFrom is earlier than EmployedTo
    /// </summary>
    /// <param name="employedTo"></param>
    /// <param name="employedFrom"></param>
    /// <returns>Return true if employedTo is null or later than EmployedFrom</returns>
	private bool IsEmployedFromLessThanTo(DateTime? employedTo, DateTime? employedFrom)
    {
        if (employedTo is null)
            return true;
        if (employedTo != null)
            return employedFrom < employedTo;
        return false;
    }
    #endregion
}
