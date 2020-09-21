using System;

namespace API.Models
{
    public class Review {
            public int ReviewId { get; set; }
            public int VenueId { get; set; }
            public int UserId { get; set; }
            public string ReviewBody { get; set; }
            public float StarRating { get; set; }
            public float CostRating { get; set; }
            public DateTime TimePosted { get; set; }
        }
}