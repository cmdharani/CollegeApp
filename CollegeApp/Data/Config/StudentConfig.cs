using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CollegeApp.Data.Config
{
    public class StudentConfig:IEntityTypeConfiguration<Student>
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


            List<Student> studentsList = new List<Student> {


             new Student { Id = 1, Address="Bangalore", Email="Test@1", StudentName="Madhu", DOB=new DateTime(1982,3,3) },
             new Student { Id = 2, Address="Bangalore", Email="Test@2", StudentName="Dharani",DOB=new DateTime(2000,4,3)  },
             new Student { Id = 3, Address="Bangalore", Email="Test@3", StudentName="MadhuDharani",DOB=new DateTime(1982,3,3)  },
             new Student { Id = 4, Address="Bangalore", Email="Test@4", StudentName="DharaniMadhu",DOB=new DateTime(2001,3,3)  },
            };

            builder.HasData(studentsList);

        }
    }
}
