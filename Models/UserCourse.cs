using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ManagerSIMS.Models
{
    public class UserCourse
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User? User { get; set; } // Không cần `[Required]`

        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public virtual Course? Course { get; set; } // Không cần `[Required]`

        public DateTime EnrollmentDate { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "ongoing"; // Trạng thái khóa học (ongoing, completed, dropped)
    }
}
