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
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .HasMany(l => l.Deals)
            .WithOne(d => d.Lender)
            .HasForeignKey(d => d.LenderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}