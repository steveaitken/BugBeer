using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BugBeer.Models;

namespace BugBeer.Dal
{
    public interface IBugBeerRepository<T> where T : NamedEntity
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> FindAll(Expression<Func<T, bool>> query);
        T FindOneById(string id);
        void Remove(string id);
        void Save(T entity);
    }
}
