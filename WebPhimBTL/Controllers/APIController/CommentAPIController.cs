using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;
using System.Data;
using WebPhimBTL.Models;
using WebPhimBTL.Models.ModeAPI;
using WebPhimBTL.Type;

namespace WebPhimBTL.Controllers.APIController
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CommentAPIController : ControllerBase
    {
        DbphimContext dbphimContext = new DbphimContext();
        [HttpGet()]
        public async Task<IEnumerable<CommentAPI>> Get([FromQuery] int maphim)
        {
            var listComment = await
               (from cmt in dbphimContext.Comments
                join TaiKhoan in dbphimContext.TaiKhoans
                on cmt.MaTaiKhoan equals TaiKhoan.MaTaiKhoan
                where cmt.MaPhim == maphim
                select new CommentAPI(cmt.MaComment, TaiKhoan.MaTaiKhoan, TaiKhoan.TenTaiKhoan, TaiKhoan.Anh, cmt.NoiDung, cmt.ThoiGianCmt)).ToListAsync();

            return listComment;
        }
        [HttpPost]
        public async Task<CommentType> Post([FromBody] CommentType cmt)
        {

            DataTable table = new DataTable();
            table.Columns.Add("MaPhim", typeof(int));
            table.Columns.Add("MaTaiKhoan", typeof(int));
            table.Columns.Add("NoiDung", typeof(string));
            table.Columns.Add("ThoiGianCmt", typeof(DateTime));


            DataRow row1 = table.NewRow();

            row1["MaPhim"] = cmt.MaPhim;
            row1["MaTaiKhoan"] = cmt.MaTaiKhoan;
            row1["NoiDung"] = cmt.NoiDung;
            row1["ThoiGianCmt"] = cmt.ThoiGianCmt;

            table.Rows.Add(row1);

            SqlParameter param = new SqlParameter("@udtCommentType", SqlDbType.Structured);
            param.Value = table;
            param.TypeName = "udtCommentType";
            dbphimContext.Database.ExecuteSqlRaw("EXEC insertComment @udtCommentType", param);
            return cmt;
        }
        [HttpPut]
        public async Task<string> Put([FromBody] Comment cmt)
        {
            dbphimContext.Comments.Update(cmt);
            dbphimContext.SaveChangesAsync();
            return cmt.MaComment.ToJson();
        }
        [HttpDelete]
        public async Task<string> Delete([FromQuery] int maCmt)
        {
            Comment comment = await dbphimContext.Comments.FindAsync(maCmt);
            dbphimContext.Comments.Remove(comment);
            dbphimContext.SaveChangesAsync();
            return maCmt.ToJson();
        }
    }
}
