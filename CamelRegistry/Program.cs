using CamelRegistry.Data;
using CamelRegistry.Repositories;
using CamelRegistry.Services;
using CamelRegistry.Validators;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://localhost:5005");

builder.Services.AddDbContext<CamelDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<ICamelRepository, CamelRepository>();
builder.Services.AddScoped<ICamelService, CamelService>();

//builder.Services.AddValidatorsFromAssemblyContaining<CamelValidator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapGet("/camels", async (ICamelService service) =>
{
    var camels = await service.GetAllCamelsAsync();
    return Results.Ok(camels);
});

app.MapDelete("/camels/{id}", async (int id, ICamelService service) =>
{
    var deleted = await service.DeleteCamelAsync(id);
    return Results.Ok(deleted);
});

app.UseMiddleware<CamelRegistry.Middleware.GlobalExceptionHandler>();

app.Run();


