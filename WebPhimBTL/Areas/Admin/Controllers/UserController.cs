using Microsoft.AspNetCore.Mvc;
using WebPhimBTL.Models;

namespace MovieWeb.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    public class UserController : Controller
    {
        DbphimContext db = new DbphimContext();
        [Route("list-user")]
        public IActionResult DanhSachUser()
        {
            var lstUser = db.TaiKhoans.Where(x => x.IsDeleted == 0 &&
            x.PhanQuyen == 1).OrderBy(x => x.TenTaiKhoan).ToList();
            return View(lstUser);
        }
        [HttpGet]
        [Route("del-user")]
        public IActionResult KhoaTK(int userCode)
        {
            TaiKhoan user = db.TaiKhoans.Find(userCode);
            var tldx = db.ThoiLuongDaXems.Where(x => x.MaTaiKhoan == userCode);
            if (tldx.Any())
            {
                db.ThoiLuongDaXems.RemoveRange(tldx);
            }
            var dgc = db.DanhGiaChungs.Where(x => x.MaTaiKhoan == userCode);
            if (dgc.Any())
            {
                db.DanhGiaChungs.RemoveRange(dgc);
            }
            var cmt = db.Comments.Where(x => x.MaTaiKhoan == userCode);
            if (cmt.Any())
            { db.Comments.RemoveRange(cmt); }
            db.TaiKhoans.Remove(user);
            db.SaveChanges();
            return RedirectToAction("DanhSachUser");
        }
    }
}
