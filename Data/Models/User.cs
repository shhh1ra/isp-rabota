using System.ComponentModel.DataAnnotations;

namespace AutoSalon.Desktop.Data.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required, MaxLength(50)]
        public string Login { get; set; } = "";

        [Required, MaxLength(200)]
        public string PasswordHash { get; set; } = "";

        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public DateTime CreatedAt { get; set; } // default в БД
    }
}
