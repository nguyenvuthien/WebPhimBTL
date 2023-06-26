using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPhimBTL.Models;
using WebPhimBTL.ViewModel;

namespace WebPhimBTL.Controllers
{
	public class FilmController : Controller
	{
		private readonly DbphimContext _dbphimContext = new DbphimContext();
		[HttpGet]
		//[Authentication("UserName")]
		public async Task<IActionResult> InfoFilm(int filmCode)
		{
			int id = 0;
			//var a = SentimentAnalyzer.Sentiments.Predict("I am extremely satisfied with this product. It works exactly as described and has exceeded my expectations. The customer service team was also very helpful and responsive when I had a question. I would definitely recommend this product to anyone in need.").Score;
			string name = "";
			string anhdaidien = "";
			if (HttpContext.Session.GetString("UserName") != null)
			{

				id = (int)HttpContext.Session.GetInt32("id");
				anhdaidien = HttpContext.Session.GetString("AnhDaiDien");
				name = HttpContext.Session.GetString("UserName");
			}

			//   int MaPhim = filmCode;
			var film1 = await _dbphimContext.TPhims.FindAsync(filmCode);
			var episodeList = await _dbphimContext.Episodes.Where(x => x.MaPhim == filmCode).OrderBy(x => x.SoTapPhim).ToListAsync();
			ViewBag.Name =
			ViewBag.Anh = anhdaidien;
			ViewBag.ID = id;
			InfoFilm film = new InfoFilm()
			{
				film = (TPhim)film1,
				episodeList = episodeList
			};
			return View(film);
		}
	}
}
