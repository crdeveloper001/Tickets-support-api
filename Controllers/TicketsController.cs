using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ticket_support_api.Models;
using ticket_support_api.Services.Tickets;

namespace ticket_support_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ServiceTickets TicketsService = new ServiceTickets();
        private readonly IConfiguration configuration;
        private string SystemEmail = null!;
        private string SystemPass = null!;

        public TicketsController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
        }
        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] TicketRequestModel ticket)
        {
            SystemEmail = configuration.GetSection("EmailService").GetSection("CorreoElectronico").Value!;
            SystemPass = configuration.GetSection("EmailService").GetSection("Password").Value!;

            if (ticket != null)
            {
                await TicketsService.CreateTicket(ticket,SystemEmail,SystemPass);

                return Ok("SE HA GENERADO EL TICKET: " + ticket.TicketNumber + "SERA ATENDIDO EN LO MAS PRONTO POSIBLE DEPENDIENDO DE LA COLA DE TICKETS");
            }
            else if (ticket!.Name == "" || ticket.Email == "" || ticket.Details == "")
            {
                return BadRequest("NO HAY DATOS SUFICIENTES PARA ABRIR ESTE TICKET!");

            }
            else
            {
                return NotFound("OCURRIO UN PROBLEMA AL CONECTARSE AL SERVIDOR O AL GENERAR EL TICKET, INTENTELO NUEVAMENTE");
            }

        }
      
        [HttpGet("{email}")]
        public async Task<IActionResult> GetClientTickets(string Email)
        {
            List<TicketRequestModel> TicketsFromUser = new List<TicketRequestModel>();

            if (Email.Equals(""))
            {
                return BadRequest("OCURRIO UN ERROR AL CARGAR LO TICKETS POR EL NOMBRE");
            }
            else
            {
                await Task.Run(() => {

                    var FilterTicket = TicketsService.GetUserProfileTickets(Email);

                    foreach (TicketRequestModel item in FilterTicket.Result)
                    {
                        TicketsFromUser.Add(new TicketRequestModel
                        {
                            TicketNumber = item.TicketNumber,
                            TypeRequest = item.TypeRequest,
                            Details = item.Details
                        });
                    }

                });
                return Ok(TicketsFromUser);
            }


        }
        [HttpGet]
        public async Task<IActionResult> GetTickets()
        {
            return Ok(await TicketsService.GetCurrentTickets());
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(string id)
        {
            if (id.Equals(""))
            {
                return BadRequest("EL ID DEL PRODUCTO NO ES VALIDO, VERIFIQUE LA INFORMACION CON EL ADMINISTRADOR");
            }
            else
            {
                await TicketsService.DeleteTicket(id);


                return Ok("EL TICKET: " + id + " HA SIDO COMPLETADO!");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTicket([FromBody] TicketRequestModel updatedTicket)
        {
            if (updatedTicket == null || updatedTicket.Equals(""))
            {
                return BadRequest("TICKET NO VALIDO, VERIFICAR INFORMACION");
            }
            else
            {
                await TicketsService.UpdateTicket(updatedTicket._id, updatedTicket);

                return Ok("EL CAMBIO FUE EXITOSO EN EL TICKET: "+updatedTicket.TicketNumber);
            }
        }
    }
}
