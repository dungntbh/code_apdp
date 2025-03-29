using ManagerSIMS.Facade;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ManagerSIMS.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserFacade _userFacade;

        public AccountController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password, string address, string role)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(role))
            {
                ViewBag.Error = "Vui lòng điền đầy đủ thông tin.";
                return View();
            }

            var result = await _userFacade.RegisterUserAsync(username, email, password, address, role);
            if (!result)
            {
                ViewBag.Error = "Đăng ký thất bại. Email đã tồn tại hoặc vai trò không hợp lệ.";
                return View();
            }

            return RedirectToAction("Login");
        }



        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var user = _userFacade.Authenticate(email, password);

            if (user == null)
            {
                ViewBag.Error = "Email hoặc mật khẩu không chính xác!";
                return View();
            }

            // Lưu session
            HttpContext.Session.SetInt32("UserId", user.UserId);

            if (user.RoleId.HasValue)
            {
                HttpContext.Session.SetInt32("RoleId", user.RoleId.Value);
            }
            else
            {
                HttpContext.Session.SetInt32("RoleId", 0); // Giá trị mặc định nếu RoleId bị null
            }
            // Kiểm tra sau khi lưu
            var userId = HttpContext.Session.GetInt32("UserId");
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (userId == null || roleId == null)
            {
                ViewBag.Error = "Lỗi hệ thống: Không thể lưu Session!";
                return View();
            }

            Console.WriteLine($"UserId: {userId}, RoleId: {roleId}");

            // Điều hướng theo vai trò
            return RedirectToAction("RedirectByRole");
        }

        public IActionResult RedirectByRole()
        {
            var roleId = HttpContext.Session.GetInt32("RoleId");

            if (roleId == null)
            {
                Console.WriteLine("Session Role is missing!");
                ViewBag.Error = "Phiên đăng nhập đã hết hạn. Vui lòng đăng nhập lại!";
                return RedirectToAction("Login");
            }

            Console.WriteLine($"Redirecting user with role: {roleId}");

            switch (roleId)
            {
                case 1:
                    return RedirectToAction("ManagerCourse", "Admin");
                case 2:
                    return RedirectToAction("ManagerGrade", "Faculty");
                case 3:
                    return RedirectToAction("ViewCourseGrade", "Student");
                default:
                    return RedirectToAction("Login");
            }
        }


        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}

