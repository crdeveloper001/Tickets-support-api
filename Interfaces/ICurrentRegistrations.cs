using System.Collections.Generic;
using System.Threading.Tasks;
using ticket_support_api.Models;

namespace ticket_support_api.Interfaces
{
    interface ICurrentRegistrations
    {
        Task<List<CurrentRegistrationModel>> GetCurrentRegistrations();
        Task ApproveUser(CurrentRegistrationModel newUser,string EmailService,string PasswordService);
        Task RejectUser(string id);
    }
}
