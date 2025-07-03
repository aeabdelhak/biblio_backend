using BiblioPfe.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace BiblioPfe.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>

    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Avatar).WithOne(e=>e.User).HasForeignKey<User>(e=>e.AvatarId).OnDelete(DeleteBehavior.Restrict);

        }
    }
}
