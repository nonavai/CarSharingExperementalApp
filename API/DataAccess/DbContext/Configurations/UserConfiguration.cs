using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataAccess.DbContext.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> modelBuilder)
    {
        modelBuilder
            .HasMany(r => r.FeedBacks)
            .WithOne(b => b.User)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.NoAction);
        modelBuilder
            .HasOne(r => r.Role)
            .WithOne(b => b.User)
            .HasForeignKey<User>(r => r.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}