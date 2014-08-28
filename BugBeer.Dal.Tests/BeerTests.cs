using System;
using System.Collections.Generic;
using BugBeer.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;

namespace BugBeer.Dal.Tests
{
    [TestClass]
    public class BeerTests
    {
        [TestMethod]
        public void ToDocument_BeerAbv_AsDouble()
        {
            var beer = Beer.CreateNew();

            beer.Abv = new decimal(1.0);

            var doc = beer.ToBsonDocument();

            Assert.IsTrue(doc["Abv"].IsDouble);
        }
        
        [TestMethod]
        public void ToDocument_BeerId_AsObjectId()
        {
            var beer = Beer.CreateNew();

            var doc = beer.ToBsonDocument();

            Assert.IsTrue(doc["_id"].IsObjectId);
        }

        [TestMethod]
        public void Beer_TestEquality()
        {
            var beer1 = Beer.CreateNew();
            var beer2 = Beer.CreateNew();

            Assert.AreNotEqual(beer1, beer2);
        }

        [TestMethod]
        public void Beer_TestHashCode()
        {
            var beer1 = Beer.CreateNew();
            var beer2 = Beer.CreateNew();

            var beers = new List<Beer>() { beer1, beer2 };

            Assert.AreEqual(2, beers.Count);
        }
    }
}
