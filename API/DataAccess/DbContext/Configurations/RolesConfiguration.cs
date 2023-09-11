using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DbContext.Configurations;

public class RolesConfiguration : IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> modelBuilder)
    {
        modelBuilder
            .HasOne(r => r.Lender)
            .WithOne(l => l.Roles)
            .HasForeignKey<Roles>(r => r.LenderId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder
            .HasOne(r => r.Borrower)
            .WithOne(b => b.Roles)
            .HasForeignKey<Roles>(r => r.BorrowerId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Cascade);
    }
}