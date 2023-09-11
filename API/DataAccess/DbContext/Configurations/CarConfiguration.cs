using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DbContext.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> modelBuilder)
    {
        modelBuilder
            .HasOne(p => p.Owner)
            .WithMany(c => c.Cars)
            .HasForeignKey(p => p.LenderId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder
            .HasOne(c => c.Activity)
            .WithOne(a => a.Car)
            .HasForeignKey<Activity>(a => a.CarId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .HasMany(c => c.Deals)
            .WithOne(d => d.Car)
            .HasForeignKey(d => d.CarId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder
            .HasMany(r => r.FeedBack)
            .WithOne(b => b.Car)
            .HasForeignKey(r => r.CarId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}