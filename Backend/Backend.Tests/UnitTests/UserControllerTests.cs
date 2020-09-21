using API.Controllers;
using API.Models;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System;



namespace Backend.Tests.UnitTests
{
    [TestClass]
    public class UserControllerUnitTests
    {
        
        [TestMethod]
        public async Task GetAllUsers()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "VenueDb")
            .Options;
            //Create mocked context by added data
            using (var context = new ApplicationDbContext(options))
            {
                 DatabaseController db = new DatabaseController(context);
            }
            //ACT using Context instance with data to run test for code
            using (var context = new ApplicationDbContext(options))
            {
                UserController controller = new UserController(context);
                var result = await controller.GetUsers();
                List<User> userResult = (List<User>)result.Value;
                //ASSERT
                Assert.AreEqual(userResult.Count, 2);
            }
        }

        [TestMethod]
        public async Task GetUserNoObjectFound()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "VenueDb")
            .Options;
            //Create mocked context by added data
            using (var context = new ApplicationDbContext(options))
            {
                 DatabaseController db = new DatabaseController(context);
            }
            //ACT using Context instance with data to run test for code
            using (var context = new ApplicationDbContext(options))
            {
                UserController controller = new UserController(context);
                int testId = 30;
                var result = await controller.GetUser(testId); 
                //ASSERT
                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public async Task GetUserWithValidUserFound()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "VenueDb")
            .Options;
            //Create mocked context by added data
            using (var context = new ApplicationDbContext(options))
            {
                 DatabaseController db = new DatabaseController(context);
            }
            //ACT using Context instance with data to run test for code
            using (var context = new ApplicationDbContext(options))
            {
                UserController controller = new UserController(context);
                int testId = 1;
                var result = await controller.GetUser(testId) as ObjectResult;
                User userResult = (User) result.Value;
                //ASSERT
                Assert.AreEqual(userResult.FirstName, "Bill");
                Assert.AreEqual(userResult.LastName, "Billingson");
            }
        }

        [TestMethod]
        public async Task CreateUserSuccessfulAndReturnsOkHttp()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "VenueDb")
            .Options;
            //Create mocked context by added data
            using (var context = new ApplicationDbContext(options))
            {
                 DatabaseController db = new DatabaseController(context);
            }
            //ACT using Context instance with data to run test for code
            using (var context = new ApplicationDbContext(options))
            {
                UserController controller = new UserController(context);
                string testName ="John";
                string testLastName = "Doe";
                var result = await controller.PostUser(new User
                {
                    FirstName = testName,
                    LastName = testLastName,
                }) as ObjectResult;
                User userResult = (User) result.Value;
                //ASSERT
                Assert.AreEqual(userResult.FirstName, testName);
                Assert.AreEqual(userResult.LastName, testLastName);
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            }
        }
        [TestMethod]
        public async Task UpdateUserSuccessfulAndReturnsOkHttp()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "VenueDb")
            .Options;
            //Create mocked context by added data
            using (var context = new ApplicationDbContext(options))
            {
                 DatabaseController db = new DatabaseController(context);
            }
            //ACT using Context instance with data to run test for code
            using (var context = new ApplicationDbContext(options))
            {
                UserController controller = new UserController(context);
                string testName ="Bobby";
                string testLastName = "Bobbington";
                int id = 1;
                var result = await controller.PutUser(new User
                {
                    UserId = id,
                    FirstName = testName,
                    LastName = testLastName,
                }, id);
                var updatedUser = await context.User.FindAsync(id);
                //ASSERT
                Assert.AreEqual(testName, updatedUser.FirstName);
                Assert.AreEqual(testLastName, updatedUser.LastName);
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            }
        }

        [TestMethod]
        public async Task UpdateUserInvalidIdNotFound()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "VenueDb")
            .Options;
            //Create mocked context by added data
            using (var context = new ApplicationDbContext(options))
            {
                 DatabaseController db = new DatabaseController(context);
            }
            //ACT using Context instance with data to run test for code
            using (var context = new ApplicationDbContext(options))
            {
                UserController controller = new UserController(context);
                string testName ="Bobby";
                string testLastName = "Bobbington";
                int id = 20;
                var result = await controller.PutUser(new User
                {
                    UserId = id,
                    FirstName = testName,
                    LastName = testLastName,
                }, id);
                // ASSERT
                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }
        
        [TestMethod]
        public async Task UpdateUserBadRequestNotMatchingId()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "VenueDb")
            .Options;
            //Create mocked context by added data
            using (var context = new ApplicationDbContext(options))
            {
                 DatabaseController db = new DatabaseController(context);
            }
            //ACT using Context instance with data to run test for code
            using (var context = new ApplicationDbContext(options))
            {
                UserController controller = new UserController(context);
                string testName ="Bobby";
                string testLastName = "Bobbington";
                int id = 20;
                var result = await controller.PutUser(new User
                {
                    UserId = 1,
                    FirstName = testName,
                    LastName = testLastName,
                }, id);
                //ASSERT
                Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            }
        }
        [TestMethod]
        public async Task GetAllUsersAfterUserCreated()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "VenueDb")
            .Options;
            //Create mocked context by added data
            using (var context = new ApplicationDbContext(options))
            {
                 DatabaseController db = new DatabaseController(context);
            }
            //ACT using Context instance with data to run test for code
            using (var context = new ApplicationDbContext(options))
            {
                UserController controller = new UserController(context);
                var result = await controller.GetUsers();
                List<User> userResult = (List<User>)result.Value;
                //ASSERT
                Assert.AreEqual(userResult.Count, 3);
            }
        }
    }
}