using dotent_core_min_api.Data;
using dotnet_core_min_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var conn = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MySqlDbContext>(options => options.UseNpgsql(conn));

var app = builder.Build();

app.MapGet("/query", async ([FromQuery] string? sno, MySqlDbContext dbContext) => {
    Console.WriteLine($"query string: {sno}");

    DeliveryEntity data;
    if (string.IsNullOrEmpty(sno))
    {
        data = await dbContext.delivery.FirstAsync();
    }
    else
    {
        data = await dbContext.delivery.FirstOrDefaultAsync(i => i.sno == sno);
    }

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
