using Zedit.Shanghai.Commons.Configuration;

namespace BugBeer.Dal
{
    static class Config
    {
        private static SettingsReader reader = new SettingsReader();

        public static string MongoDbConnectionString
        {
            get
            {
                return reader.GetConnectionString("mongodb");
            }
        }

        public static string MongoDbName
        {
            get
            {
                return reader.GetAppSetting<string>("MongoDbName");
            }
        }
    }
}
