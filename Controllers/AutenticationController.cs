using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ticket_support_api.Models;
using ticket_support_api.Services.Users;

namespace ticket_support_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutenticationController : ControllerBase
    {
        private ServiceUserRegister RegisterUserService = new ServiceUserRegister();
        private readonly ArrayList listaUsuario = new ArrayList();
        private readonly ArrayList listaAdministradores = new ArrayList();
        private readonly ArrayList CurrentUserLogged = new ArrayList();
        private List<string> RolEncontrado = new List<string>();

        [HttpPost]
        public async Task<IActionResult> GetUserAuth([FromBody] UserRegisterModel credentials)
        {
            string[] roles = { "User", "Admin" };
           

            var sesionCurrentUsers = await RegisterUserService.GetCurrentUsers();
            var sesionCurrentAdmins = await RegisterUserService.GetCurrentAdmin();

            foreach (var datos in sesionCurrentUsers)
            {
                listaUsuario.Add(datos.Email);
                listaUsuario.Add(datos.Pass);

            }
            foreach (var datos in sesionCurrentAdmins)
            {
                listaAdministradores.Add(datos.Email);
                listaAdministradores.Add(datos.Pass);

            }
            if (credentials == null)
            {
                return NoContent();
            }
            else
            {
                if (listaUsuario.Contains(credentials.Email) && listaUsuario.Contains(credentials.Pass))
                {

                    var session = RegisterUserService.GetCurrentSessionUser(credentials.Email!);
                    foreach (UserRegisterModel item in session.Result)
                    {
                        CurrentUserLogged.Add(item.Name);
                        CurrentUserLogged.Add(item.LastName);
                        CurrentUserLogged.Add(item.Email);
                        CurrentUserLogged.Add(item.Role);
                        CurrentUserLogged.Add(item.Phone);
                    }
                   
                    return Ok(CurrentUserLogged);
                }
                else if (listaAdministradores.Contains(credentials.Email) && listaAdministradores.Contains(credentials.Pass))
                {
                    var session = RegisterUserService.GetCurrentSessionAdministrator(credentials.Email!);
                    foreach (UserAdminRegisterModel item in session.Result)
                    {
                        CurrentUserLogged.Add(item.Name);
                        CurrentUserLogged.Add(item.LastName);
                        CurrentUserLogged.Add(item.Email);
                        CurrentUserLogged.Add(item.Role);

                    }
                    
                    return Ok(CurrentUserLogged);
                }
                else
                {
                    return NotFound("El Usuario no se encuentra registrado!");
                }
            }
        }
     

    }
}
