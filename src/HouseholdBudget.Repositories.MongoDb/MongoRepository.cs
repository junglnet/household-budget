using MongoDB.Driver;


namespace HouseholdBudget.Repositories.MongoDb
{
    public sealed class MongoRepository<T>
    {
        private readonly IMongoClient _mongoClient;
        private readonly IMongoDatabase _mongoDatabase;


        public MongoRepository (
            string collectionName, 
            string connectionString, 
            string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;

            // ConnectionString = "mongodb://localhost:27017";
            // DatabaseName = "householdbudget";
            this._mongoClient = new MongoClient(ConnectionString);
            this._mongoDatabase = _mongoClient.GetDatabase(DatabaseName);
            this.Collection = this._mongoDatabase.GetCollection<T>(collectionName);


        }

        public IMongoCollection<T> Collection { get; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }


    }
}
