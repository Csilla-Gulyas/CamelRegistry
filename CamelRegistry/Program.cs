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

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.ContentType = "application/json";
        var exception = context.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerFeature>()?.Error;
        if (exception != null)
        {
            context.Response.StatusCode = 500;
            await context.Response.WriteAsJsonAsync(new { error = exception.Message });
        }
    });
});

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

app.Run();


