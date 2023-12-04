using dotnet_core_min_api.Models;
using Microsoft.EntityFrameworkCore;

namespace dotent_core_min_api.Data;

public class MySqlDbContext : DbContext
{
    public MySqlDbContext(DbContextOptions<MySqlDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<DeliveryEntity> delivery { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<RecipientEntity>();
        modelBuilder.Ignore<DeliveryLocationEntity>();
        modelBuilder.Entity<DeiveryHistoryEntity>(eb => eb.HasNoKey());
    }
}