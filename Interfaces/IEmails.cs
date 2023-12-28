using System.Collections.Generic;
using System.Threading.Tasks;
using ticket_support_api.Models;

namespace ticket_support_api.Interfaces
{
    interface IEmails
    {
        Task SendEmail(EmailMessageModel message, string email, string pass);

        Task<List<EmailMessageModel>> GetEmails();
    }
}
