using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ticket_support_api.Models;
using ticket_support_api.Services.InventoryService;

namespace ticket_support_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private InventoryServices inventoryServices = new InventoryServices();

        [HttpGet]
        public async Task<IActionResult> GetComputersInventory()
        {
            var computers = await inventoryServices.GetInventoryComputers();

            return Ok(computers);
        }
        [HttpGet("{CATEGORY}")]
        public async Task<IActionResult> GetInventorySelected(string Category)
        {
            List<InventoryItemModel> CategoryInventory = new List<InventoryItemModel>();
            if (Category == null || Category == "")
            {
                return BadRequest("LA CATEGORIA NO FUE SELECCIONADA O NO FUE ENCONTRADA!");
            }
            else
            {
                var FilterInventory = await inventoryServices.GetInventorySelected(Category);

                foreach (InventoryItemModel item in FilterInventory)
                {
                    CategoryInventory.Add(new InventoryItemModel
                    {
                        _id = item._id,
                       Code = item.Code,
                       Quantity = item.Quantity,
                       Tag = item.Tag,
                       Brand = item.Brand,
                       RoomLocation = item.RoomLocation,
                       Category = item.Category,
                       CurrentStatus = item.CurrentStatus
                    });
                }

                return Ok(CategoryInventory);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostItemInventory([FromBody] InventoryItemModel item)
        {
            if (item == null)
            {
                return NoContent();
            }
            else
            {
                await inventoryServices.InsertItemToInventory(item);

                return Ok("EL EQUIPO: "+item.Tag+" FUE INSERTADO EN LA BASE DE DATOS");
            }
        }
        [HttpPut]
        public async Task<IActionResult> UpdatedUItem([FromBody] InventoryItemModel item) {

            if (item == null)
            {
                return NoContent();
            }
            else
            {
                await inventoryServices.UpdateItemFromInventory(item.Category!, item);

                return Ok("EL EQUIPO: "+ item.Code+ "HA SIDO ACTUALIZADO");
            }
            
            
        }
        [HttpPost]
        [Route("DeleteItemInventory")]
        public async Task<IActionResult> DeleteItemInventory([FromBody] InventoryItemModel item)
        {
            if (item.Equals("") || item.Category!.Equals(""))
            {
                return BadRequest("ocurrio un error al ingresar el id o category");
            }
            else
            {
                await inventoryServices.DeleteItemFromInventory(item._id!, item.Category);

                return Ok("Se ha eliminado el item: " + item._id + " de la base de datos de inventario de: "+ item.Category);
            }
        }
    }
}
