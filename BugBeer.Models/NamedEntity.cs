using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace BugBeer.Models
{
    public abstract class NamedEntity
    {
        protected static T Initialize<T>(T entity) where T : NamedEntity
        {
            entity.ID = ObjectId.GenerateNewId().ToString();
            entity.CreatedOn = DateTime.Now;
            return entity;
        }

        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string ID { get; set; }

        public string Name { get; set; }

        public DateTime CreatedOn { get; set; }

        public User CreatedBy { get; set; }

        protected NamedEntity()
        {
        }

        public override bool Equals(object obj)
        {
            var other = obj as NamedEntity;

            if (other == null)
            {
                return false;
            }

            return other.ID == this.ID;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", this.GetType().Name, this.ID);
        }

        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
