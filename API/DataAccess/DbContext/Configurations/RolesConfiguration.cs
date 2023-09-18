using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DbContext.Configurations;

public class RolesConfiguration : IEntityTypeConfiguration<Roles>
{
    public void Configure(EntityTypeBuilder<Roles> modelBuilder)
    {
        modelBuilder
            .HasOne(r => r.User)
            .WithOne(u => u.Role)
            .HasForeignKey<Roles>(r => r.UserId)
            .OnDelete(deleteBehavior: DeleteBehavior.Cascade);
        modelBuilder
            .HasOne(r => r.Borrower)
            .WithOne(b => b.Roles)
            .HasForeignKey<Roles>(r => r.BorrowerId)
            .OnDelete(deleteBehavior: DeleteBehavior.Restrict);
        modelBuilder
            .HasOne(r => r.Lender)
            .WithOne(b => b.Roles)
            .HasForeignKey<Roles>(r => r.LenderId)
            .OnDelete(deleteBehavior: DeleteBehavior.Restrict);
    }
}