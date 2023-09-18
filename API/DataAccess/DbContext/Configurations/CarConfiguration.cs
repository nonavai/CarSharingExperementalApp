using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics;

namespace DataAccess.DbContext.Configurations;

public class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> modelBuilder)
    {
        modelBuilder
            .HasOne(c => c.Lender)
            .WithMany(l => l.Cars)
            .HasForeignKey(c => c.LenderId);
        
    }
}