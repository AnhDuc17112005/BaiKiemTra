using Cau123.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Causo1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangHoaController : ControllerBase
    {
        private readonly GoodsDbContext _context;

        public HangHoaController(GoodsDbContext context)
        {
            _context = context;
        }

        // READ: Lấy tất cả hàng hóa (GET: api/HangHoa)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HangHoa>>> GetHangHoa()
        {
            var hangHoas = await _context.Goods.ToListAsync();
            return await _context.Goods.ToListAsync();
        }

        // READ: Lấy hàng hóa theo khóa chính (2 kiểu truyền tham số)
        // Kiểu 1: api/HangHoa/ABC123456
        [HttpGet("{maHangHoa}")]
        public async Task<ActionResult<HangHoa>> GetHangHoaById(string maHangHoa)
        {
            var hangHoa = await _context.Goods.FindAsync(maHangHoa);
            if (hangHoa == null)
            {
                return NotFound();
            }
            return hangHoa;
        }

        // Kiểu 2: api/HangHoa?id=ABC123456
        [HttpGet("byId")]
        public async Task<ActionResult<HangHoa>> GetHangHoaByQuery([FromQuery] string id)
        {
            var hangHoa = await _context.Goods.FindAsync(id);
            if (hangHoa == null)
            {
                return NotFound();
            }
            return hangHoa;
        }

        // CREATE: Thêm hàng hóa (POST: api/HangHoa)
        [HttpPost]
        public async Task<ActionResult<HangHoa>> PostHangHoa(HangHoa hangHoa)
        {
            _context.Goods.Add(hangHoa);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetHangHoaById), new { maHangHoa = hangHoa.MaHangHoa }, hangHoa);
        }

        // UPDATE: Cập nhật hàng hóa (PUT: api/HangHoa/ABC123456)
        [HttpPut("{maHangHoa}")]
        public async Task<IActionResult> PutHangHoa(string maHangHoa, HangHoa hangHoa)
        {
            if (maHangHoa != hangHoa.MaHangHoa)
            {
                return BadRequest("Mã hàng hóa không khớp");
            }

            var existingHangHoa = await _context.Goods.FindAsync(maHangHoa);
            if (existingHangHoa == null)
            {
                return NotFound(); // Trả về 404 nếu không tìm thấy
            }

            existingHangHoa.TenHangHoa = hangHoa.TenHangHoa;
            existingHangHoa.SoLuong = hangHoa.SoLuong;
            existingHangHoa.GhiChu = hangHoa.GhiChu;

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // DELETE: Xóa hàng hóa (DELETE: api/HangHoa/ABC123456)
        [HttpDelete("{maHangHoa}")]
        public async Task<IActionResult> DeleteHangHoa(string maHangHoa)
        {
            var hangHoa = await _context.Goods.FindAsync(maHangHoa);
            if (hangHoa == null)
            {
                return NotFound();
            }

            _context.Goods.Remove(hangHoa);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{maHangHoa}/update-note")]
        public async Task<IActionResult> UpdateGhiChu(string maHangHoa, [FromBody] string ghiChu)
        {
            var hangHoa = await _context.Goods.FindAsync(maHangHoa);
            if (hangHoa == null)
            {
                return NotFound();
            }

            hangHoa.GhiChu = ghiChu;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
