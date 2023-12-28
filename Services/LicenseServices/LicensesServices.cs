using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using ticket_support_api.Interfaces;
using ticket_support_api.Models;
using ticket_support_api.Services.DatabaseAccess;

namespace ticket_support_api.Services.LicenseServices
{
    public class LicensesServices : ILicenses
    {
        internal DatabaseConnection accessDB = new DatabaseConnection();

        private readonly IMongoCollection<LicensesModel> CollectionLicenses;

        public LicensesServices()
        {
            CollectionLicenses = accessDB.database.GetCollection<LicensesModel>("CurrentLicenses");
        }
        public async Task CreateLicense(LicensesModel newLicense)
        {
         await CollectionLicenses.InsertOneAsync(newLicense);
        }
        public async Task DeleteLicense(string id)
        {
            var FiltroConsulta = Builders<LicensesModel>.Filter.Eq(X => X._id, id);

            await CollectionLicenses.DeleteOneAsync(FiltroConsulta);
        }
        public async Task<List<LicensesModel>> GetCurrentLicenses()
        {
            return await CollectionLicenses.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
        public async Task UpdateLicenses(string id, LicensesModel updateLicense)
        {
            var FiltroConsulta = Builders<LicensesModel>.Filter.Eq(X => X._id, updateLicense._id);

            await CollectionLicenses.ReplaceOneAsync(FiltroConsulta, updateLicense);
        }
    }
}
