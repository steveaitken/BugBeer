namespace BugBeer.Models
{
    public class BeerImage : NamedEntity
    {
        public static BeerImage CreateNew()
        {
            return Initialize<BeerImage>(new BeerImage());
        }

        private BeerImage()
        {
        }
    }
}
