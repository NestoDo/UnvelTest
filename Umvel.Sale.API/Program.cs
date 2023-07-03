using MediatR;
using Microsoft.EntityFrameworkCore;
using Umvel.Contracts.DTO.Sale;
using Umvel.Core.Handlers.Sale.Commands;
using Umvel.Core.Handlers.Sale.Queries;
using Umvel.Infrastructure.Data.Repositories;
using Umvel.Infrastructure.Data.Repositories.Interfaces;
using Umvel.Infrastructure.Database.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<UmveltestContext>(
        options => options.UseSqlServer(connString));

builder.Services.AddScoped<ISaleRepository, SaleRepository>();

builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddScoped<IRequestHandler<RegisterSaleCommand, RegisterSaleResponse>, RegisterSaleCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetSaleQuery, IEnumerable<GetSaleResponse>>, GetSaleQueryHandler>();

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
