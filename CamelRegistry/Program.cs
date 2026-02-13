using CamelRegistry.Data;
using CamelRegistry.DTOs.Requests;
using CamelRegistry.DTOs.Responses;
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

builder.Services.AddTransient<IValidator<CreateCamelDto>, CreateCamelValidator>();
builder.Services.AddTransient<IValidator<UpdateCamelDto>, UpdateCamelValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Camel Registry API",
        Version = "v1",
        Description = "API a tevék kezelésére (CRUD)"
    });
});

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
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Camel Registry API v1");
        c.RoutePrefix = "swagger";
    });
}

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<CamelDbContext>();
    db.Database.EnsureCreated();

    SeedData.Initialize(db);
}

app.UseMiddleware<CamelRegistry.Middleware.GlobalExceptionHandler>();

app.UseCors();


app.MapPost("/api/camels", async (CreateCamelDto camelDto, ICamelService service, IValidator<CreateCamelDto> validator) =>
{
    var result = await validator.ValidateAsync(camelDto);
    if (!result.IsValid)
        return Results.BadRequest(result.Errors.Select(e => e.ErrorMessage));

    var addedCamel = await service.AddCamelAsync(camelDto);
    return Results.Created($"/camels/{addedCamel.Id}", addedCamel);
})
.Produces<CamelDto>(StatusCodes.Status201Created);

app.MapGet("/api/camels", async (ICamelService service) =>
{
    var camels = await service.GetAllCamelsAsync();
    return Results.Ok(camels);
});

app.MapGet("/api/camels/{id}", async (int id, ICamelService service) =>
{
    var camel = await service.GetByIdAsync(id);
    return Results.Ok(camel);
});

app.MapPatch("/api/camels/{id}", async (int id, UpdateCamelDto camelDto, ICamelService service, IValidator<UpdateCamelDto> validator) =>
{
    var existingCamel = await service.GetByIdAsync(id);

    var result = await validator.ValidateAsync(camelDto);
    if (!result.IsValid)
        return Results.BadRequest(result.Errors.Select(e => e.ErrorMessage));

    var updatedCamel = await service.UpdateCamelAsync(id, camelDto);
    return Results.Ok(updatedCamel);
});

app.MapDelete("/api/camels/{id}", async (int id, ICamelService service) =>
{
    var deleted = await service.DeleteCamelAsync(id);
    return Results.Ok(deleted);
});

app.Run();


