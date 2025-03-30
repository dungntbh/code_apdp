using ManagerSIMS.Models;
using ManagerSIMS.Facade;
using ManagerSIMS.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace ManagerSIMS.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICourseFacade _courseFacade;
        private readonly ApplicationDbContext _context;

        public AdminController(ICourseFacade courseFacade, ApplicationDbContext dbContext)
        {
            _courseFacade = courseFacade;
            _context = dbContext;
        }

        public IActionResult ManagerCourse()
        {
            var courses = _courseFacade.GetAllCourses();
            return View(courses);
        }

        public IActionResult AddCourse()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCourse(Course course)
        {
            if (!ModelState.IsValid)
            {
                Console.WriteLine("❌ ModelState không hợp lệ! Dưới đây là các lỗi:");

                foreach (var key in ModelState.Keys)
                {
                    foreach (var error in ModelState[key].Errors)
                    {
                        Console.WriteLine($"- Trường '{key}': {error.ErrorMessage}");
                    }
                }

                ModelState.AddModelError("", "Có lỗi trong dữ liệu nhập. Vui lòng kiểm tra lại.");
                return View(course);
            }

            try
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = $"✅ Khóa học '{course.CourseName}' đã được thêm thành công!";
                return RedirectToAction(nameof(ManagerCourse));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Lỗi hệ thống khi thêm khóa học: {ex.Message}");
                ModelState.AddModelError("", "Đã xảy ra lỗi khi lưu dữ liệu. Vui lòng thử lại sau.");
                return View(course);
            }
        }


        public IActionResult EditCourse(int id)
        {
            var course = _courseFacade.GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditCourse(Course course)
        {
            if (ModelState.IsValid)
            {
                _courseFacade.EditCourse(course);
                return RedirectToAction(nameof(ManagerCourse));
            }
            return View(course);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var course = _courseFacade.GetCourseById(id);
            if (course == null) return NotFound();

            _courseFacade.RemoveCourse(id);

            // Quay về danh sách ngay sau khi xóa
            return RedirectToAction("ManagerCourse");
        }

    }

}
