using System;
using System.Linq;
using BugBeer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BugBeer.Dal.Tests
{
    [TestClass]
    public class BeerContextTests
    {
        [ClassCleanup]
        public static void ClassCleanup()
        {
            var repo = new BugBeerRepository();
            repo.Database.DropCollection("beer");
        }

        [TestInitialize]
        public void TestInit()
        {
            var repo = new BugBeerRepository();
            repo.Database.DropCollection("beer");
        }

        [TestMethod]
        public void EmptyContext_BuildInfo()
        {
            var repo = new BugBeerRepository();
            var info = repo.GetBuildInfo();

            Assert.IsNotNull(info);
        }

        [TestMethod]
        public void BeerContext_FindOneByID()
        {
            var repo = new BugBeerRepository<Beer>("beer");

            var beer = Beer.CreateNew();

            beer.Name = "test beer";

            var id = beer.ID;

            repo.Save(beer);

            var foundBeer = repo.FindOneById(id);

            Assert.AreEqual(beer.Name, foundBeer.Name);
        }

        [TestMethod]
        public void BeerContext_Save_Success()
        {
            var repo = new BugBeerRepository<Beer>("beer");
            
            var beer = Beer.CreateNew();
            
            beer.Name = "test beer";

            repo.Save(beer);

            //TODO: error handling
        }

        [TestMethod]
        public void BeerContext_OnSave()
        {
            var repo = new BugBeerRepository<Beer>("beer");

            string savedName = null;

            repo.OnSave += (s, t) =>
            {
                savedName = t.Name;
            };

            var beer = Beer.CreateNew();

            beer.Name = "test beer";


            repo.Save(beer);

            Assert.AreEqual(beer.Name, savedName);
        }


        [TestMethod]
        public void BeerContext_OnSaved()
        {
            var repo = new BugBeerRepository<Beer>("beer");

            string insertId = null;

            repo.OnSaved += (s, t) =>
            {
                insertId = t.Upserted.ToString();
            };

            var beer = Beer.CreateNew();
            
            beer.Name =  "test beer";

            repo.Save(beer);

            Assert.IsNotNull(insertId);
        }

        [TestMethod]
        public void BeerContext_Delete_Success()
        {
            var repo = new BugBeerRepository<Beer>("beer");

            var beer = Beer.CreateNew();

            beer.Name = "test beer";

            repo.Save(beer);

            repo.Remove(beer.ID);

            var foundBeer = repo.FindOneById(beer.ID);

            Assert.IsNull(foundBeer);
        }

        [TestMethod]
        public void BeerContext_OnDelete()
        {
            var repo = new BugBeerRepository<Beer>("beer");

            string deletedId = null;

            repo.OnRemove += (s, t) =>
            {
                deletedId = t;
            };

            var beer = Beer.CreateNew();

            beer.Name = "test beer";
            
            repo.Save(beer);

            repo.Remove(beer.ID);

            Assert.AreEqual(beer.ID, deletedId);
        }


        [TestMethod]
        public void BeerContext_OnRemoved()
        {
            var repo = new BugBeerRepository<Beer>("beer");

            long affected = 0;

            repo.OnRemoved += (s, t) =>
            {
                affected = t.DocumentsAffected;
            };

            var beer = Beer.CreateNew();

            beer.Name = "test beer";

            repo.Save(beer);

            repo.Remove(beer.ID);

            Assert.AreEqual(1, affected);
        }

        [TestMethod]
        public void BeerContext_TestFindAll_Abv()
        {
            var repo = new BugBeerRepository<Beer>("beer");

            for (int i = 0; i < 10; i++)
            {
                var beer = Beer.CreateNew();
                beer.Name = string.Format("Beer {0}", i);
                beer.Abv = new decimal(i);

                repo.Save(beer);
            }

            var results = repo.FindAll(x => x.Abv >= 5);

            Assert.AreEqual(5, results.Count());
        }

    }
}
