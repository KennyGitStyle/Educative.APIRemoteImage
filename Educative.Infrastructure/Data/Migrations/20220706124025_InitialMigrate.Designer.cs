// <auto-generated />
using System;
using Educative.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Educative.Infrastructure.Data.Migrations
{
    [DbContext(typeof(EducativeContext))]
    [Migration("20220706124025_InitialMigrate")]
    partial class InitialMigrate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.5");

            modelBuilder.Entity("Educative.Core.Address", b =>
                {
                    b.Property<string>("AddressId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Add2")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Addr1")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("County")
                        .HasColumnType("TEXT");

                    b.Property<string>("Postcode")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentAddressId")
                        .HasColumnType("TEXT");

                    b.HasKey("AddressId");

                    b.HasIndex("StudentAddressId")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("Educative.Core.Course", b =>
                {
                    b.Property<string>("CourseId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseDescription")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseTopic")
                        .IsRequired()
                        .HasMaxLength(60)
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseTutor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("CourseId");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("Educative.Core.Entity.StudentCourse", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseId")
                        .HasColumnType("TEXT");

                    b.HasKey("StudentId", "CourseId");

                    b.HasIndex("CourseId");

                    b.ToTable("StudentCourses");
                });

            modelBuilder.Entity("Educative.Core.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<int>("Attendance")
                        .HasMaxLength(100)
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("DateOfBirth")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Lastname")
                        .HasColumnType("TEXT");

                    b.Property<char?>("MiddlenameInitial")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNo")
                        .HasColumnType("TEXT");

                    b.HasKey("StudentId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("Educative.Core.Address", b =>
                {
                    b.HasOne("Educative.Core.Student", "Student")
                        .WithOne("Address")
                        .HasForeignKey("Educative.Core.Address", "StudentAddressId");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Educative.Core.Entity.StudentCourse", b =>
                {
                    b.HasOne("Educative.Core.Course", "Course")
                        .WithMany("StudentCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Educative.Core.Student", "Student")
                        .WithMany("StudentCourses")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Course");

                    b.Navigation("Student");
                });

            modelBuilder.Entity("Educative.Core.Course", b =>
                {
                    b.Navigation("StudentCourses");
                });

            modelBuilder.Entity("Educative.Core.Student", b =>
                {
                    b.Navigation("Address");

                    b.Navigation("StudentCourses");
                });
#pragma warning restore 612, 618
        }
    }
}
