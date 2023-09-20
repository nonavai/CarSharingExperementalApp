using DataAccess.DbContext.Configurations;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Microsoft.Extensions.Configuration;

namespace DataAccess.DbContext;

public class CarSharingContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<Deal> Deals { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Lender> Lenders { get; set; }
    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<FeedBack> FeedBacks { get; set; }
    public DbSet<Roles?> Roles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    

    public string DbPath;

    public CarSharingContext()
    { 
        
    }
    public CarSharingContext(DbContextOptions<CarSharingContext> options, IConfiguration configuration) : base(options)
    {
        DbPath = configuration.GetConnectionString("CarSharingDb");
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new CarConfiguration());
        modelBuilder.ApplyConfiguration(new RolesConfiguration());
        modelBuilder.ApplyConfiguration(new FeedBackConfiguration());
        modelBuilder.ApplyConfiguration(new DealConfiguration());
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(DbPath);
}