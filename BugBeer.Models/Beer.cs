using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BugBeer.Models
{
    public class Beer : NamedEntity
    {
        public static Beer CreateNew()
        {
            return Initialize<Beer>(new Beer());
        }

        [BsonRepresentation(BsonType.Double)]
        public decimal Abv { get; set; }

        public string Notes { get; set; }
        
        public BeerStyle Style { get; set; }
        
        public Brewery Brewery { get; set; }
        
        public IList<BeerImage> Images { get; set; }

        private Beer()
        {
            Images = new List<BeerImage>();
        }
    }
}
