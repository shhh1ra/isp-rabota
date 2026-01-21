using System;
using System.ComponentModel.DataAnnotations;

namespace AutoSalon.Desktop.Data.Models
{
    public class Car
    {
        public int CarId { get; set; }

        public int BrandId { get; set; }
        public Brand Brand { get; set; } = null!;

        [Required, MaxLength(17), MinLength(17)]
        public string VIN { get; set; } = "";

        [Required, MaxLength(120)]
        public string ModelName { get; set; } = "";

        public int ModelYear { get; set; }

        [MaxLength(40)]
        public string? Color { get; set; }

        public decimal Price { get; set; }

        public int? MileageKm { get; set; }

        public bool IsSold { get; set; }

        public byte[]? Photo { get; set; }
        public string? PhotoFileName { get; set; }

        public DateTime CreatedAt { get; set; } // default в БД
    }
}
