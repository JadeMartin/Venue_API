
using System.Collections.Generic;

namespace API.Models
{
    public class Search {
            public string City { get; set; }
            public string Q { get; set; }
            public int CategoryId { get; set; }
            public int UserId { get; set; }
            public int MinStarRating { get; set; }
            public int MaxCostRating { get; set; }
            public string SortBy { get; set; }
            public bool RevereSort { get; set; }
        }
}