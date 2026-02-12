using CamelRegistry.Data;
using CamelRegistry.NewFolder;
using CamelRegistry.Repositories;
using CamelRegistry.Services;
using CamelRegistry.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5005");

builder.Services.AddDbContext<CamelDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICamelRepository, CamelRepository>();
builder.Services.AddScoped<ICamelService, CamelService>();

builder.Services.AddValidatorsFromAssemblyContaining<CamelValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CamelDbContext>();
    db.Database.EnsureCreated();

    SeedData.Initialize(db);
}

app.UseMiddleware<CamelRegistry.Middleware.GlobalExceptionHandler>();

app.UseCors();

app.MapGet("/camels", async (ICamelService service) =>
{
    var camels = await service.GetAllCamelsAsync();
    return Results.Ok(camels);
});

app.MapGet("/camels/{id}", async (int id, ICamelService service) =>
{
    var camel = await service.GetByIdAsync(id);
    return Results.Ok(camel);
});

app.MapPatch("/camels/{id}", async (int id, CamelDto camelDto, ICamelService service, IValidator<CamelDto> validator) =>
{
    var existingCamel = await service.GetByIdAsync(id);

    var result = await validator.ValidateAsync(camelDto);
    if (!result.IsValid)
        return Results.BadRequest(result.Errors.Select(e => e.ErrorMessage));

    var updatedCamel = await service.UpdateCamelAsync(id, camelDto);
    return Results.Ok(updatedCamel);
});

app.MapDelete("/camels/{id}", async (int id, ICamelService service) =>
{
    var deleted = await service.DeleteCamelAsync(id);
    return Results.Ok(deleted);
});

app.Run();


