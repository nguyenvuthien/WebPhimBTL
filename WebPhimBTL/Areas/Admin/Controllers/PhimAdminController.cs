using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebPhimBTL.Models;
using WebPhimBTL.Models.Authentication;
using X.PagedList;
namespace WebPhim.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin")]
    [Route("admin/homeadmin")]
    [Authentication("Admin")]
    public class PhimAdminController : Controller
    {
        private static int maphimcmt;
        DbphimContext db = new DbphimContext();
        [Route("")]
        [Route("in")]
        public IActionResult Index()
        {
            return View();
        }
        public void anh()
        {
            ViewBag.Anh = HttpContext.Session.GetString("AnhDaiDien");
            ViewBag.Ten = HttpContext.Session.GetString("Admin");
        }
        [Route("DanhSachPhim")]
        public IActionResult DanhSachPhim(int? page)
        {
            anh();
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstphim = db.TPhims.AsNoTracking().OrderBy(x => x.TenPhim);
            PagedList<TPhim> lst = new PagedList<TPhim>(lstphim, pageNumber, pageSize);
            return View(lst);
        }
        [Route("DanhSachTheLoai")]
        public IActionResult DanhSachTheLoai(int matheloai, int? page)
        {
            anh();
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            List<TPhim> lstphim = db.TPhims.Where(x => x.MaTheLoai == matheloai).OrderBy(x => x.TenPhim).ToList();
            PagedList<TPhim> lst = new PagedList<TPhim>(lstphim, pageNumber, pageSize);
            ViewBag.matheloai = matheloai;
            return View(lst);
        }
        [Route("MenuCacPhim")]
        public IActionResult MenuCacPhim(int maphim, int? page)
        {
            anh();
            maphimcmt = maphim;
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            List<Comment> lstcmt = db.Comments.Where(x => x.MaPhim == maphim).OrderBy(x => x.MaComment).ToList();
            PagedList<Comment> lst = new PagedList<Comment>(lstcmt, pageNumber, pageSize);
            ViewBag.maphim = maphim;
            return View(lst);
        }
        [Route("LocCommentXau")]
        public IActionResult LocCommentXau(int? page)
        {
            anh();
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            List<Comment> lstcmt = db.Comments.Where(x => x.MaPhim == maphimcmt).OrderBy(x => x.MaComment).ToList();
            string[] subArr = { "DMM" };
            List<Comment> filteredList = new List<Comment>(); ;
            foreach (string sub in subArr)
            {
                filteredList = filteredList.Concat(lstcmt.FindAll(s => s.NoiDung.IndexOf(sub, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
            }
            List<Comment> filteredList1 = filteredList.Distinct().ToList();
            PagedList<Comment> lst = new PagedList<Comment>(filteredList1, pageNumber, pageSize);
            return View(lst);
        }
        [Route("LocCommentTot")]
        public IActionResult LocCommentTot(int? page)
        {
            anh();
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            List<Comment> lstcmt = db.Comments.Where(x => x.MaPhim == maphimcmt).OrderBy(x => x.MaComment).ToList();
            string[] totArr = { "tuyệt" };
            string[] xauArr = { "DMM" };
            List<Comment> filteredList = new List<Comment>(); ;
            foreach (string sub in totArr)
            {
                filteredList = filteredList.Concat(lstcmt.FindAll(s => s.NoiDung.IndexOf(sub, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
            }
            foreach (string sub in xauArr)
            {
                filteredList = filteredList.Except(lstcmt.FindAll(s => s.NoiDung.IndexOf(sub, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
            }
            List<Comment> filteredList1 = filteredList.Distinct().ToList();
            PagedList<Comment> lst = new PagedList<Comment>(filteredList1, pageNumber, pageSize);
            return View(lst);
        }
        [Route("XoaCommentXau")]
        [HttpGet]
        public IActionResult XoaCommentXau(int maComment, int mataikhoan)
        {
            TempData["Message"] = "";
            db.TaiKhoans.Where(x => x.MaTaiKhoan == mataikhoan).ToList().ForEach(x => x.IsDeleted = 0);
            db.SaveChanges();
            db.Remove(db.Comments.Find(maComment));
            db.SaveChanges();
            TempData["Message"] = "Bình luận đã được xóa";
            return RedirectToAction("LocCommentXau", "PhimAdmin");
        }
        [Route("XoaCommentTot")]
        [HttpGet]
        public IActionResult XoaCommentTot(int maComment)
        {
            TempData["Message"] = "";
            db.Remove(db.Comments.Find(maComment));
            db.SaveChanges();
            TempData["Message"] = "Bình luận đã được xóa";
            return RedirectToAction("LocCommentTot", "PhimAdmin");
        }
        [Route("ThemPhimMoi")]
        [HttpGet]
        public IActionResult ThemPhimMoi()
        {
            anh();
            ViewBag.MaDaoDien = new SelectList(db.DaoDiens.ToList(), "MaDaoDien", "TenDaoDien");
            ViewBag.MaHinhThuc = new SelectList(db.HinhThucs.ToList(), "MaHinhThuc", "TenHinhThuc");
            ViewBag.MaTrangThai = new SelectList(db.TrangThais.ToList(), "MaTrangThai", "TenTrangThai");
            ViewBag.MaTheLoai = new SelectList(db.TheLoais.ToList(), "MaTheLoai", "TenTheLoai");
            ViewBag.MaTrailer = new SelectList(db.Trailers.ToList(), "MaTrailer", "Link");
            return View();
        }
        [Route("ThemPhimMoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ThemPhimMoi(TPhim phim, IFormFile uploadhinh)
        {

            if (ModelState.IsValid)
            {
                db.TPhims.Add(phim);
                db.SaveChanges();
                if (uploadhinh != null && uploadhinh.Length > 0)
                {
                    int maphim = int.Parse(db.TPhims.ToList().Last().MaPhim.ToString());
                    string fileName = "";
                    fileName = maphim.ToString() + ".jpg";
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadAnh", "Anh", fileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadhinh.CopyToAsync(fileStream);
                    }
                    TPhim uphim = db.TPhims.FirstOrDefault(x => x.MaPhim == maphim);
                    uphim.Anh = fileName;
                    db.SaveChanges();
                }
                return RedirectToAction("DanhSachPhim");
            }

            return View(phim);
        }
        [Route("SuaPhim")]
        [HttpGet]
        public IActionResult SuaPhim(int maPhim)
        {
            anh();
            ViewBag.MaDaoDien = new SelectList(db.DaoDiens.ToList(), "MaDaoDien", "TenDaoDien");
            ViewBag.MaHinhThuc = new SelectList(db.HinhThucs.ToList(), "MaHinhThuc", "TenHinhThuc");
            ViewBag.MaTrangThai = new SelectList(db.TrangThais.ToList(), "MaTrangThai", "TenTrangThai");
            ViewBag.MaTheLoai = new SelectList(db.TheLoais.ToList(), "MaTheLoai", "TenTheLoai");
            ViewBag.MaTrailer = new SelectList(db.Trailers.ToList(), "MaTrailer", "Link");
            var phim = db.TPhims.Find(maPhim);
            return View(phim);
        }
        [Route("SuaPhim")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SuaPhim(TPhim phim, IFormFile uploadhinh)
        {
            //int maphim = phim.MaPhim;
            //string fileName = "";
            //fileName = maphim.ToString() + ".jpg";
            //TPhim uphim = db.TPhims.FirstOrDefault(x => x.MaPhim == maphim);
            //uphim.Anh = fileName;
            //db.SaveChanges();
            //if (ModelState.IsValid)
            //{
            db.Entry(phim).State = EntityState.Modified;
            db.SaveChanges();
            if (uploadhinh != null && uploadhinh.Length > 0)
            {
                int maphim = phim.MaPhim;
                string fileName = "";
                fileName = maphim.ToString() + ".jpg";
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "UploadAnh", "Anh", fileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await uploadhinh.CopyToAsync(fileStream);
                }
                TPhim uphim = db.TPhims.FirstOrDefault(x => x.MaPhim == maphim);
                uphim.Anh = fileName;
                db.SaveChanges();
            }
            else
            {
                int maphim = phim.MaPhim;
                string fileName = "";
                fileName = maphim.ToString() + ".jpg";
                TPhim uphim = db.TPhims.FirstOrDefault(x => x.MaPhim == maphim);
                uphim.Anh = fileName;
                db.SaveChanges();
            }
            return RedirectToAction("DanhSachPhim", "PhimAdmin");
            //}
            //return View(phim);
        }

        [Route("XoaPhim")]
        [HttpGet]
        public IActionResult XoaPhim(int maPhim)
        {
            TempData["Message"] = "";
            var episodes = db.Episodes.Where(x => x.MaPhim == maPhim);
            int matapphim = db.Episodes.Where(x => x.MaPhim == maPhim).FirstOrDefault()?.MaTapPhim ?? 0;
            var thoiluongdaxem = db.ThoiLuongDaXems.Where(x => x.MaTapPhim == matapphim);
            if (episodes.Any() && matapphim != 0)
            {
                db.Episodes.RemoveRange(episodes);
                db.ThoiLuongDaXems.RemoveRange(thoiluongdaxem);
                db.SaveChanges();
            }
            else if (episodes.Any())
            {
                db.Episodes.RemoveRange(episodes);
                db.SaveChanges();
            }
            var comments = db.Comments.Where(x => x.MaPhim == maPhim);
            if (comments.Any())
            {
                db.Comments.RemoveRange(comments);
                db.SaveChanges();
            }
            var danhgiachungs = db.DanhGiaChungs.Where(x => x.MaPhim == maPhim);
            if (danhgiachungs.Any())
            {
                db.DanhGiaChungs.RemoveRange(danhgiachungs);
                db.SaveChanges();
            }
            db.Remove(db.TPhims.Find(maPhim));
            db.SaveChanges();
            TempData["Message"] = "Phim đã được xóa";
            return RedirectToAction("DanhSachPhim", "PhimAdmin");

        }

        [Route("DanhSachTrailer")]
        public IActionResult DanhSachTrailer(int? page)
        {
            anh();
            int pageSize = 12;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lsttrailer = db.Trailers.AsNoTracking().OrderBy(x => x.MaTrailer);
            PagedList<Trailer> lst = new PagedList<Trailer>(lsttrailer, pageNumber, pageSize);
            return View(lst);
        }
        [Route("ThemTrailer")]
        [HttpGet]
        public IActionResult ThemTrailer()
        {
            anh();
            return View();
        }
        [Route("ThemTrailer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTrailer(Trailer tl)
        {
            if (ModelState.IsValid)
            {
                db.Trailers.Add(tl);
                db.SaveChanges();
                return RedirectToAction("DanhSachTrailer");
            }
            return View(tl);
        }
        [Route("SuaTrailer")]
        [HttpGet]
        public IActionResult SuaTrailer(int maTrailer)
        {
            anh();
            var tl = db.Trailers.Find(maTrailer);
            return View(tl);
        }
        [Route("SuaTrailer")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTrailer(Trailer tl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("DanhSachTrailer", "PhimAdmin");
            }
            return View(tl);
        }
        [Route("XoaTrailer")]
        [HttpGet]
        public IActionResult XoaTrailer(int maTrailer)
        {
            TempData["Message"] = "";
            db.Trailers.Remove(db.Trailers.Find(maTrailer));
            db.SaveChanges();
            TempData["Message"] = "Trailer đã được xóa";
            return RedirectToAction("DanhSachTrailer", "PhimAdmin");

        }

        // tung
        [Route("danhsachtapphim")]
        public IActionResult DanhSachTapPhim(int? page, int maphim)
        {
            anh();
            int pageSize = 10;
            ViewBag.MaPhim = maphim;
            string tenphim = db.TPhims.Find(maphim).TenPhim;
            ViewBag.TenPhim = tenphim;
            int pageNumber = page == null || page < 0 ? 1 : page.Value;
            var lstTapPhim = db.Episodes.Where(x => x.MaPhim == maphim).OrderBy(x => x.SoTapPhim);
            PagedList<Episode> lst = new PagedList<Episode>(lstTapPhim, pageNumber, pageSize);
            return View(lst);
        }

        [Route("themtapphimmoi")]
        [HttpGet]
        public IActionResult ThemTapPhimMoi(int maphim)
        {
            anh();
            ViewBag.MaPhim = maphim;
            return View();
        }
        [Route("themtapphimmoi")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ThemTapPhimMoi(Episode episode)
        {
            if (ModelState.IsValid)
            {
                db.Episodes.Add(episode);
                db.SaveChanges();
                return RedirectToAction("DanhSachTapPhim", new { maphim = episode.MaPhim });
            }
            return View(episode);
        }

        [Route("suatapphim")]
        [HttpGet]
        public IActionResult SuaTapPhim(int maTapPhim)
        {
            anh();
            var episode = db.Episodes.Find(maTapPhim);
            ViewBag.MaPhim = episode.MaPhim;

            return View(episode);
        }
        [Route("suatapphim")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SuaTapPhim(Episode episode)
        {
            if (ModelState.IsValid)
            {
                db.Update(episode);
                db.SaveChanges();
                return RedirectToAction("DanhSachTapPhim", "PhimAdmin", new { maphim = episode.MaPhim });
            }
            return View(episode);
        }

        [Route("xoatapphim")]
        [HttpGet]
        public IActionResult XoaTapPhim(int maTapPhim)
        {
            TempData["Message"] = "";
            var episode = db.Episodes.Find(maTapPhim);
            var thoiLuongXemPhim = db.ThoiLuongDaXems.Where(x => x.MaTapPhim == maTapPhim).ToList();
            if (thoiLuongXemPhim != null)
            {
                db.ThoiLuongDaXems.RemoveRange(thoiLuongXemPhim);
            }
            db.Remove(episode);
            db.SaveChanges();
            TempData["Message"] = "Xóa thành công";
            return RedirectToAction("DanhSachTapPhim", "PhimAdmin", new { maphim = episode.MaPhim });
        }
    }
}
