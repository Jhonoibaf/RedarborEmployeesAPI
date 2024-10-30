using Microsoft.EntityFrameworkCore;
using RedarborEmployees.Infrastructure.Data;

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
