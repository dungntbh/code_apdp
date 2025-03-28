using ManagerSIMS.Models;

namespace ManagerSIMS.Facade
{
    public interface IUserFacade
    {
        Task<bool> RegisterUserAsync(string username, string email, string password, string address, string roleName);
        User Authenticate(string email, string password);
    }
}
