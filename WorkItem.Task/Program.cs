using Microsoft.EntityFrameworkCore;
using WorkItem.Task.Services;
using WorkItem.Task.Services.Contracts;
using WorkItem.Task.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//builder.Services.AddScoped<ITasksRepository, TasksRepository>();

builder.Services.AddScoped(typeof(IEntityRepository<WorkItem.Task.Models.Task>), typeof(EntityRepository<WorkItem.Task.Models.Task>));

builder.Services.AddScoped(typeof(IEntityRepository<WorkItem.Task.Models.UserProfile>), typeof(EntityRepository<WorkItem.Task.Models.UserProfile>));

builder.Services.AddScoped(typeof(IEntityRepository<WorkItem.Task.Models.TaskStatus>), typeof(EntityRepository<WorkItem.Task.Models.TaskStatus>));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ITasksService, TasksService>();

builder.Services.AddDbContext<WorkItem.Task.Models.WorkItemContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection"));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Tasks}/{action=Index}/{id?}");

app.Run();
