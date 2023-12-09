using dotnet_core_min_api.Data;
using dotnet_core_min_api.Models;
using dotnet_core_min_api.Repositories;
using dotnet_core_min_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApiDbContext>(options => options.UseNpgsql(conn));
builder.Services.AddScoped<IDeliveryService, DeliveryService>();
builder.Services.AddScoped<IDeliveryRepository, DeliveryRepository>();

var app = builder.Build();

app.MapGet("query", async (HttpContext context, [FromServices] IDeliveryService deliveryService) =>
{
    var querySno = context.Request.Query["sno"];
    Console.WriteLine($"query string sno: {querySno}");

    var result = await deliveryService.GetDeliveryData(querySno);

    if (result is null)
    {
        return Results.NotFound(new ApiResponseEntity
        {
            Status = "error",
            Error = new { code = 404, message = "Tracking number not found" }
        });
    }
    
    return Results.Ok(new ApiResponseEntity
    {
        Status = "success",
        Data = result
    });
});

    if (data == null)
    {
        return new ApiResponseEntity
        {
            Status = "error",
            Error = new { code = 404, message = "Tracking number not found" }
        };
    }

    return new ApiResponseEntity
    {
        Status = "success",
        Data = data
    };
});

app.Run();
