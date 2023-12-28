using System.Collections.Generic;
using System.Threading.Tasks;
using ticket_support_api.Models;

namespace ticket_support_api.Interfaces
{
    interface IContacts
    {
        Task AddContact(ContactModel contactInfo);

        Task<List<ContactModel>> GetContacts();
    }
}
