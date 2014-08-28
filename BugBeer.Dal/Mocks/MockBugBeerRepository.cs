using System;
using System.Collections.Generic;
using System.Linq;
using BugBeer.Models;

namespace BugBeer.Dal.Mocks
{
    public class MockBugBeerRepository<T> : IBugBeerRepository<T> where T : NamedEntity
    {
        private readonly List<T> store = new List<T>();

        public MockBugBeerRepository()
        {
        }

        public MockBugBeerRepository(IEnumerable<T> seed)
        {
            store.AddRange(seed);
        }

        public IEnumerable<T> FindAll()
        {
            return store;
        }

        public IEnumerable<T> FindAll(System.Linq.Expressions.Expression<Func<T, bool>> query)
        {
            var w = query.Compile();
            return store.Where(w);
        }

        public T FindOneById(string id)
        {
            return store.FirstOrDefault(x => x.ID == id);
        }

        public void Remove(string id)
        {
            var obj = FindOneById(id);

            if (obj != null)
            {
                store.Remove(obj);
            }
        }

        public void Save(T entity)
        {
            Remove(entity.ID);
            store.Add(entity);
        }
    }
}
