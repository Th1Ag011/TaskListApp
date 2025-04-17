using TaskListApp.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using TaskListApp.Domain.Interfaces;
using TaskListApp.Infrastructure.Repository;
using TaskListApp.Aplication.Interfaces;
using TaskListApp.Aplication.Services;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(doc =>
{
    doc.SwaggerDoc("v1", new()
    {
        Title = "TaskList API",
        Version = "v1",
        Description = "API para gerenciar tarefas",
    });

    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    doc.IncludeXmlComments(xmlPath);
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("PoliticaCors", policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddDbContext<TaskListDbContext>(option =>
                               option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ITaskItemRepository, TaskItemRepository>();
builder.Services.AddScoped<ITaskItemService, TaskItemService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.MapSwagger();

app.UseCors("PoliticaCors");

app.UseAuthorization();

app.MapControllers();

app.Run();

