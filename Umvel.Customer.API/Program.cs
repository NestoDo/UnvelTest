using MediatR;
using Microsoft.EntityFrameworkCore;
using Umvel.Contracts.DTO.Customer;
using Umvel.Core.Handlers.Customer.Commands;
using Umvel.Core.Handlers.Customer.Queries;
using Umvel.Infrastructure.Data.Repositories;
using Umvel.Infrastructure.Database.Models;
using Umvel.Infrastructure.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<UmveltestContext>(
        options => options.UseSqlServer(connString));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();

builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddScoped<IRequestHandler<RegisterCustomerCommand, RegisterCustomerResponse>, RegisterCustomerCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllCustomerQuery, IEnumerable<GetAllCustomerResponse>>, GetAllCustomerQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetCustomerByIdQuery, GetCustomerByIdResponse>, GetCustomerByIdQueryHandler>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
