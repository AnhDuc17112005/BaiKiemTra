using Microsoft.EntityFrameworkCore;
namespace Cau123.Models
{
    public class GoodsDbContext : DbContext
    {
        public GoodsDbContext(DbContextOptions<GoodsDbContext> options) : base(options)
        {
        }

        // Constructor mặc định cho design-time (Migration)
        public GoodsDbContext()
        {
        }

        public DbSet<HangHoa> Goods { get; set; }

        // Cấu hình chuỗi kết nối trong OnConfiguring (dùng cho Migration)
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Chuỗi kết nối mặc định nếu không được cấu hình qua DI
                optionsBuilder.UseSqlServer("Data Source=SQL1002.site4now.net;Initial Catalog=db_ab624d_gooddb;User Id=db_ab624d_gooddb_admin;Password=wwe123456789");
            }
        }
    }
}
