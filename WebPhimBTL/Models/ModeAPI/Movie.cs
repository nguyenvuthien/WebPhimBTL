namespace WebPhimBTL.Models.ModeAPI
{
	public class Movie
	{
		public int MaPhim { get; set; }

		public string? TenPhim { get; set; }

		public int? MaTheLoai { get; set; }
		public string? Anh { get; set; }

		public DateTime? NgayKhoiChieu { get; set; }
		public string? MoTa { get; set; }
	}
}
