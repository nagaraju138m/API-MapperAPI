using apiSample.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Configurations;
using Repository.Interfaces;
using Repository.Interfaces.Implement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors(a =>
{
    a.AddPolicy("AlloeAll", a =>
    {
        a.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

builder.Services.AddDbContext<sDbContext>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("sConString"));
});

builder.Services.AddAutoMapper(typeof(MapperConfig).Assembly);

builder.Services.AddScoped(typeof(IGenericRepo<>), typeof(GenericRepo<>));
builder.Services.AddScoped<IbookRepo, bookRepo>();
builder.Services.AddScoped<IgroupRepo, groupRepo>();
builder.Services.AddScoped<IsBookRepo, sBookRepo>();
builder.Services.AddScoped<iStudent,  studentRepo>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AlloeAll");
app.Run();
