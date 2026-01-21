using System.ComponentModel.DataAnnotations;

namespace AutoSalon.Desktop.Data.Models
{
    public class Role
    {
        public int RoleId { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = ""; // Admin / Viewer

        public List<User> Users { get; set; } = new();
    }
}
