using ManagerSIMS.Models;

namespace ManagerSIMS.Repository
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task AddUserAsync(User user);
        Task<Role> GetRoleByNameAsync(string roleName);
        Task SaveChangesAsync();

        User GetUserByEmail(string email);
    }
}
