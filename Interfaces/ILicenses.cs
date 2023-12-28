using System.Collections.Generic;
using System.Threading.Tasks;
using ticket_support_api.Models;

namespace ticket_support_api.Interfaces
{
    public interface ILicenses
    {
        Task CreateLicense(LicensesModel newLicense);
        Task<List<LicensesModel>> GetCurrentLicenses();
        Task UpdateLicenses(string? id, LicensesModel updateLicense);
        Task DeleteLicense(string id);
    }
}
