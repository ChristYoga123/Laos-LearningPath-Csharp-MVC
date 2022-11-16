using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laos_LearningPath_Backend.Models
{
    public class Order
    {
        [Key]
        public int id { get; set; }
        [Required]
        [ForeignKey("User")]
        public int user_id { get; set; }
        public User? User { get; set; }
        [ForeignKey("Course")]
        public int course_id { get; set; }
        public Course? Course { get; set; }
        public bool status { get; set; }
    }
}
