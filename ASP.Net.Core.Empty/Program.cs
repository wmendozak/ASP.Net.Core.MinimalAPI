using ASP.Net.Core.Empty.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("AppDb");

builder.Services.AddTransient<DataSeeder>();
builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddDbContext<EmployeeDbContext>(context => context.UseSqlServer(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseSwaggerUI();
    
if (args.Length == 1 && args[0].ToLower() == "seeddata")
    SeedData(app);

void SeedData(IHost app)
{
    var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
    using (var scope = scopedFactory.CreateScope())
    {
        var service = scope.ServiceProvider.GetService<DataSeeder>();
        service.Seed();
    }
    Console.Write("Seeded!");
}


if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger(x => x.SerializeAsV2 = true);

app.MapGet("/", () => "Hello World!");

app.MapGet("/employees", ([FromServices] IDataRepository db) =>
{
    return db.GetEmployees();
});

app.MapGet("/employee/{id}", ([FromServices] IDataRepository db, string id) =>
{
    return db.GetEmployee(id);
});

app.MapPut("/employee/{id}", ([FromServices] IDataRepository db, Employee employee) =>
{
    try
    {
        db.PutEmployee(employee);
        return StatusCodes.Status200OK;
    }
    catch (Exception)
    {
        return StatusCodes.Status400BadRequest;
    }
});

app.MapPost("/employee", ([FromServices] IDataRepository db, Employee employee) =>
{
    try
    {
        db.PostEmployee(employee);
        return StatusCodes.Status200OK;
    }
    catch (Exception)
    {
        return StatusCodes.Status400BadRequest;
    }
});

app.Run();