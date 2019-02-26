using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Lab7
{
    class Program
    {
        static void List()
        {
            using (var db = new AppDbContext())
            {
                var allStuff = db.Courses.Include(c => c.Enrollments).ThenInclude(e => e.Student);

                foreach (var course in allStuff)
                {
                    Console.WriteLine($"{course.CourseName} -");
                    foreach (var student in course.Enrollments)
                    {
                        Console.WriteLine($"\t{student.Student.FirstName} {student.Student.LastName} {student.GPA}");
                    }
                    Console.WriteLine();
                }
            }
        }
        static void Main(string[] args)
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                List<Student> students = new List<Student>() {
                    new Student {FirstName = "Jake", LastName = "Smith"},
                    new Student {FirstName = "Jenny", LastName = "Johnson"},
                    new Student {FirstName = "Will", LastName = "Jones"},
                    new Student {FirstName = "Samantha", LastName = "Williams"},
                };

                List<Course> courses = new List<Course>() {
                    new Course {CourseName = "History"},
                    new Course {CourseName = "CIS"}
                };

                List<Enrollment> joinTable = new List<Enrollment>() {
                    new Enrollment {Student = students[0], Course = courses[0], GPA = 3.2},
                    new Enrollment {Student = students[1], Course = courses[0], GPA = 4},
                    new Enrollment {Student = students[2], Course = courses[0], GPA = 2.75},
                    new Enrollment {Student = students[3], Course = courses[0], GPA = 3.1},
                    new Enrollment {Student = students[1], Course = courses[1], GPA = 2.57},
                    new Enrollment {Student = students[3], Course = courses[1], GPA = 2.3},
                    new Enrollment {Student = students[2], Course = courses[1], GPA = 2.75},
                };

                db.AddRange(students);
                db.AddRange(courses);
                db.AddRange(joinTable);
                db.SaveChanges();
            }
            List();

            using (var db = new AppDbContext())
            {
                int studentId = 3;
                int courseId = 1;
                
                // 5. Remove student "3" from course 1
                Enrollment eToRemove = db.Enrollments.Find(studentId, courseId);
                db.Remove(eToRemove);
                db.SaveChanges();

                Student newStudent = new Student {FirstName = "Jack", LastName = "Daniels"};
                db.Add(newStudent);
                db.SaveChanges();

                // Student studentToAdd = db.Students.Where(s => s.FirstName == "Jack");
                // Course courseToAdd = db.Courses.Where(c => c.CourseName == "CIS");
                // Enrollment newEnroll = new Enrollment {
                // Student = studentToAdd, // This is Employee A
                // Course = courseToAdd // This is Project 2
                // };
                // db.Add(newEnroll);
                // db.SaveChanges();

            }
            List();
        }
    }
}
