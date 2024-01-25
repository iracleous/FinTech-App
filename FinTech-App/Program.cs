using FinTech_App.Model;
using FinTech_App.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var optionsCon = builder.Configuration.GetConnectionString("MyConn");
builder.Services.AddDbContext<FinTechDbContext>(options => options.UseSqlServer(optionsCon));
builder.Services.AddScoped<IGenericService<Client, long>, GenericService<Client, long>>();
builder.Services.AddScoped<IGenericService<Account, long>, GenericService<Account, long>>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
