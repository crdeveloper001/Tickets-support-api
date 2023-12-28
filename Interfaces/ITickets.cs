using System.Collections.Generic;
using System.Threading.Tasks;
using ticket_support_api.Models;

namespace ticket_support_api.Interfaces
{
    interface ITickets
    {
        Task CreateTicket(TicketRequestModel newTickets,string emailSyS,string passSys);
        Task<List<TicketRequestModel>> GetCurrentTickets();
        Task UpdateTicket(string id, TicketRequestModel updateTicket);
        Task DeleteTicket(string id);
        Task<List<TicketRequestModel>> GetUserProfileTickets(string name);
    }
}
