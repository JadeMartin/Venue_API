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
    public class VenueControllerUnitTests
    {
        
        [TestMethod]
        public async Task GetAllVenues()
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
                VenueController controller = new VenueController(context);
                var result = await controller.Search(new Search
                {
                    City = "",
                    Q = "",
                    CategoryId = 0,
                    UserId = 0,
                    MinStarRating = 0,
                    MaxCostRating = 0,
                    SortBy = "",
                    RevereSort = false,
                });     
                List<Result> venueResult = (List<Result>)result.Value;
                //ASSERT
                Assert.AreEqual(venueResult.Count, 1);
            }
        }

        [TestMethod]
        public async Task GetVenueNoObjectFound()
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
                VenueController controller = new VenueController(context);
                int testId = 30;
                var result = await controller.GetVenue(testId); 
                //ASSERT
                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public async Task GetVenueWithValidVenueFound()
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
                VenueController controller = new VenueController(context);
                int testId = 1;
                var result = await controller.GetVenue(testId) as ObjectResult;
                Venue venueResult = (Venue) result.Value;
                //ASSERT
                Assert.AreEqual(venueResult.VenueName, "Mc donalds");
                Assert.AreEqual(venueResult.City, "Christchurch");
                Assert.AreEqual(venueResult.ShortDescription, "Miccy D's");
                Assert.AreEqual(venueResult.LongDescription, "Mac donalds resturant");
            }
        }

        [TestMethod]
        public async Task CreateVenueSuccessfulAndReturnsOkHttp()
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
                VenueController controller = new VenueController(context);

                string testVenueName ="Starbucks";
                int testUserId = 1;
                int testCategoryId = 1;
                string testCity = "Christchurch";
                string testShortDescription ="Starbucks cafe";
                string testLongDescription = "A place to get coffee";
                DateTime testDateAdded = new System.DateTime();
                double testLatitude = -43.5252;
                double testLongitude = 172.5919;

                var result = await controller.PostVenue(new Venue
                {
                    VenueName = testVenueName,
                    UserId = testUserId,
                    CategoryId = testCategoryId,
                    City = testCity,
                    ShortDescription = testShortDescription,
                    LongDescription = testLongDescription,
                    DateAdded = testDateAdded,
                    Latitude = testLatitude,
                    Longitude = testLongitude,
                }) as ObjectResult;
                Venue venueResult = (Venue) result.Value;
                //ASSERT
                Assert.AreEqual(venueResult.VenueName, testVenueName);
                Assert.AreEqual(venueResult.ShortDescription, testShortDescription);
                Assert.AreEqual(venueResult.LongDescription, testLongDescription);
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            }
        }
        [TestMethod]
        public async Task UpdateVenueSuccessfulAndReturnsOkHttp()
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
                VenueController controller = new VenueController(context);

                int testVenueId = 1;
                string testVenueName ="Mc donalds";
                int testUserId = 1;
                int testCategoryId = 1;
                string testCity = "Christchurch";
                string testShortDescription ="Fast food";
                string testLongDescription = "A fast food resturant that sells burgers!";
                double testLatitude = -43.5252;
                double testLongitude = 172.5919;

                var result = await controller.PutVenue(new Venue
                {
                    VenueId = testVenueId,
                    VenueName = testVenueName,
                    UserId = testUserId,
                    CategoryId = testCategoryId,
                    City = testCity,
                    ShortDescription = testShortDescription,
                    LongDescription = testLongDescription,
                    Latitude = testLatitude,
                    Longitude = testLongitude,
                }, testVenueId);
                var updatedVenue = await context.Venue.FindAsync(testVenueId);
                //ASSERT
                Assert.AreEqual(testShortDescription, updatedVenue.ShortDescription);
                Assert.AreEqual(testLongDescription, updatedVenue.LongDescription);
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            }
        }

        [TestMethod]
        public async Task UpdateVenueInvalidIdNotFound()
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
                VenueController controller = new VenueController(context);
                int testVenueId = 30;
                string testVenueName ="Mc donalds";
                int testUserId = 1;
                int testCategoryId = 1;
                string testCity = "Christchurch";
                string testShortDescription ="Fast food";
                string testLongDescription = "A fast food resturant that sells burgers!";
                double testLatitude = -43.5252;
                double testLongitude = 172.5919;
                var result = await controller.PutVenue(new Venue
                {
                    VenueId = testVenueId,
                    VenueName = testVenueName,
                    UserId = testUserId,
                    CategoryId = testCategoryId,
                    City = testCity,
                    ShortDescription = testShortDescription,
                    LongDescription = testLongDescription,
                    Latitude = testLatitude,
                    Longitude = testLongitude,
                }, testVenueId);
                // ASSERT
                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }
        
        [TestMethod]
        public async Task UpdateVenueBadRequestNotMatchingId()
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
                VenueController controller = new VenueController(context);
                int testVenueId = 23;
                string testVenueName ="Mc donalds";
                int testUserId = 1;
                int testCategoryId = 1;
                string testCity = "Christchurch";
                string testShortDescription ="Fast food";
                string testLongDescription = "A fast food resturant that sells burgers!";
                double testLatitude = -43.5252;
                double testLongitude = 172.5919;

                var result = await controller.PutVenue(new Venue
                {
                    VenueId = testVenueId,
                    VenueName = testVenueName,
                    UserId = testUserId,
                    CategoryId = testCategoryId,
                    City = testCity,
                    ShortDescription = testShortDescription,
                    LongDescription = testLongDescription,
                    Latitude = testLatitude,
                    Longitude = testLongitude,
                }, 30);
                //ASSERT
                Assert.IsInstanceOfType(result, typeof(BadRequestResult));
            }
        }
        [TestMethod]
        public async Task GetAllVenuesAfterVenueCreated()
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
                VenueController controller = new VenueController(context);
                Venue venueNew = await context.Venue.FindAsync(2);
                Console.Write(venueNew.VenueName);
                var result = await controller.Search(new Search
                {
                    City = "",
                    Q = "",
                    CategoryId = 0,
                    UserId = 0,
                    MinStarRating = 0,
                    MaxCostRating = 0,
                    SortBy = "",
                    RevereSort = true,
                });                
                List<Result> venueResult = (List<Result>)result.Value;
                //ASSERT
                Assert.AreEqual(venueResult.Count, 2);
            }
        }

        [TestMethod]
        public async Task SearchNoResult()
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
                VenueController controller = new VenueController(context);
                Venue venueNew = await context.Venue.FindAsync(2);
                Console.Write(venueNew.VenueName);
                var result = await controller.Search(new Search
                {
                    City = "Wellington",
                    Q = "",
                    CategoryId = 0,
                    UserId = 0,
                    MinStarRating = 0,
                    MaxCostRating = 0,
                    SortBy = "",
                    RevereSort = true,
                });                
                List<Result> venueResult = (List<Result>)result.Value;
                //ASSERT
                Assert.AreEqual(venueResult.Count, 0);
            }
        }

        [TestMethod]
        public async Task SearchTwoResultsCitySearch()
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
                VenueController controller = new VenueController(context);
                Venue venueNew = await context.Venue.FindAsync(2);
                Console.Write(venueNew.VenueName);
                var result = await controller.Search(new Search
                {
                    City = "Christchurch",
                    Q = "",
                    CategoryId = 0,
                    UserId = 0,
                    MinStarRating = 0,
                    MaxCostRating = 0,
                    SortBy = "",
                    RevereSort = true,
                });                
                List<Result> venueResult = (List<Result>)result.Value;
                //ASSERT
                Assert.AreEqual(venueResult.Count, 2);
            }
        }

        [TestMethod]
        public async Task SearchMultipleFieldsOneResult()
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
                VenueController controller = new VenueController(context);
                Venue venueNew = await context.Venue.FindAsync(2);
                Console.Write(venueNew.VenueName);
                var result = await controller.Search(new Search
                {
                    City = "Christchurch",
                    Q = "Star",
                    CategoryId = 1,
                    UserId = 0,
                    MinStarRating = 0,
                    MaxCostRating = 0,
                    SortBy = "",
                    RevereSort = true,
                });                
                List<Result> venueResult = (List<Result>)result.Value;
                //ASSERT
                Assert.AreEqual(venueResult.Count, 1);
            }
        }

        [TestMethod]
        public async Task SearchSortByStarRating()
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
                VenueController controller = new VenueController(context);
                Venue venueNew = await context.Venue.FindAsync(2);
                Console.Write(venueNew.VenueName);
                var result = await controller.Search(new Search
                {
                    City = "",
                    Q = "",
                    CategoryId = 1,
                    UserId = 0,
                    MinStarRating = 0,
                    MaxCostRating = 0,
                    SortBy = "",
                    RevereSort = false,
                });                
                List<Result> venueResult = (List<Result>)result.Value;
                //ASSERT
                Assert.AreEqual(venueResult[0].venue_name, "Mc donalds");
            }
        }

        [TestMethod]
        public async Task SearchSortByStarRatingReverse()
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
                VenueController controller = new VenueController(context);
                Venue venueNew = await context.Venue.FindAsync(2);
                Console.Write(venueNew.VenueName);
                var result = await controller.Search(new Search
                {
                    City = "",
                    Q = "",
                    CategoryId = 1,
                    UserId = 0,
                    MinStarRating = 0,
                    MaxCostRating = 0,
                    SortBy = "",
                    RevereSort = true,
                });                
                List<Result> venueResult = (List<Result>)result.Value;
                //ASSERT
                Assert.AreEqual(venueResult[0].venue_name, "Starbucks");
            }
        }

    }
}