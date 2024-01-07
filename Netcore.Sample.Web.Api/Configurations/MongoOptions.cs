namespace Netcore.Sample.Web.Api.Configurations
{
    public class MongoOptions
    {
        public const string Mongo = "Mongo";

        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
