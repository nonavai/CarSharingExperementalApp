using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DbContext.Configurations;

public class ActivityConfiguration : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> modelBuilder)
    {
        modelBuilder
            .HasOne(a => a.Car)
            .WithOne(c => c.Activity)
            .HasForeignKey<Activity>(a => a.CarId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}