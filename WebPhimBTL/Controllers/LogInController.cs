using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPhimBTL.Common;
using WebPhimBTL.Models;

namespace WebPhimBTL.Controllers
{
	public class LogInController : Controller
	{
		DbphimContext db = new DbphimContext();
		[HttpGet]
		public async Task<IActionResult> Login()
		{


			if (HttpContext.Session.GetString("UserName") != null)
			{
				return RedirectToAction("index", "home");
			}
			if (HttpContext.Session.GetString("Admin") != null)
			{
				return RedirectToAction("DanhSachPhim", "admin");
			}
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> Login(TaiKhoan user)
		{
			if (ModelState.IsValid)
			{
				if (HttpContext.Session.GetString("UserName") == null && HttpContext.Session.GetString("Admin") == null)
				{
					var pass = MaHoa.GetMd5Hash16(user.PassWord);

					var obj = await db.TaiKhoans.Where(x => x.TenTaiKhoan == user.TenTaiKhoan &&
					x.PassWord == pass).FirstOrDefaultAsync();
					if (obj != null)
					{
						if (obj.PhanQuyen == 1)
						{
							HttpContext.Session.SetInt32("id", obj.MaTaiKhoan);
							HttpContext.Session.SetString("UserName", obj.TenTaiKhoan.ToString());
							HttpContext.Session.SetString("AnhDaiDien", obj.Anh != null ? obj.Anh.ToString() : "https://antimatter.vn/wp-content/uploads/2022/11/anh-avatar-trang-fb-mac-dinh.jpg");
							//HttpCookie authCookie = new HttpCookie("AuthCookie");
							//authCookie.Values["Username"] = "johnsmith";
							//authCookie.Values["UserID"] = "1234";
							//authCookie.Expires = DateTime.Now.AddDays(7);
							//Response.Cookies.Add(authCookie);
							return RedirectToAction("index", "home");
						}
						if (obj.PhanQuyen == 2)
						{
							HttpContext.Session.SetString("Admin", obj.TenTaiKhoan.ToString());
							HttpContext.Session.SetString("AnhDaiDien", obj.Anh != null ? obj.Anh.ToString() : "https://antimatter.vn/wp-content/uploads/2022/11/anh-avatar-trang-fb-mac-dinh.jpg");
							//Response.Cookies.Append(HttpContext.Session.GetString("Admin"), HttpContext.Session.GetString("password"), new CookieOptions
							//{
							//    Expires = DateTimeOffset.UtcNow.AddDays(7),
							//    HttpOnly = true
							//});
							return RedirectToAction("DanhSachPhim", "admin");
						}

					}
				}

			}
			return View();
		}
		public IActionResult Logout()
		{
			HttpContext.Session.Clear();
			HttpContext.Session.Remove("UserName");
			HttpContext.Session.Remove("Admin");
			return RedirectToAction("Index", "Home");

		}
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Register(TaiKhoan taiKhoan)
		{
			if (ModelState.IsValid)
			{
				var check = await db.TaiKhoans.FirstOrDefaultAsync(x => x.TenTaiKhoan == taiKhoan.TenTaiKhoan);
				if (check == null)
				{
					taiKhoan.PassWord = MaHoa.GetMd5Hash16(taiKhoan.PassWord);
					taiKhoan.IsDeleted = 0;
					taiKhoan.PhanQuyen = 1;
					taiKhoan.Anh = "https://antimatter.vn/wp-content/uploads/2022/11/anh-avatar-trang-fb-mac-dinh.jpg";
					db.TaiKhoans.Add(taiKhoan);
					await db.SaveChangesAsync();
					return RedirectToAction("LogIn");
				}
				else
				{
					ViewBag.error = "Tên tài khoản đã được sử dụng!Vui lòng nhập 1 tên khác";
					return View();
				}
			}
			return View();
		}
	}
}
