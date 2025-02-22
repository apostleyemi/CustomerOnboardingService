using CustomerOnboardingService.Data;
using CustomerOnboardingService.InterfaceRepositories.Interfaces;
using CustomerOnboardingService.InterfaceRepositories.Repositories;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<AppDbContext>(opt =>
opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// serilog


builder.Services.AddScoped<ICustomer, CustomerRepository>();
builder.Services.AddScoped<IOtp, OtpRepository>();
builder.Services.AddScoped<IStateLocalgov, StateLocalGovRepository>();
builder.Services.AddScoped<IBank, BankRepository>();
builder.Services.AddHttpClient();
builder.Services.AddAutoMapper(typeof(Program));

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

app.Run();


