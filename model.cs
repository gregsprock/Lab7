using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Lab7
{
    public class AppDbContext: DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=database.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Enrollment>()
                .HasKey(e => new {e.StudentId, e.CourseId});
        }
        public DbSet<Student> Students {get; set;}
        public DbSet<Course> Courses {get; set;}
        public DbSet<Enrollment> Enrollments {get; set;}
    }

    public class Student
    {
        public int StudentId {get; set;}
        public string FirstName {get; set;}
        public string LastName {get; set;}
        public List<Enrollment> Enrollments {get; set;} // Navigation Property. Student can have MANY Enrollments
    }

    public class Course
    {
        public int CourseId {get; set;}
        public string CourseName {get; set;}
        public List<Enrollment> Enrollments {get; set;} // Navigation Property. Course can have MANY Enrollments
    }

    public class Enrollment
    {
        public int StudentId {get; set;} // Composite Primary Key, Foreign Key 1
        public int CourseId {get; set;} // Composite Primary Key, Foreign Key 2
        public Student Student {get; set;} // Navigation Property. One student per Enrollment
        public Course Course {get; set;} // Navigation Property. One Course per Enrollment
        public double GPA {get; set;}
    }
}