using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<TodoAdminContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoAdminContext") ?? throw new InvalidOperationException("Connection string 'TodoAdminContext' not found.")));
builder.Services.AddDbContext<TodotaskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodotaskContext") ?? throw new InvalidOperationException("Connection string 'TodotaskContext' not found.")));
builder.Services.AddDbContext<TodoAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TodoAppContext") ?? throw new InvalidOperationException("Connection string 'TodoAppContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Todolists}/{action=Login}/{id?}");

app.Run();
