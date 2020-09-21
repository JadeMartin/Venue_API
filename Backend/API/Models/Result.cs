
using System.Collections.Generic;

namespace API.Models
{
    public class Result {
            public int venue_id { get; set; }
            public int user_id { get; set; }
            public int category_id { get; set; }
            public string venue_name {get; set;}
            public string city { get; set; }
            public string short_description { get; set; }
            public string long_description { get; set; }
            public double latitude { get; set; }
            public double longitude { get; set; }
            public double mean_star_rating { get; set; }
            public double mean_cost_rating { get; set; }
        }
}