using ManagerSIMS.Data;
using ManagerSIMS.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagerSIMS.Repository
{
    public class CourseRepository : ICourseRepository
    {
        private readonly ApplicationDbContext _context;

        public CourseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _context.Courses.ToList();
        }

        public Course GetCourseById(int courseId)
        {
            return _context.Courses.Find(courseId);
        }

        public void AddCourse(Course course)
        {
            _context.Courses.Add(course);
        }

        public void UpdateCourse(Course course)
        {
            _context.Entry(course).State = EntityState.Modified;
        }

        public void DeleteCourse(int courseId)
        {
            var course = _context.Courses.Find(courseId);
            if (course != null)
            {
                _context.Courses.Remove(course);
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }

}

