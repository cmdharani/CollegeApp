﻿using CollegeApp.Data.Config;
using Microsoft.EntityFrameworkCore;

namespace CollegeApp.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {

        }


        public DbSet<Student> Students { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new StudentConfig());

            //List<Student> studentsList = new List<Student> {


            // new Student { Id = 1, Address="Bangalore", Email="Test@1", StudentName="Madhu", DOB=new DateTime(1982,3,3) },
            // new Student { Id = 2, Address="Bangalore", Email="Test@2", StudentName="Dharani",DOB=new DateTime(2000,4,3)  },
            // new Student { Id = 3, Address="Bangalore", Email="Test@3", StudentName="MadhuDharani",DOB=new DateTime(1982,3,3)  },
            // new Student { Id = 4, Address="Bangalore", Email="Test@4", StudentName="DharaniMadhu",DOB=new DateTime(2001,3,3)  },

            //};

            //modelBuilder.Entity<Student>().HasData(studentsList);

            //modelBuilder.Entity<Student>(entity =>
            //{

            //    entity.Property(x => x.StudentName).IsRequired().HasMaxLength(100);
            //    entity.Property(x => x.Email).IsRequired().HasMaxLength(100);
            //    entity.Property(x => x.Address).IsRequired(false).HasMaxLength(300);

            //});

        }
    }
}
