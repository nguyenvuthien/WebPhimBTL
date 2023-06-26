using Microsoft.AspNetCore.Mvc;
using WebPhimBTL.Repository;

namespace WebPhimBTL.ViewComponents
{
	public class TheLoaiMenuViewComponent : ViewComponent
	{
		private readonly ITheLoaiRespository _theLoai;
		public TheLoaiMenuViewComponent(ITheLoaiRespository theLoaiRespository)
		{
			_theLoai = theLoaiRespository;
		}
		public IViewComponentResult Invoke()
		{
			var theLoai = _theLoai.GetAllTheLoai().OrderBy(x => x.TenTheLoai);
			return View(theLoai);
		}
	}
}
