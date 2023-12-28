using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ticket_support_api.Models;
using ticket_support_api.Services.LogsTickets;
using ticket_support_api.Services.Tickets;

namespace ticket_support_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogTicketsController : ControllerBase
    {
        private LogsTickets LogsTicketsService = new LogsTickets();
        private ServiceTickets ticketsService = new ServiceTickets();    
       

        [HttpPost]
        public async Task<IActionResult> SaveTicketHistory([FromBody] LogTicketModel record) 
        {
            if (record != null)
            {
                await ticketsService.DeleteTicket(record._id!);

                record._id = "";

                await LogsTicketsService.CreateLogTicket(record);

           
                return Ok("SE GUARDO EL REGISTRO DEL TICKET: "+record.TicketNumber);
            }
            else
            {
                return BadRequest("OCURRIO UN ERROR AL INSERTA LA INFORMACION AL REGISTRO");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetLogsTickets()
        {
            return Ok(await LogsTicketsService.GetCurrentTicketsLogs());
        }
      
    }
}
