using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Laos_LearningPath_Backend.Models
{
    public class Category
    {
        [Key]
        public int id { get; set; }

        [Required]
        public string name { get; set; }
    }
}
