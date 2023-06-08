using Learn.Metrans.API.Models;
using Learn.Metrans.PERSISTANCE.Entities;
using Mapster;

namespace Learn.Metrans.API.Profiles;
/// <summary>
/// Mappers
/// </summary>
public static class EmployeesMapper
{
    /// <summary>
    /// Map Employees to DTOs
    /// </summary>
    /// <param name="employyes"><see cref="IList{Employees}"/></param>
    /// <returns><see cref="IList{EmployeesDto}"/></returns>
    public static IList<EmployeesDto> MapEmployyesToDto(this IList<Employees> employyes)
    {
        var config = new TypeAdapterConfig();
        config.NewConfig<Employees, EmployeesDto>();
        return employyes.Adapt<IList<EmployeesDto>>(config);
    }
    /// <summary>
    /// Map Dtos to DB Emloyees
    /// </summary>
    /// <param name="employyesDto"><see cref="IList{EmployeesDto}"/></param>
    /// <returns><see cref="IList{Employees}"/></returns>
    public static IList<Employees> MapEmployyesDtoToDb(this IList<EmployeesDto> employyesDto)
    {
        var config = new TypeAdapterConfig();
        config.NewConfig<EmployeesDto, Employees>();
        return employyesDto.Adapt<IList<Employees>>(config);
    }
    /// <summary>
    /// Map Employee to Dto
    /// </summary>
    /// <param name="employyes"><see cref="Employees"/></param>
    /// <returns><see cref="EmployeesDto"/></returns>
    public static EmployeesDto MapEmployyesToDto(this Employees employyes)
    {
        var config = new TypeAdapterConfig();
        config.NewConfig<Employees, EmployeesDto>();
        return employyes.Adapt<EmployeesDto>(config);
    }
    /// <summary>
    /// Map dto to db employee
    /// </summary>
    /// <param name="employyesDto"><see cref="EmployeesDto"/></param>
    /// <returns><see cref="Employees"/></returns>
    public static Employees MapEmployyesDtoToDb(this EmployeesDto employyesDto)
    {
        var config = new TypeAdapterConfig();
        config.NewConfig<EmployeesDto, Employees>()
            .Map(dest => dest.EmployedTo, src => src.EmployedTo != null ? src.EmployedTo : new DateTime(2099, 12, 31));
        return employyesDto.Adapt<Employees>(config);
    }
}
