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
    public class ReviewControllerUnitTests
    {
        
        [TestMethod]
        public async Task GetReviewWithReviewIdNoObjectFound()
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
                ReviewController controller = new ReviewController(context);
                int testId = 30;
                var result = await controller.GetVenueReviews(testId);
                //ASSERT
                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }
        [TestMethod]
        public async Task GetReviewWithUserIdNoObjectFound()
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
                ReviewController controller = new ReviewController(context);
                int testId = 30;
                var result = await controller.GetUserReviews(testId);
                //ASSERT
                Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            }
        }

        [TestMethod]
        public async Task GetReviewWithVenueIdValidReviewFound()
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
                ReviewController controller = new ReviewController(context);
                int testId = 1;
                var result = await controller.GetVenueReviews(testId) as ObjectResult;
                List<Review> reviewResult = (List<Review>)result.Value;
                //ASSERT
                Assert.AreEqual(reviewResult.Count, 2);
            }
        }

        [TestMethod]
        public async Task GetReviewWithUserIdValidReviewFound()
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
                ReviewController controller = new ReviewController(context);
                int testId = 1;
                var result = await controller.GetUserReviews(testId) as ObjectResult;
                List<Review> reviewResult = (List<Review>)result.Value;
                //ASSERT
                Assert.AreEqual(reviewResult.Count, 1);
            }
        }

        [TestMethod]
        public async Task CreateReviewSuccessfulAndReturnsOkHttp()
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
                ReviewController controller = new ReviewController(context);
                int venueId = 1;
                int userId = 1;
                string reviewBody ="Some review about reviewing stuff";
                float starRating = 3;
                float costRating = 4;
                var result = await controller.PostReview(new Review
                {
                    VenueId = venueId,
                    UserId = userId,
                    ReviewBody = reviewBody,
                    StarRating = starRating,
                    CostRating = costRating,
                }) as ObjectResult;
                Review reviewResult = (Review) result.Value;
                //ASSERT
                Assert.AreEqual(reviewResult.VenueId, venueId);
                Assert.AreEqual(reviewResult.UserId, userId);
                Assert.AreEqual(reviewResult.ReviewBody, reviewBody);
                Assert.AreEqual(reviewResult.StarRating, starRating);
                Assert.AreEqual(reviewResult.CostRating, costRating);
                Assert.IsInstanceOfType(result, typeof(OkObjectResult));
            }
        }
        [TestMethod]
        public async Task GetReviewWithUserIdValidReviewFoundAfterReviewCreated()
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
                ReviewController controller = new ReviewController(context);
                int testId = 1;
                var result = await controller.GetUserReviews(testId) as ObjectResult;
                List<Review> reviewResult = (List<Review>)result.Value;
                //ASSERT
                Assert.AreEqual(reviewResult.Count, 2);
            }
        }
    }
}