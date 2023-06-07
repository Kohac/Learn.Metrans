using Learn.Metrans.API.Controllers;
using Learn.Metrans.API.Models;
using Learn.Metrans.PERSISTANCE.Entities;
using Mapster;

namespace Learn.Metrans.API.Profiles;

public static class EmployeesMapper
{
    public static IList<EmployeesDto> MapEmployyesToDto(this IList<Employees> employyes)
    {
        var config = new TypeAdapterConfig();
        config.NewConfig<Employees, EmployeesDto>();
        return employyes.Adapt<IList<EmployeesDto>>(config);
    }
    public static IList<Employees> MapEmployyesDtoToDb(this IList<EmployeesDto> employyesDto)
    {
        var config = new TypeAdapterConfig();
        config.NewConfig<EmployeesDto, Employees>();
        return employyesDto.Adapt<IList<Employees>>(config);
    }
    public static EmployeesDto MapEmployyesToDto(this Employees employyes)
    {
        var config = new TypeAdapterConfig();
        config.NewConfig<Employees, EmployeesDto>();
        return employyes.Adapt<EmployeesDto>(config);
    }
    public static Employees MapEmployyesDtoToDb(this EmployeesDto employyesDto)
    {
        var config = new TypeAdapterConfig();
        config.NewConfig<EmployeesDto, Employees>();
        return employyesDto.Adapt<Employees>(config);
    }
}
