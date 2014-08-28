using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BugBeer.Models;

namespace BugBeer.Dal.Mocks
{
    public class MockBeerRepo : MockBugBeerRepository<Beer>
    {
        private static MockBeerRepo _default;

        public static MockBeerRepo Default
        {
            get
            {
                if (_default == null)
                {
                    _default = new MockBeerRepo();

                    for (int i = 1; i < 11; i++)
                    {
                        var beer = Beer.CreateNew();
                        beer.Name = string.Format("Beer {0}", i);
                        beer.Abv = i;
                        _default.Save(beer);
                    }
                }

                return _default;
            }
        }
    }
}
