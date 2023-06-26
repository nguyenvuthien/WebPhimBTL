using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebPhimBTL.Models;
using WebPhimBTL.ViewModel;

namespace WebMovieBTL.Controllers
{
    public class HomeController : Controller
    {
        DbphimContext db = new DbphimContext();
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //var a = SentimentAnalyzer.Sentiments.Predict("I am extremely satisfied with this product. It works exactly as described and has exceeded my expectations. The customer service team was also very helpful and responsive when I had a question. I would definitely recommend this product to anyone in need.").Score;

            string name = "";
            if (HttpContext.Session.GetString("UserName") != null)
            {
                name = HttpContext.Session.GetString("UserName");
            }

            //   int MaPhim = filmCode;
            ViewBag.Name = name;
            FilmHome filmHome = new FilmHome
            {
                topPhim = await db.TPhims.FromSqlRaw("dbo.Proc_Top5MovieByView").ToListAsync(),
                phimmoi = await db.TPhims.FromSqlRaw("dbo.Proc_NewMovie").ToListAsync(),
                theloaiphim = await db.TPhims.FromSqlRaw("dbo.Proc_GetMovies").ToListAsync()
            };
            return View(filmHome);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}