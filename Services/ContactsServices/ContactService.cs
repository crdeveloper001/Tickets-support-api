using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ticket_support_api.Interfaces;
using ticket_support_api.Models;
using ticket_support_api.Services.DatabaseAccess;

namespace ticket_support_api.Services.ContactsServices
{
    public class ContactService : IContacts
    {
        internal DatabaseConnection accessDB = new DatabaseConnection();

        private readonly IMongoCollection<ContactModel> CollectionContacts;
        public ContactService()
        {
            CollectionContacts = accessDB.database.GetCollection<ContactModel>("Contacts");
        }
        public async Task AddContact(ContactModel contactInfo)
        {
             await CollectionContacts.InsertOneAsync(contactInfo);
        }

        public async Task<List<ContactModel>> GetContacts()
        {
            return await CollectionContacts.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
    }
}
