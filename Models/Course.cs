using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ManagerSIMS.Models;
public class Course
{
    public Course()
    {
        UserCourses = new List<UserCourse>(); // Đảm bảo không bị null
    }
    [Key]
    public int CourseId { get; set; }

    [Required]
    [StringLength(255)]
    public string CourseName { get; set; }

    [StringLength(1000)]
    public string Description { get; set; }

    [StringLength(100)]
    public string Category { get; set; }

    [Required]
    public int Duration { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    [ForeignKey("User")]
    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public virtual User? User { get; set; }

    public virtual ICollection<UserCourse> UserCourses { get; set; }
}

