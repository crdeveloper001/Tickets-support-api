using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ticket_support_api.Models;
using ticket_support_api.Services.ContactsServices;

namespace ticket_support_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private ContactService serviceContact = new ContactService();

        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            var contacts = await serviceContact.GetContacts();
            return Ok(contacts);
        }

        [HttpPost]
        public async Task<IActionResult> PostContact([FromBody] ContactModel contact)
        {
            if (contact != null)
            {
                await serviceContact.AddContact(contact);

                return Ok("EL CONTACTO HA SIDO AGREGADO A LA BASE DE DATOS");
            }
            else
            {
                return BadRequest("LA INFORMACION DEL CONTACTO EN INVALIDA, INTENTELO DE NUEVO!!");
            }
        }
    }
}
