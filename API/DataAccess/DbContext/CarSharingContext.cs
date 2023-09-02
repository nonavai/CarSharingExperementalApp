using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DbContext;

public class CarSharingContext : Microsoft.EntityFrameworkCore.DbContext
{//
    public DbSet<Car> Cars { get; set; }
    public DbSet<Deal> Deals { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Lender> Lenders { get; set; }
    public DbSet<Borrower> Borrowers { get; set; }
    public DbSet<Activity> Activity { get; set; }
    public DbSet<FeedBack> FeedBacks { get; set; }
    public DbSet<Roles> Roles { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    

    public string DbPath = $"Server=(localdb)\\mssqllocaldb;Database=CarSharingDB;Trusted_Connection=True;";

    public CarSharingContext()
    { 
        //Database.EnsureCreated();
    }
    public CarSharingContext(DbContextOptions<CarSharingContext> options) : base(options)
    {
    }
    

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Car>()
            .HasOne(p => p.Owner)
            .WithMany(c => c.Cars)
            .HasForeignKey(p => p.LenderId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Lender>()
            .HasMany(l => l.Cars)
            .WithOne(c => c.Owner)
            .HasForeignKey(c => c.LenderId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Activity>()
            .HasOne(a => a.Car)
            .WithOne(c => c.Activity)
            .HasForeignKey<Activity>(a => a.CarId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Car>()
            .HasOne(c => c.Activity)
            .WithOne(a => a.Car)
            .HasForeignKey<Activity>(a => a.CarId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Car>()
            .HasMany(c => c.Deals)
            .WithOne(d => d.Car)
            .HasForeignKey(d => d.CarId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Borrower>()
            .HasMany(b => b.Deals)
            .WithOne(d => d.Borrower)
            .HasForeignKey(d => d.BorrowerId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Lender>()
            .HasMany(l => l.Deals)
            .WithOne(d => d.Lender)
            .HasForeignKey(d => d.LenderId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithOne(r => r.User)
            .HasForeignKey<User>(u => u.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Roles>()
            .HasOne(r => r.Lender)
            .WithOne(l => l.Roles)
            .HasForeignKey<Roles>(r => r.LenderId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Roles>()
            .HasOne(r => r.Borrower)
            .WithOne(b => b.Roles)
            .HasForeignKey<Roles>(r => r.BorrowerId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Car>()
            .HasMany(r => r.FeedBack)
            .WithOne(b => b.Car)
            .HasForeignKey(r => r.CarId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder.Entity<User>()
            .HasMany(r => r.FeedBacks)
            .WithOne(b => b.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);

    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(DbPath);
}