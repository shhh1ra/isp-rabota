using System.ComponentModel.DataAnnotations;

namespace AutoSalon.Desktop.Data.Models
{
    public class Manufacturer
    {
        public int ManufacturerId { get; set; }

        [Required, MaxLength(120)]
        public string Name { get; set; } = "";

        [MaxLength(80)]
        public string? Country { get; set; }

        [MaxLength(200)]
        public string? Website { get; set; }

        public DateTime CreatedAt { get; set; } // default в БД

        public List<Brand> Brands { get; set; } = new();
    }
}
