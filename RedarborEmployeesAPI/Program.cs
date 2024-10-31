using FluentValidation;
using MediatR.Extensions.FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RedarborEmployees.Application.Mappers;
using RedarborEmployees.Infrastructure.Data;
using RedarborEmployees.Application.EmployeesAdministration.Commands;
using System.Text.Json.Serialization;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

//var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings__AplicationDbConection");

//if (string.IsNullOrEmpty(connectionString))
//{
//    throw new InvalidOperationException("Connection string not found in environment variables.");
//}

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AplicationDbConection"))
);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.JsonSerializerOptions.Converters.Add(new RedarborEmployees.Application.DTOs.JsonDateTimeConverter());
    });

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(CreateEmployeeCommand).Assembly));
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dataContext.Database.Migrate();
}

if (!app.Environment.IsDevelopment())
{                                                                              
    app.UseExceptionHandler("/Home/Error");
}

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
