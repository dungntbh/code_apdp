using ManagerSIMS.Models;
using ManagerSIMS.Repository;

namespace ManagerSIMS.Facade
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserRepository _userRepository;

        public UserFacade(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> RegisterUserAsync(string username, string email, string password, string address, string roleName)
        {
            var existingUser = await _userRepository.GetUserByEmailAsync(email);
            if (existingUser != null) return false; // Email đã tồn tại

            var role = await _userRepository.GetRoleByNameAsync(roleName);
            if (role == null) return false; // Vai trò không hợp lệ

            var newUser = new User
            {
                Username = username,
                Email = email,
                Password = password, // Không hash theo yêu cầu
                Address = address,
                RoleId = role.RoleId,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddUserAsync(newUser);
            await _userRepository.SaveChangesAsync();
            return true;
        }
        public User Authenticate(string email, string password)
        {
            var user = _userRepository.GetUserByEmail(email);
            if (user == null || user.Password != password)
                return null; // Trả về null nếu đăng nhập sai

            return user;
        }
    }
}
