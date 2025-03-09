using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Cau123.Models
{
    [Table("Goods")]
    public class HangHoa
    {
        [Key]
        [StringLength(9, MinimumLength = 9)]
        public string? MaHangHoa { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string? TenHangHoa { get; set; }

        public int SoLuong { get; set; }
        public string? GhiChu { get; set; }
    }
}
