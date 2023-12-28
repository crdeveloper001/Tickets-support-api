using System.Collections.Generic;
using System.Threading.Tasks;
using ticket_support_api.Models;

namespace ticket_support_api.Interfaces
{
    interface ILogsTickets
    {
        Task CreateLogTicket(LogTicketModel newLogTicket);
        Task<List<LogTicketModel>> GetCurrentTicketsLogs();
    }
}
