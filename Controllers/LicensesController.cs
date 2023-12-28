using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ticket_support_api.Models;
using ticket_support_api.Services.LicenseServices;

namespace ticket_support_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicensesController : ControllerBase
    {
        private readonly LicensesServices licensesServices = new LicensesServices();

        [HttpGet]
        public async Task<IActionResult> GetLicenses()
        {
            return Ok(await licensesServices.GetCurrentLicenses());
        }
        [HttpPost]
        public async Task<IActionResult> Licenses([FromBody] LicensesModel license)
        {
            if (license != null)
            {
                await licensesServices.CreateLicense(license);

                return Ok("SE HA CREADO LA LICENCIA: "+license.LicenseName);
            }
            else
            {
                return BadRequest("EL OBJETO A INSERTAR ES INVALIDO");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLicenses(string id)
        {
            if (id != null)
            {
                await licensesServices.DeleteLicense(id);
                return Ok("LA LICENCIA: " + id + " SE HA ELIMINADO");
            }
            else
            {
                return BadRequest("OCURRIO UN ERROR AL VALIDAR EL ID O ES NULO");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutLicenses([FromBody] LicensesModel updateLicense)
        {
            if (updateLicense != null)
            {
                await licensesServices.UpdateLicenses(updateLicense._id, updateLicense);

                return Ok("SE ACTUALIZO LA LICENCIA: "+updateLicense.LicenseName);
            }
            else
            {
                return BadRequest("OCURRIO UN ERROR AL ACTUALIZAR LA LICENCIA: " + updateLicense!.LicenseName);
            }
        }
    }
}
