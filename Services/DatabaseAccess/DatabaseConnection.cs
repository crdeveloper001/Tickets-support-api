using MongoDB.Driver;

namespace ticket_support_api.Services.DatabaseAccess
{
    public class DatabaseConnection
    {
        public MongoClient client;
        public IMongoDatabase database;

        public DatabaseConnection()
        {
         
            client = new MongoClient("mongodb+srv://TicketsAdminAdmin:XOxcmLRvhNEtrWCC@ticketsappenv.evpchpw.mongodb.net/");
            database = client.GetDatabase("TicketsAppDatabase");
         
        }
    }
}
