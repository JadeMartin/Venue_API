using API.Controllers;
using API.Models;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace Backend.Tests.UnitTests
{
    [TestClass]
    public class CategoryControllerUnitTests
    {
        
        [TestMethod]
        public async Task GetCategoryNoObjectFound()
        {
            //Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "VenueDb")
            .Options;
            //Create mocked context by added data
            using (var context = new ApplicationDbContext(options))
            {
                 DatabaseController db = new DatabaseController(context);
                 db.populate();
            }
            //ACT using Context instance with data to run test for code
            using (var context = new ApplicationDbContext(options))
            {
                CategoryController controller = new CategoryController(context);
                int testId = 30;
                var result = await controller.GetCategory(testId);
                //ASSERT
                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public async Task GetCategoryWithValidCategoryFound()
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
                CategoryController controller = new CategoryController(context);
                int testId = 1;
                var result = await controller.GetCategory(testId) as ObjectResult;
                Category caregoryResult = (Category) result.Value;
                //ASSERT
                Assert.AreEqual(caregoryResult.CategoryName, "Restaurant");
                Assert.AreEqual(caregoryResult.CategoryDescription, "A place to eat");
            }
        }

        [TestMethod]
        public async Task GetAllCategories()
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
                CategoryController controller = new CategoryController(context);
                var result = await controller.GetCategorys();
                List<Category> caregoryResult = (List<Category>)result.Value;
                Assert.AreEqual(caregoryResult.Count, 1);
            }
        }

        [TestMethod]
        public async Task CreateCategorySuccessfulAndReturnsOkHttp()
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
                CategoryController controller = new CategoryController(context);
                string testCategoryName ="Hotel";
                string testCategoryDescription = "A place to sleep";
                var result = await controller.PostCategory(new Category
                {
                    CategoryName = testCategoryName,
                    CategoryDescription = testCategoryDescription,
                }) as ObjectResult;
                Category categoryResult = (Category) result.Value;
                //ASSERT
                Assert.AreEqual(categoryResult.CategoryName, testCategoryName);
                Assert.AreEqual(categoryResult.CategoryDescription, testCategoryDescription);
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            }
        }

        [TestMethod]
        public async Task GetAllCategoriesAfterCreate()
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
                CategoryController controller = new CategoryController(context);
                var result = await controller.GetCategorys() ;
                List<Category> caregoryResult = (List<Category>)result.Value;
                Assert.AreEqual(caregoryResult.Count, 2);
            }
        }

    }
}