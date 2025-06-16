using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace NetDemo.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).UseIdentityColumn();

            builder.Property(n => n.StudentName).IsRequired();
            builder.Property(n => n.StudentName).HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);

            builder.HasData(new List<Student>()
                {
                new Student {
                    Id = 1,
                    StudentName = "Student A",
                    Address = "A",
                    Email = "studenta@gmail.com",
                    DOB = new DateTime(2025, 12, 09)
                },
                 new Student {
                    Id = 2,
                    StudentName = "Student B",
                    Address = "B",
                    Email = "studentb@gmail.com",
                    DOB = new DateTime(2025, 12, 23)
                }
            });

            builder.HasOne(n => n.Department)
                .WithMany(n => n.Students)
                .HasForeignKey(n => n.DepartmentId)
                .HasConstraintName("Fk_Students_Department");
        }
    }
}
