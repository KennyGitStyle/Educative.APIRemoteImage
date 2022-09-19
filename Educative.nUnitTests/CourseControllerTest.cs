using Educative.API.Controllers;
using Educative.Core;
using Educative.Infrastructure.Interface;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Educative.nUnitTests
{
    public class CourseControllerTest
    {
        private readonly Mock<IUnitOfWork> unitOfWorkStub = new();
        

        [Test]
        public async Task GetCourseAsync_WithNonExistingCourse_ReturnsNotFound()
        {
            // Arrange
            unitOfWorkStub.Setup(u => u.CourseRepository
            .GetByIdAsync(It.IsAny<string>())).ReturnsAsync((Course)null);

            var controller = new DefaultCourseController(unitOfWorkStub.Object);

            string id = null;

            // Act
            var result  = await controller.GetCourseById(id);

            // Assert
            Assert.That(result.Result, Is.TypeOf<NotFoundObjectResult>());
        }

        [Test]
        public async Task GetCourseAsync_ExistingCourse_ReturnsFound()
        {
            // Arrange
            var course =  CreateRandomCourse();


            unitOfWorkStub.Setup(unit => unit.CourseRepository
            .GetByIdAsync(It.IsAny<string>())).ReturnsAsync(course[0]);

            var controllerUnit = new DefaultCourseController(unitOfWorkStub.Object);

            // Act

            var result  = await controllerUnit.GetCourseById(course[0].CourseId);

            var expectedController = await controllerUnit.GetCourseById("ARM"); 

            var finalResult = result.Value;

            var finalExpectedResult = expectedController.Value;

            // Assert

            finalResult.Should().BeEquivalentTo(finalExpectedResult,
                opts => opts.ComparingByMembers<Course>());

        }

        

        [Test]
        public async Task CreateCoursesAsync_CreateCourse_ReturnCreatedCourse()
        {

            // Arrange

            var courseToCreate = new Course()
            {
                CourseDescription = "Course for manage airlines",
                CourseId = "ARM",
                CourseName = "Airline Management",
                CourseTopic = "Airline Practice",
                CourseTutor = "Shelly-Ann Porter",
                StudentCourses = null

            };

            unitOfWorkStub.Setup(unit =>
                unit.CourseRepository.AddAsync(courseToCreate))
                .ReturnsAsync(courseToCreate);

            var controllerUnit = new DefaultCourseController(unitOfWorkStub.Object);

            // Act

            var result = await controllerUnit.AddCourse(courseToCreate);

            // Assert

            var createdCourse = (result.Result as CreatedAtRouteResult).Value as Course;

            courseToCreate.Should().BeEquivalentTo(createdCourse, opts => opts.ComparingByMembers<Course>());

        }

        [Test]
        public async Task UpdateCourseAsync_WithExistingCourse_ReturnNoContent()
        {
            // Arrange

            Course existing = BusinessCourse();

            unitOfWorkStub.Setup(unit => 
                unit.CourseRepository.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(existing);


            var courseToUpdate = new Course()
            {
                CourseId = "Bus",
                CourseName = "Business",
                CourseTutor = "Maye Teddman",
                CourseDescription = "Lorem ipsum dolor sit amet. Eos vero minima sit dolor ipsum qui dolorum sunt sed accusamus fugiat hic odio saepe non blanditiis veritatis. Cum itaque reprehenderit qui velit blanditiis est vero architecto in veniam voluptatem cum repellat voluptas eos corporis perferendis. Ab sunt harum ut illum veritatis aut nulla provident vel odit saepe.",
                CourseTopic = "Arts",
                Price = 1599,
                StudentCourses = null

            };

            var controller = new VersionOneCourseController(unitOfWorkStub.Object);

            // Act

            var result = await controller.V1UpdateCourse(existing.CourseId, courseToUpdate);

            var finalResult = result.Result;
            // Assert

            finalResult.Should().BeOfType<NoContentResult>();

        }
        [Test]
        public async Task DeleteCourseAsync_WithExistingCourse_ReturnNoContent()
        {
            // Arrange

            Course existing = BusinessCourse();

            unitOfWorkStub.Setup(unit => 
                unit.CourseRepository.GetByIdAsync(It.IsAny<string>())).ReturnsAsync(existing);



            var controller = new VersionOneCourseController(unitOfWorkStub.Object);

            // Act

            var result = await controller.V1DeleteCourse(existing.CourseId);

            var finalResult = result.Result;
            // Assert

            finalResult.Should().BeOfType<NoContentResult>();

        }

        private static Course BusinessCourse()
        {
            return new()
            {
                CourseId = "Bus",
                CourseName = "Business",
                CourseTutor = "Maye Teddman",
                CourseDescription = "Lorem ipsum dolor sit amet. Eos vero minima sit dolor ipsum qui dolorum sunt sed accusamus fugiat hic odio saepe non blanditiis veritatis. Cum itaque reprehenderit qui velit blanditiis est vero architecto in veniam voluptatem cum repellat voluptas eos corporis perferendis. Ab sunt harum ut illum veritatis aut nulla provident vel odit saepe.",
                CourseTopic = "Arts",
                Price = 12,
                StudentCourses = null
            };
        }

        private static List<Course> CreateRandomCourse()
        {
            var course = new List<Course>()
            {
                new()
                {
                    CourseDescription = "Course for manage airlines",
                    CourseId = "ARM",
                    CourseName = "Airline Management",
                    CourseTopic = "Airline Practice",
                    CourseTutor = "Shelly-Ann Porter",
                    StudentCourses = null, 
                    Price = 999
                },
                new()
                {
                    CourseDescription = "Best Practice in Jurnalism",
                    CourseId = "JRN",
                    CourseName = "Jurnalism Fundamental",
                    CourseTopic = "Jurnalism",
                    CourseTutor = "Junaid Sully",
                    StudentCourses = null,
                    Price = 990
                },
                new()
                {
                    CourseDescription = "WW2 what britain acheived",
                    CourseId = "HTR",
                    CourseName = "History WW2",
                    CourseTopic = "WW2",
                    CourseTutor = "Stanley Butcher",
                    StudentCourses = null,
                     Price = 990
                    
                }
            };
            
            return course;
        }

       
    }
}
