using System.ComponentModel.DataAnnotations;

namespace AutoSalon.Desktop.Data.Models
{
    public class Brand
    {
        public int BrandId { get; set; }

        public int ManufacturerId { get; set; }
        public Manufacturer Manufacturer { get; set; } = null!;

        [Required, MaxLength(120)]
        public string Name { get; set; } = "";

        public DateTime CreatedAt { get; set; } // default в БД

        public List<Car> Cars { get; set; } = new();
    }
}
