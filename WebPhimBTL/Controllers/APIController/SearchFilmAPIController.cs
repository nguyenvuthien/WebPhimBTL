using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebPhimBTL.Models;
namespace WebPhimBTL.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchFilmController : ControllerBase
    {
        DbphimContext db = new DbphimContext();
        [HttpGet()]
        public async Task<List<TPhim>> get(string name)
        {
            var val = new SqlParameter("@name", System.Data.SqlDbType.NVarChar);
            val.Value = name;
            var list = await db.TPhims.FromSqlRaw("exec search @name", val).ToListAsync();

            return list;
        }
    }
}
