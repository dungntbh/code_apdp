using ManagerSIMS.Models;
using ManagerSIMS.Repository;

namespace ManagerSIMS.Facade
{
    public class CourseFacade : ICourseFacade
    {
        private readonly ICourseRepository _courseRepository;

        public CourseFacade(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IEnumerable<Course> GetAllCourses()
        {
            return _courseRepository.GetAllCourses();
        }

        public Course GetCourseById(int courseId)
        {
            return _courseRepository.GetCourseById(courseId);
        }

        public void CreateCourse(Course course)
        {
            _courseRepository.AddCourse(course);
            _courseRepository.Save();
        }

        public void EditCourse(Course course)
        {
            _courseRepository.UpdateCourse(course);
            _courseRepository.Save();
        }

        public void RemoveCourse(int courseId)
        {
            _courseRepository.DeleteCourse(courseId);
            _courseRepository.Save();
        }
    }


}
