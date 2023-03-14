using Business.Layer.Services;
using Business.Layer.Services.IServices;
using Data.AccessLayer.DBContext;
using Data.AccessLayer.Repositories;
using Data.AccessLayer.Repositories.IRepositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddControllers().AddJsonOptions(options =>
//               options.JsonSerializerOptions.PropertyNamingPolicy = null);

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

builder.Services.AddDbContext<ClassTracking_DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("constring")));

builder.Services.AddScoped<IClassInformationRepository, ClassInformationRepository>();
builder.Services.AddScoped<IClassInformationServices, ClassInformationServices>();
builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
builder.Services.AddScoped<ITeacherServices, TeacherServices>();
builder.Services.AddScoped<IClassScheduleRepository, ClassScheduleRepository>();
builder.Services.AddScoped<IClassScheduleService, ClassScheduleService>();


//builder.Services.Configure<JsonOptions>(options =>
//{
//    options.SerializerOptions.PropertyNameCaseInsensitive = false;
//    options.SerializerOptions.PropertyNamingPolicy = null;
//    options.SerializerOptions.WriteIndented = true;
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
