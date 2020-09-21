using System;
using System.Collections.Generic;
using System.Linq;
namespace API.Models
{
    public class Venue {
            public int VenueId { get; set; }
            public string  VenueName { get; set; }
            public int UserId {get;set;}
            public int  CategoryId { get; set; }
            public string  City { get; set; }
            public string  ShortDescription { get; set; }
            public string  LongDescription { get; set; }
            public DateTime DateAdded { get; set; }
            public double  Latitude { get; set; }
            public double  Longitude { get; set; }

        }
}