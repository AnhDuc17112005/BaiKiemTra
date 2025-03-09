using Cau123.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Tr? v? JSON v?i tên thu?c tính nguyên b?n
    });

// K?t n?i CSDL qua chu?i k?t n?i t? appsettings.json
builder.Services.AddDbContext<GoodsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// H? tr? Swagger UI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// C?u hình CORS (cho phép t?t c? ngu?n g?c - tùy ch?nh theo nhu c?u)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
////////if (app.Environment.IsDevelopment())
////////{
    app.UseSwagger();
    app.UseSwaggerUI();
////////////}

// H? tr? HTTPS (b?t ho?c t?t tùy yêu c?u, m?c ??nh b?t trong môi tr??ng phát tri?n)
app.UseHttpsRedirection();

// S? d?ng CORS
app.UseCors("AllowAll");
app.UseDefaultFiles(); // Phục vụ index.html làm mặc định
app.UseStaticFiles();
app.UseAuthorization();

app.MapControllers();

app.Run();