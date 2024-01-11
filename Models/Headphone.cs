using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping.Models
{
    [Table("Headphone")]
    public class Headphone
    {
        [Key]
        [Column("ID_headphone")]
        public Guid IdHeadphone { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;

        [StringLength(100)]
        public string? Model { get; set; }

        [StringLength(100)]
        public string? Warranty { get; set; }

        [StringLength(100)]
        public string? BluetoothVersion { get; set; }

        [StringLength(100)]
        public string? Color { get; set; }

        [StringLength(100)]
        public string? DeviceType { get; set; }

        [StringLength(100)]
        public string? Impedance { get; set; }

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
    }

}
