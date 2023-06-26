using System.ComponentModel.DataAnnotations;

namespace WebPhimBTL.Models;

public partial class TPhim
{
    public int MaPhim { get; set; }

    public string? TenPhim { get; set; }

    public int? MaTheLoai { get; set; }

    public int? MaDaoDien { get; set; }
    public int? PhuDe { get; set; }

    public string? Anh { get; set; }

    public DateTime? NgayKhoiChieu { get; set; }

    public int? MaTrangThai { get; set; }

    public string? MoTa { get; set; }
    [RegularExpression("^[a-zA-Z]*$", ErrorMessage = "e002")]
    public string? QuocGia { get; set; }

    public int? MaHinhThuc { get; set; }
    [Range(1, 120)]
    public int? GioiHanDoTuoi { get; set; }

    public int? MaTrailer { get; set; }

    public virtual ICollection<Comment> Comments { get; } = new List<Comment>();

    public virtual ICollection<DanhGiaChung> DanhGiaChungs { get; } = new List<DanhGiaChung>();

    public virtual ICollection<Episode> Episodes { get; } = new List<Episode>();

    public virtual DaoDien? MaDaoDienNavigation { get; set; }

    public virtual HinhThuc? MaHinhThucNavigation { get; set; }

    public virtual TheLoai? MaTheLoaiNavigation { get; set; }

    public virtual Trailer? MaTrailerNavigation { get; set; }

    public virtual TrangThai? MaTrangThaiNavigation { get; set; }
}
