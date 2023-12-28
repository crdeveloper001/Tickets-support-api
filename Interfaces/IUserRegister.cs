using System.Collections.Generic;
using System.Threading.Tasks;
using ticket_support_api.Models;

namespace ticket_support_api.Interfaces
{
    interface IUserRegister
    {
        Task CreateUserAccount(UserRegisterModel UserData);
        Task CreateAdminAccount(UserAdminRegisterModel AdminData);
        Task<List<UserRegisterModel>> GetCurrentUsers();
        Task<List<UserAdminRegisterModel>> GetCurrentAdmin();

        Task DeleteUser(string _id);

        Task UpdateUser(UserRegisterModel update);

        Task<List<UserRegisterModel>> GetCurrentSessionUser(string email);
        Task<List<UserAdminRegisterModel>> GetCurrentSessionAdministrator(string email);

    }
}
