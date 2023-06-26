using Microsoft.AspNetCore.Mvc;
using WebPhimBTL.Repository;


namespace WebPhimBTL.ViewComponents
{
    public class LoaiPhimMenuViewComponent : ViewComponent
    {
        private readonly ILoaiPhimRepository _loaiPhim;
        public LoaiPhimMenuViewComponent(ILoaiPhimRepository loaiPhimRepository)
        {
            _loaiPhim = loaiPhimRepository;
        }
        public IViewComponentResult Invoke()
        {
            var loaiPhim = _loaiPhim.GetAllTheLoai().OrderBy(x => x.TenTheLoai);
            return View(loaiPhim);
        }
    }
}
