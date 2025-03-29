using ManagerSIMS.Models;

namespace ManagerSIMS.Repository
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAllCourses();
        Course GetCourseById(int courseId);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int courseId);
        void Save();
    }
}
