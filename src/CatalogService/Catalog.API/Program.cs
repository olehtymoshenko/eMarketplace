using Catalog.API.Utils;
using Catalog.Business.Extensions;
using Catalog.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Information)
    .WriteTo.Console()
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddSerilog((services, lc) => lc
        .ReadFrom.Configuration(builder.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
    );

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

    app.UseSerilogRequestLogging();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();

    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "An unhandled exception occurred during bootstrapping");
    return 1;
}
finally
{
    Log.CloseAndFlush();
}