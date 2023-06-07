using Learn.Metrans.API.Models;
using Learn.Metrans.API.Profiles;
using Learn.Metrans.API.Services;
using Learn.Metrans.PERSISTANCE.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel;
using System.Linq;
using System.Reflection;

namespace Learn.Metrans.API.Controllers;

public static class MetransEmployees
{
    public static void EndpointConfiguration(WebApplication app)
    {
        app.MapGet("Employees/{id}", GetEmployees)
            .Produces<EmployeesDto>(200)
            .ProducesProblem(404)
            .ProducesProblem(204)
            .ProducesProblem(500);
        app.MapGet("Employees", GetEmployeesList);
        app.MapPost("Employees/Create", PostEmployees);
        app.MapPut("Employees/Update", PutEmployees);
        app.MapDelete("Employees/Delete",DeleteEmployees);
    }
    private static IResult GetEmployees([FromQuery] Int32 id,
                                     [FromServices] IEmployeesRepository repo)
    {
        try
        {
            var result = repo.GetEmployees(id);
            if (id <= 0)
                return Results.BadRequest("Id should be bigger than 0");
            if (result == null)
                return Results.NoContent();
            return Results.Ok(result);
        }
        catch (Exception ex)
        {
            return Results.Problem($"Something wrong happen, try it later or contact our service desk! \n\t {ex.Message} \n\t {ex.StackTrace} \n\t {ex.GetType()}", statusCode: 500);
        }
    }
    private static IResult PostEmployees([FromBody] IList<EmployeesDto> employeesDtos,
                                     [FromServices] IEmployeesRepository repo)
    {
        try
        {
            repo.InsertEmployyes(employeesDtos.MapEmployyesDtoToDb());
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Something wrong happen, try it later or contact our service desk! \n\t {ex.Message} \n\t {ex.StackTrace} \n\t {ex.GetType()}", statusCode: 500);
        }
    }
    private static IResult PutEmployees([FromBody] EmployeesDto employee,
                                     [FromServices] IEmployeesRepository repo)
    {
        try
        {
            repo.UpdateEmployyes(employee.MapEmployyesDtoToDb());
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Something wrong happen, try it later or contact our service desk! \n\t {ex.Message} \n\t {ex.StackTrace} \n\t {ex.GetType()}", statusCode: 500);
        }
    }
    private static IResult DeleteEmployees([FromQuery] Int32 id,
                                     [FromServices] IEmployeesRepository repo)
    {
        try
        {
            repo.DeleteEmployees(id);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Something wrong happen, try it later or contact our service desk! \n\t {ex.Message} \n\t {ex.StackTrace} \n\t {ex.GetType()}", statusCode: 500);
        }
    }
    private static IResult GetEmployeesList([FromQuery] string? sortBy,
                                     [FromQuery] string? sortType,
                                     [FromServices] IEmployeesRepository repo)
    {
        try
        {
            var result = repo.GetEmployees().MapEmployyesToDto();
            if (result == null)
                Results.NoContent();
            if (sortBy == null && sortType == null)
                Results.Ok(result!.OrderByDescending(x => x.Id));
            if (sortType!.ToLower() == "descending" && sortBy == null)
                result!.OrderByDescending(x => x.Id);
            else result!.OrderBy(x => x.Id);
            if(sortBy != null)
            {
                PropertyInfo? pi = typeof(EmployeesDto)!.GetProperty(sortBy);
                if (pi == null)
                    return Results.BadRequest("Column for SortBy doesn't exists!");
                if(sortType.ToLower() == "descending")
                    return Results.Ok(result.OrderByDescending(x => x.))
            }



        }
        catch (Exception ex)
        {
            return Results.Problem($"Something wrong happen, try it later or contact our service desk! \n\t {ex.Message} \n\t {ex.StackTrace} \n\t {ex.GetType()}", statusCode: 500);
        }
    }
    #region Private 
    private static IEnumerable<T> OrderByProperty<T,Tkey>(IEnumerable<T> employees, Func<IEnumerable<T>, Tkey> func)
    {
        return employees.ToList().OrderBy(func);
    }
    private static IList<EmployeesDto> OrderByProperty(IList<EmployeesDto> employees, Func<IList<EmployeesDto>, string> func)
    {
        return employees.OrderBy(func);
    }
    private static IList<EmployeesDto> OrderByDesendingProperty(IList<EmployeesDto> employees, Func<IList<EmployeesDto>, string> func)
    {
        return employees.OrderByDescending(func);
    }
    private static IList<EmployeesDto> OrderBy(IList<EmployeesDto> employees, Func<IList<EmployeesDto>, string> func)
    {
        return employees.OrderByDescending(func);
    }
    #endregion
}
