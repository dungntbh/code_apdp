using ManagerSIMS.Models;

namespace ManagerSIMS.Facade
{
    public interface ICourseFacade
    {
        IEnumerable<Course> GetAllCourses();
        Course GetCourseById(int courseId);
        void CreateCourse(Course course);
        void EditCourse(Course course);
        void RemoveCourse(int courseId);
    }
}
