using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BugBeer.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace BugBeer.Dal
{
    public class BugBeerRepository<T> : BugBeerRepository, BugBeer.Dal.IBugBeerRepository<T> where T : NamedEntity
    {
        private readonly string collectionName;

        public BugBeerRepository(string collectionName)
        {
            this.collectionName = collectionName;
        }

        protected MongoCollection<T> Entities
        {
            get
            {
                return Database.GetCollection<T>(collectionName);
            }
        }

        public IEnumerable<T> FindAll()
        {
            return Entities.FindAll();
        }

        public IEnumerable<T> FindAll(Expression<Func<T, bool>> query)
        {
            var mongoQ = Query<T>.Where(query);

            return Entities.FindAs<T>(mongoQ);
        }

        public T FindOneById(string id)
        {
            return Entities.FindOneById(new ObjectId(id));
        }

        public void Save(T entity)
        {
            Handle(OnSave, entity);

            var result = Entities.Save(entity);

            //TODO: result handling

            Handle(OnSaved, result);
        }

        public void Remove(string id)
        {
            Handle(OnRemove, id);

            var result = Entities.Remove(Query.EQ("_id", new ObjectId(id)));

            //TODO: result handling

            Handle(OnRemoved, result);
        }

        #region events and event handling

        public delegate void ContextHandler<ResultType>(object sender, ResultType result);

        public event ContextHandler<T> OnSave;
        public event ContextHandler<WriteConcernResult> OnSaved;

        public event ContextHandler<string> OnRemove;
        public event ContextHandler<WriteConcernResult> OnRemoved;

        private void Handle<ResultType>(ContextHandler<ResultType> handler, ResultType result)
        {
            if (handler != null)
            {
                handler(this, result);
            }
        }

        #endregion
    }
}
