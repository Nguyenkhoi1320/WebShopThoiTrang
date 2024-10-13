using Microsoft.Extensions.FileProviders;
using service;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddScoped<san_phamService>();
builder.Services.AddScoped<don_hangService>();
builder.Services.AddScoped<chi_tiet_don_hangService>();
builder.Services.AddScoped<ShoppingCartService>();
builder.Services.AddScoped<khach_hangService>();
builder.Services.AddScoped<quy_dinh_giam_giaService>();
builder.Services.AddScoped<ma_giam_giaService>();
builder.Services.AddScoped<thong_tin_van_chuyenService>();
builder.Services.AddScoped<nhan_vienService>();
builder.Services.AddScoped<su_dung_ma_giam_giaService>();
builder.Services.AddScoped<lich_su_thanh_toanService>();
builder.Services.AddScoped<NhaCungCapService>();
builder.Services.AddScoped<SizeService>();
builder.Services.AddScoped<danh_mucService>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}); 
builder.Services.AddHttpContextAccessor();
var app = builder.Build();
app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(                     
    name: "default",
    pattern: "{controller=Home}/{Action=Index}/{id?}");

app.Run();