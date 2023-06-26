using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebPhimBTL.Models;
using WebPhimBTL.Models.ModeAPI;

namespace WebPhimBTL.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhimController : ControllerBase
    {
        DbphimContext db = new DbphimContext();
        [HttpGet("{matheloai}")]
        public async Task<IEnumerable<Movie>> GetMovieByCategory(int matheloai)
        {
            var phim = await (from p in db.TPhims
                              where p.MaTheLoai == matheloai
                              select new Movie
                              {
                                  MaPhim = p.MaPhim,
                                  TenPhim = p.TenPhim,
                                  MaTheLoai = p.MaTheLoai,
                                  Anh = p.Anh,
                                  NgayKhoiChieu = p.NgayKhoiChieu,
                                  MoTa = p.MoTa,
                              }).ToListAsync();
            return phim;
        }
        [Route("search")]
        [HttpGet]
        public async Task<IEnumerable<Movie>> GetMovieBySearch(string data)
        {
            var viewbooks = new List<Movie>();
            string connectionString = "Data Source=PC\\ManhQuang;Initial Catalog=DBPhimUpdate;Integrated Security=True; TrustServerCertificate=True";
            //Khoi tao doi tuong
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            //Doi tuong sql command
            SqlCommand sqlCommand = sqlConnection.CreateCommand();
            sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
            //Khai bao cau truy van
            sqlCommand.CommandText = "dbo.Proc_Search";
            sqlCommand.Parameters.Add("@data", System.Data.SqlDbType.NVarChar).Value = data;
            //Mo ket noi
            sqlConnection.Open();
            //Thuc thi cong viec voi database
            SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
            //Xu ly du lieu tra ve
            while (sqlDataReader.Read())
            {
                var movie = new Movie();
                for (int i = 0; i < sqlDataReader.FieldCount; i++)
                {
                    //Lay ten cot du lieu dang doc
                    var colName = sqlDataReader.GetName(i);
                    //Lay gia tri o du lieu dang doc
                    var value = sqlDataReader.GetValue(i);
                    //Lay property theo ten cot 
                    var property = movie.GetType().GetProperty(colName);

                    if (property != null && value != DBNull.Value)
                    {
                        property.SetValue(movie, value);
                    }
                }

                viewbooks.Add(movie);
            }
            //Dong ket noi
            sqlConnection.Close();
            return viewbooks;
        }
    }
}
