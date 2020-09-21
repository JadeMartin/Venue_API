// using Backend.Models;

// namespace Backend 
// {
//     public class PopulateDb {
//         public void populateUsers()
//         {
//             User newUserOne = new User 
//             {
//                 UserId = 1,
//                 FirstName = "Bill",
//                 LastName = "Billingson",
//             };
//             _dbContext.Add(newUserOne);
//             User newUserTwo = new User 
//             {
//                 UserId = 2,
//                 FirstName = "Bob",
//                 LastName = "The family man",
//             };
//             _dbContext.Add(newUserTwo);
//         }
//         public void populateVenues()
//         {
//             Venue newVenueOne = new Venue 
//             {
//                 VenueId = 1,
//                 VenueName = "Mc donalds",
//                 UserId = 1,
//                 CategoryId = 1,
//                 City = "Christchurch",
//                 ShortDescription  = "Miccy D's",
//                 LongDescription = "Mac donalds resturant",
//                 DateAdded = new System.DateTime(),
//                 Latitude = 38.8951, 
//                 Longitude = -77.0364
//             };
//             _dbContext.Add(newVenueOne);
//         }
//         public void populateReviews()
//         {
//             Review newReview = new Review 
//             {
//                 ReviewId = 1,
//                 VenueId = 1,
//                 UserId =  2,
//                 ReviewBody = "A terrible place to take the family",
//                 StarRating = 2,
//                 CostRating = 3,
//                 TimePosted = new System.DateTime()
//             };
//             _dbContext.Add(newReview);
//         }
//         public void populateCategories()
//         {
//             Category newCategoryOne = new Category 
//             {
//                 CategoryId = 1,
//                 CategoryName = "Restaurant",
//                 CategoryDescription = "A place to eat"
//             };
//             _dbContext.Add(newCategoryOne);
//         }   
//     }
// }