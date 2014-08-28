namespace BugBeer.Models
{
    public class BeerStyle : NamedEntity
    {
        public static BeerStyle CreateNew()
        {
            return Initialize<BeerStyle>(new BeerStyle());
        }

        private BeerStyle()
        {
        }
    }
}
