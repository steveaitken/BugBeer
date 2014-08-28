namespace BugBeer.Models
{
    public class Brewery : NamedEntity
    {
        public static Brewery CreateNew()
        {
            return Initialize<Brewery>(new Brewery());
        }

        private Brewery()
        {
        }
    }
}
