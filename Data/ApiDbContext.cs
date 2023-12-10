using dotnet_core_min_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnet_core_min_api.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<DeliveryEntity> delivery { get; set; }
    public DbSet<HistoryEntity> history { get; set; }
    public DbSet<LocationEntity> location { get; set; }
    public DbSet<RecipientEntity> recipient { get; set; }
}