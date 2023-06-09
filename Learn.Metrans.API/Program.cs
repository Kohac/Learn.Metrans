var builder = WebApplication.CreateBuilder(args);
//inject DI
builder.Services.InjectServices();

var app = builder.Build()
    .ConfigureWebApplication(builder.Configuration)
    .EndpointConfiguration()
    .InsertEmployeesOnInitialization();

app.Run();