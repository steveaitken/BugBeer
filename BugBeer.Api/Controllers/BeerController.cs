using System.Collections.Generic;
using System.Web.Http;
using BugBeer.Dal;
using BugBeer.Models;

namespace BugBeer.Api.Controllers
{
    public class BeerController : ApiController
    {
        private IBugBeerRepository<Beer> repo;

        public BeerController(IBugBeerRepository<Beer> repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        [Route("beer")]
        public IEnumerable<Beer> Get()
        {
            return repo.FindAll();
        }

        [HttpGet]
        [Route("beer/{id}")]
        public Beer Get(string id)
        {
            return repo.FindOneById(id);
        }

        [HttpPost]
        [Route("Beer/Create")]
        public void Post([FromBody]Beer beer)
        {
            repo.Save(beer);
        }

        [HttpPut]
        [Route("Beer/{id}")]
        public void Put(string id, [FromBody]Beer beer)
        {
            repo.Save(beer);
        }

        [HttpDelete]
        [Route("Beer/{id}")]
        public void Delete(string id)
        {
            repo.Remove(id);
        }
    }
}
