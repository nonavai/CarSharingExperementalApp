using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DbContext.Configurations;

public class FeedBackConfiguration : IEntityTypeConfiguration<FeedBack>
{
    public void Configure(EntityTypeBuilder<FeedBack> modelBuilder)
    {
        modelBuilder
            .HasOne(b => b.Car)
            .WithMany(d => d.FeedBack)
            .HasForeignKey(d => d.CarId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .HasOne(b => b.User)
            .WithMany(d => d.FeedBacks)
            .HasForeignKey(d => d.UserId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}