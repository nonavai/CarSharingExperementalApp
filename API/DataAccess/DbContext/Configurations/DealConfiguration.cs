using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DbContext.Configurations;

public class DealConfiguration : IEntityTypeConfiguration<Deal>
{
    public void Configure(EntityTypeBuilder<Deal> modelBuilder)
    {
        modelBuilder
            .HasOne(d => d.Lender)
            .WithMany(l => l.Deals)
            .HasForeignKey(d => d.LenderId)
            .OnDelete(deleteBehavior: DeleteBehavior.Restrict);
        modelBuilder
            .HasOne(d => d.Borrower)
            .WithMany(l => l.Deals)
            .HasForeignKey(d => d.BorrowerId)
            .OnDelete(deleteBehavior: DeleteBehavior.Restrict);        
        modelBuilder
            .HasOne(d => d.Car)
            .WithMany(l => l.Deals)
            .HasForeignKey(d => d.CarId)
            .OnDelete(deleteBehavior: DeleteBehavior.Restrict);

    }
}