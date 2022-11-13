using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laos_LearningPath_Backend.Models
{
    public class Lesson
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string url { get; set; }
        [ForeignKey("Course")]
        public int course_id { get; set; }

        public Course Course { get; set; }
    }
}
