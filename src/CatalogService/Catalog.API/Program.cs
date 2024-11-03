using AutoMapper.Internal;
using Catalog.API.Controllers;
using Catalog.API.Utils;
using Catalog.Business.Extensions;
using Catalog.Persistence.Entities;
using Catalog.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddBusinessLayer();
builder.Services.AddPersistence(builder.Configuration);

// TODO: We also need to validate mapings but for that IMapper object is required, hence it's a bit akward to do this in here.
// Ideally, one should add that will call the method to validate mappings
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers(options =>
{
    options.Conventions.Add(new RouteTokenTransformerConvention(new KebabNamingParameterTransformer()));
});
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

app.UseAuthorization();

app.MapControllers();

app.Run();
