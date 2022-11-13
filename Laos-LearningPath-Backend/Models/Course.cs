using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace Laos_LearningPath_Backend.Models
{
    public class Course
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string image { get; set; }
        public string description { get; set; }
        [ForeignKey("Category")]
        public int category_id { get; set; }
        public Category Category { get; set; }
        public int price { get; set; }
    }
}
