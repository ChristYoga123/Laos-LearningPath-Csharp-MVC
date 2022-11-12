using System.ComponentModel.DataAnnotations;

namespace Laos_LearningPath_Backend.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string name { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool is_admin { get; set; }
    }
}
