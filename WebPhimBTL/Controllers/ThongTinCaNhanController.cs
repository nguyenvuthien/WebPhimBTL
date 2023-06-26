using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPhimBTL.Models;
using WebPhimBTL.Models.Authentication;

namespace WebPhimBTL.Controllers
{
    public class ThongTinCaNhanController : Controller
    {
        DbphimContext db = new DbphimContext();
        [Authentication("UserName")]
        [Route("thongtintaikhoan")]
        public async Task<IActionResult> ThongTinTaiKhoan()
        {
            int id = (int)HttpContext.Session.GetInt32("id");
            string anhdaidien = HttpContext.Session.GetString("AnhDaiDien");
            string ten = HttpContext.Session.GetString("UserName");

            ViewBag.Anh = anhdaidien;
            ViewBag.ID = id; ViewBag.Ten = ten;
            var taiKhoan = await db.TaiKhoans.FirstOrDefaultAsync(x => x.TenTaiKhoan == ten);
            return View(taiKhoan);
        }

        [Route("SuaThongTin")]
        [HttpGet]
        public async Task<IActionResult> SuaThongTin()
        {
            string ten = HttpContext.Session.GetString("UserName");
            var taiKhoan = await db.TaiKhoans.Where(x => x.TenTaiKhoan == ten).FirstOrDefaultAsync();
            //var taiKhoan = db.TaiKhoans.Find(tenTaiKhoan);
            return View(taiKhoan);
        }
        [Route("SuaThongTin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaThongTin(TaiKhoan taiKhoan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(taiKhoan).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChangesAsync();
                return RedirectToAction("ThongTinTaiKhoan", "ThongTinCaNhan");
                //"ThongTinTaiKhoan", "Home", taiKhoan.TenTaiKhoan
            }
            return View(taiKhoan);
        }
    }
}
