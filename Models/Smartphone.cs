using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scraping.Models
{

    [Table("Smartphone")]
    public partial class Smartphone
    {
        [Key]
        [Column("ID_smartphone")]
        public Guid IdSmartphone { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;
        [StringLength(100)]
        public string Screen { get; set; } = null!;
        [StringLength(100)]
        public string Processor { get; set; } = null!;
        [StringLength(100)]
        public string MainCamera { get; set; } = null!;
        [StringLength(100)]
        public string Ram { get; set; } = null!;
        [StringLength(100)]
        public string InternalMemory { get; set; } = null!;
        [StringLength(100)]
        public string BatteryCapacity { get; set; } = null!;

        [ForeignKey("Brand")]
        public Guid BrandId { get; set; }
        public Brand Brand { get; set; } = null!;
    }
}
