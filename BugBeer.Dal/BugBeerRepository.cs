using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace BugBeer.Dal
{
    public class BugBeerRepository
    {
        private readonly MongoDatabase db;

        public MongoDatabase Database
        {
            get
            {
                return db;
            }
        }

        public BugBeerRepository()
        {
            db = new MongoClient(Config.MongoDbConnectionString)
                    .GetServer()
                    .GetDatabase(Config.MongoDbName);
        }

        internal MongoServerBuildInfo GetBuildInfo()
        {
            Database.GetStats();
            return Database.Server.BuildInfo;
        }
    }
}
