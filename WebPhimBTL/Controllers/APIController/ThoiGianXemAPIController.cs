

using Microsoft.AspNetCore.Mvc;
using WebPhimBTL.Models;
using WebPhimBTL.Models.ModeAPI;

namespace WebPhimBTL.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThoiGianXemAPIController : ControllerBase
    {
        DbphimContext dbphimContext = new DbphimContext();
        [HttpGet()]
        public TGXem Get([FromQuery] int maPhim, int maUser)
        {
            var a = dbphimContext.ThoiLuongDaXems.Where
                (x => x.MaTapPhim == maPhim && x.MaTaiKhoan == maUser).FirstOrDefault();
            if (a == null)
            {
                return new TGXem(0);
            }
            int tg = (int)a.ThoiGianDaXem;
            return new TGXem(tg);
        }

        [HttpPost]
        public ThoiLuongDaXem Post([FromBody] ThoiLuongDaXem tg)
        {
            dbphimContext.ThoiLuongDaXems.Add(tg);
            dbphimContext.SaveChanges();
            return tg;
        }
        [HttpPut]
        public ThoiLuongDaXem Put([FromBody] ThoiLuongDaXem tg)
        {
            dbphimContext.ThoiLuongDaXems.Update(tg);
            dbphimContext.SaveChanges();
            return tg;
        }
    }
}
