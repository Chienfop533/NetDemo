using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetDemo.Data.Config
{
    public class RoleConfig : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.Name).HasMaxLength(250).IsRequired();
            builder.Property(n => n.IsActive).IsRequired();
            builder.Property(n => n.isDeleted).IsRequired();
            builder.Property(n => n.CreatedDate).IsRequired();
        }
    }
}
