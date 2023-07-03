using MediatR;
using Microsoft.EntityFrameworkCore;
using Umvel.Contracts.DTO.Product;
using Umvel.Core.Handlers.Product.Commands;
using Umvel.Core.Handlers.Product.Queries;
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

builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddScoped<IRequestHandler<RegisterProductCommand, RegisterProductResponse>, RegisterProductCommandHandler>();
builder.Services.AddScoped<IRequestHandler<GetAllProductQuery, IEnumerable<GetAllProductResponse>>, GetAllProductQueryHandler>();
builder.Services.AddScoped<IRequestHandler<GetProductByIdQuery, GetProductByIdResponse>, GetProductByIdQueryHandler>();

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
