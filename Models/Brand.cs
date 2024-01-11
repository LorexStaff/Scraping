using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Scraping.Models
{
    [Table("Brand")]
    public partial class Brand
    {
        [Key]
        [Column("ID_brand")]
        public Guid IdBrand { get; set; }

        [StringLength(100)]
        public string Name { get; set; } = null!;
    }
}