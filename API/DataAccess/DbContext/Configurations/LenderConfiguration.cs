using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DbContext.Configurations;

public class LenderConfiguration : IEntityTypeConfiguration<Lender>
{
    public void Configure(EntityTypeBuilder<Lender> modelBuilder)
    {
        modelBuilder
            .HasMany(l => l.Cars)
            .WithOne(c => c.Owner)
            .HasForeignKey(c => c.LenderId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder
            .HasMany(l => l.Deals)
            .WithOne(d => d.Lender)
            .HasForeignKey(d => d.LenderId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder
            .HasOne(l => l.Roles)
            .WithOne(d => d.Lender)
            .OnDelete(DeleteBehavior.Cascade);
    }
}