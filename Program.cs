using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Application.Services;
using TaskManagementSystem.Core.Entities;
using TaskManagementSystem.Infrastructure.Models;
using TaskManagementSystem.Infrastructure.Repository;
using TaskManagementSystem.Infrastructure.TMSData;
using TaskManagementSystem.Infrastructure.TMSData.Interfaces;
using TaskManagementSystem.Core.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

// Database connectionstring
builder.Services.AddDbContext<TaskManagementDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("TaskManagementDBCon")));

//Enable CORS
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod()
      .AllowAnyHeader());
});

// Automapper configuration
builder.Services.AddAutoMapper(config =>
{
    config.CreateMap<TaskManagementSystem.Core.Entities.TaskModel, TaskManagementSystem.Infrastructure.Models.TblTask>();
    config.CreateMap<TaskManagementSystem.Infrastructure.Models.TblTask, TaskManagementSystem.Core.Entities.TaskModel>();
    config.CreateMap<TaskViewModel, TaskManagementSystem.Infrastructure.Models.TblTask>();
    config.CreateMap<TaskManagementSystem.Infrastructure.Models.TblTask, TaskViewModel>();
    config.CreateMap<TaskViewModel, TaskManagementSystem.Core.Entities.TaskModel>().ReverseMap();

}, AppDomain.CurrentDomain.GetAssemblies());

//builder.Services.AddScoped<IDBFactory, DBFactory>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
//builder.Services.AddScoped<IGenRepository, GenRepository();
builder.Services.AddScoped<ITaskService, TaskService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    
}

app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
