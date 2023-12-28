using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using ticket_support_api.Models;
using ticket_support_api.Services.SystemRegistrations;
using ticket_support_api.Services.Users;

namespace ticket_support_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private ServiceUserRegister RegisterUserService = new ServiceUserRegister();
        private ServiceRegistrations systemRegister = new ServiceRegistrations();
        private readonly IConfiguration configuration;
        private string SystemEmail = null!;
        private string SystemPass = null!;
        public RegisterController(IConfiguration _iConfig)
        {
            configuration = _iConfig;
        }
        [HttpPost]
        public async Task<IActionResult> PostNewUser([FromBody] UserRegisterModel user)
        {
            SystemEmail = configuration.GetSection("EmailService").GetSection("CorreoElectronico").Value!;
            SystemPass = configuration.GetSection("EmailService").GetSection("Password").Value!;

            if (user != null && user.Role!.Contains("User"))
            {
                await RegisterUserService.CreateUserAccount(user);

                return Ok("Se ha registrado el Usuario: " + user.Name);
            }
            else if (User != null && user!.Role == "Approve")
            {
                CurrentRegistrationModel UserData = new CurrentRegistrationModel()
                {
                    Name = user.Name,
                    LastName = user.LastName,
                    Phone = user.Phone,
                    Email = user.Email,
                    Pass = user.Pass,
                    Role = "User"
                };
                await systemRegister.ApproveUser(UserData,SystemEmail,SystemPass);

                await systemRegister.RejectUser(user._id);

                return Ok("Se ha registrado el cliente: " + UserData.Name + "como usuario del sistema");
            }
            else
            {
                return BadRequest("Ocurrio un error al registrar el usuario");
            }

        }
        [HttpGet]
        public async Task<IActionResult> GetRegistrations()
        {
            return Ok(await systemRegister.GetCurrentRegistrations());
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSuscription(string id)
        {
            if (id.Equals(""))
            {
                return BadRequest("ocurrio un error al ingresar el id");
            }
            else
            {
                await systemRegister.RejectUser(id);

                return Ok("Se la eliminado la suscripcion del cliente: " + id + " de la base de datos!");
            }
        }
    }
}
