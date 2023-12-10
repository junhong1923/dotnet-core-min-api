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

app.MapGet("fake", async (HttpContext context, [FromServices] IDeliveryService deliveryService) =>
{
    var queryNum = context.Request.Query["num"];
    Console.WriteLine($"query string num: {queryNum}");

    try
    {
        object result;
        if (int.TryParse(queryNum, out int num))
        {
            if (num > 100 || num < 1)
            {
                return new ApiResponseEntity
                {
                    Status = "success",
                    Data = "非法的 num，請輸入 1~99 的正整數"
                };
            }
            
            result = await deliveryService.CreateFakeDeliveryData(num);
        }
        else
        {
            var msg = $"Invalid query string: {queryNum}";
            Console.WriteLine(msg);
            result = msg;
        }

        return new ApiResponseEntity
        {
            Status = "success",
            Data = result
        };
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
        return new ApiResponseEntity
        {
            Status = "error",
            Error = "Something went wrong"
        };
    }
});

app.MapGet("report", async ([FromServices] IDeliveryService deliveryService) => await deliveryService.Report());

app.Run();
