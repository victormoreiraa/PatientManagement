using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using PatientManagement.CrossCutting.DependencyInjection;
using PatientManagement.Infrastructure.Data;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

DependencyInjectionConfig.RegisterRepositories(builder.Services);
DependencyInjectionConfig.RegisterServices(builder.Services);

builder.Services.AddControllers();
builder.Services.AddCors(options =>
    {
    options.AddPolicy("AllowAnyOrigin", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseRouting();

app.UseCors("AllowAnyOrigin");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});



app.Run();
