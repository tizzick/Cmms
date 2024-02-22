using Cmms.Api.Assets;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add useful interface for accessing the ActionContext outside a controller.
builder.Services.AddHttpContextAccessor()
    .AddSingleton<IActionContextAccessor, ActionContextAccessor>()
    .AddValidatorsFromAssemblyContaining<Program>(lifetime: ServiceLifetime.Singleton)
    .AddControllers(x => x.ModelValidatorProviders.Clear());

builder.Services.AddAssetServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.Run();
