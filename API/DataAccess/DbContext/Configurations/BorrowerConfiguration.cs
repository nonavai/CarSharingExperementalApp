using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DbContext.Configurations;

public class BorrowerConfiguration : IEntityTypeConfiguration<Borrower>
{
    public void Configure(EntityTypeBuilder<Borrower> modelBuilder)
    {
        modelBuilder
            .HasMany(b => b.Deals)
            .WithOne(d => d.Borrower)
            .HasForeignKey(d => d.BorrowerId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder
            .HasOne(b => b.Roles)
            .WithOne(d => d.Borrower)
            .OnDelete(DeleteBehavior.Cascade);
        
    }
}