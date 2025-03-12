using System.Collections.Generic;
using Cau4.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Cau4.Models
{
    public class GoodsDbContext : DbContext
    {
        public GoodsDbContext(DbContextOptions<GoodsDbContext> options) : base(options)
        {
        }

        public DbSet<HangHoa> Goods { get; set; }
        public class GoodsDbContextFactory : IDesignTimeDbContextFactory<GoodsDbContext>
        {
            public GoodsDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<GoodsDbContext>();
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-DN3PCHGN;Initial Catalog=GoodDB;Integrated Security=True;Encrypt=False;Trust Server Certificate=True;");
                return new GoodsDbContext(optionsBuilder.Options);
            }
        }
    }
}