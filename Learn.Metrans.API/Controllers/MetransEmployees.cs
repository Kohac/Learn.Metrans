using Learn.Metrans.API.Models;
using Learn.Metrans.API.Profiles;
using Learn.Metrans.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Learn.Metrans.API.Controllers;
/// <summary>
/// Minimal API controller
/// </summary>
public static class MetransEmployees
{
    /// <summary>
    /// Configuration of all endpoints
    /// </summary>
    /// <param name="app"><see cref="WebApplication"/></param>
    /// <returns>Return <see cref="WebApplication"/> with configured endpoints.</returns>
    public static WebApplication EndpointConfiguration(this WebApplication app)
    {
        app.MapGet("Employees/{id}", GetEmployees)
            .Produces<EmployeesDto>(200)
            .ProducesProblem(404)
            .ProducesProblem(204)
            .ProducesProblem(500);
        app.MapGet("Employees", GetEmployeesList)
            .Produces<IList<EmployeesDto>>(200)
            .ProducesProblem(404)
            .ProducesProblem(204)
            .ProducesProblem(500);
        app.MapPost("Employees/Create", PostEmployees)
            .Produces(200)
            .ProducesProblem(500);
        app.MapPut("Employees/Update", PutEmployees)
            .Produces(200)
            .ProducesProblem(404)
            .ProducesProblem(204)
            .ProducesProblem(500);
        app.MapDelete("Employees/Delete", DeleteEmployees)
            .Produces(200)
            .ProducesProblem(404)
            .ProducesProblem(204)
            .ProducesProblem(500);
        return app;
    }
    /// <summary>
    /// Get employee from database
    /// </summary>
    /// <param name="id">id</param>
    /// <param name="repo"><see cref="IEmployeesRepository"/></param>
    /// <returns>Return <see cref="EmployeesDto"/> from db.</returns>
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
    /// <summary>
    /// Insert Employee to DB
    /// </summary>
    /// <param name="employeesDtos"></param>
    /// <param name="repo"></param>
    /// <returns>Return response in <see cref="Results"/></returns>
    private static IResult PostEmployees([FromBody]EmployeesDto employeesDtos,
                                     [FromServices] IEmployeesRepository repo)
    {
        try
        {
            if (repo.IsEmployeeExists(employeesDtos.Id))
                return Results.BadRequest("The Employee with this id already exists. Please check id and try it again.");
            repo.InsertEmployyes(employeesDtos.MapEmployyesDtoToDb());
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Something wrong happen, try it later or contact our service desk! \n\t {ex.Message} \n\t {ex.StackTrace} \n\t {ex.GetType()}", statusCode: 500);
        }
    }
    /// <summary>
    /// Update employee in DB
    /// </summary>
    /// <param name="employee"></param>
    /// <param name="repo"></param>
    /// <returns>Return response in <see cref="Results"/></returns>
    private static IResult PutEmployees([FromBody] EmployeesDto employee,
                                     [FromServices] IEmployeesRepository repo)
    {
        try
        {
            var result = repo.GetEmployees(employee.Id);
            if (employee.Id <= 0)
                return Results.BadRequest("Id should be bigger than 0");
            if (result == null)
                return Results.NoContent();

            result.Name = employee.Name;
            result.Surname = employee.Surname;
            result.DateOfBirth = employee.DateOfBirth;
            result.EmployedFrom = employee.EmployedFrom;
            result.EmployedTo = employee.EmployedTo != null ? employee.EmployedTo : result.EmployedTo;

            repo.UpdateEmployyes(result);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Something wrong happen, try it later or contact our service desk! \n\t {ex.Message} \n\t {ex.StackTrace} \n\t {ex.GetType()}", statusCode: 500);
        }
    }
    /// <summary>
    /// Delete Employee from db
    /// </summary>
    /// <param name="id"></param>
    /// <param name="repo"></param>
    /// <returns>Return response in <see cref="Results"/></returns>
    private static IResult DeleteEmployees([FromQuery] Int32 id,
                                     [FromServices] IEmployeesRepository repo)
    {
        try
        {
            if (id <= 0)
                return Results.BadRequest("Id should be bigger than 0");
            var employee = repo.GetEmployees(id);
            if (employee == null)
                return Results.NotFound();
            repo.DeleteEmployees(employee);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.Problem($"Something wrong happen, try it later or contact our service desk! \n\t {ex.Message} \n\t {ex.StackTrace} \n\t {ex.GetType()}", statusCode: 500);
        }
    }
    /// <summary>
    /// Return all Employees from DB
    /// </summary>
    /// <param name="sortBy"></param>
    /// <param name="sortType"></param>
    /// <param name="repo"></param>
    /// <returns>Return response in <see cref="Results"/></returns>
    private static IResult GetEmployeesList([FromQuery] string? sortBy,
                                     [FromQuery] string? sortType,
                                     [FromServices] IEmployeesRepository repo)
    {
        try
        {
            var result = repo.GetEmployees().MapEmployyesToDto();
            if (result == null)
                return Results.NoContent();
            if (result.Count == 0)
                return Results.NoContent();
            if (sortBy == null && sortType == null)
                return Results.Ok(result!.OrderByDescending(x => x.Id));
            if (IsPropertyExists<EmployeesDto>(sortBy!))
                return Results.Ok(SortList(result!, sortBy!, sortType is null ? "ascending" : sortType));
            else
                return Results.BadRequest("Column for SortBy doesn't exists!");
        }
        catch (Exception ex)
        {
            return Results.Problem($"Something wrong happen, try it later or contact our service desk! \n\t {ex.Message} \n\t {ex.StackTrace} \n\t {ex.GetType()}", statusCode: 500);
        }
    }
    #region Private utils
    /// <summary>
    /// Check if the property exists on object with ignoring case
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="propertyName"></param>
    /// <returns>Return true if property exists otherwise false</returns>
    private static bool IsPropertyExists<T>(string propertyName) => (typeof(T).GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)! != null);
    /// <summary>
    /// Sort <see cref="IList{T}"/> base on input property name and sort type
    /// </summary>
    /// <param name="employeesDtos"></param>
    /// <param name="propertyName"></param>
    /// <param name="sortType"></param>
    /// <returns>Return sorted list of <see cref="IList{T}"/></returns>
    private static IList<EmployeesDto> SortList(IList<EmployeesDto> employeesDtos, string propertyName, string sortType)
    {
        if (sortType.ToLowerInvariant() == "descending")
            return employeesDtos.OrderByDescending(e => e.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)!.GetValue(e, null))
                .ToList();
        else
            return employeesDtos.OrderBy(e => e.GetType().GetProperty(propertyName, BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance)!.GetValue(e, null)).ToList();
    }
    #endregion
}
