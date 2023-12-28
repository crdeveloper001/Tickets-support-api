using System.Collections.Generic;
using System.Threading.Tasks;
using ticket_support_api.Models;

namespace ticket_support_api.Interfaces
{
    interface IInventoryItems
    {
        Task InsertItemToInventory(InventoryItemModel item);
        Task<List<InventoryItemModel>> GetInventorySelected(string category);
        Task<List<InventoryItemModel>> GetInventoryComputers();
        Task DeleteItemFromInventory(string _id,string category);
        Task UpdateItemFromInventory(string id,InventoryItemModel update);

    }
}
