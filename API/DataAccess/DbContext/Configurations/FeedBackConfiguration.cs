using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DbContext.Configurations;

public class FeedBackConfiguration : IEntityTypeConfiguration<FeedBack>
{
    public void Configure(EntityTypeBuilder<FeedBack> modelBuilder)
    {
        modelBuilder
            .HasOne(f => f.User)
            .WithMany(u => u.FeedBacks)
            .HasForeignKey(f => f.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.Restrict);
        modelBuilder
            .HasOne(f => f.Car)
            .WithMany(c => c.FeedBacks)
            .HasForeignKey(f => f.CarId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
    }
}