using System;
using System.Collections.Generic;

namespace WebPhimBTL.Models;

public partial class DanhGiaChung
{
    public int MaTaiKhoan { get; set; }

    public int MaPhim { get; set; }

    public int? DanhGia { get; set; }

    public virtual TPhim MaPhimNavigation { get; set; } = null!;

    public virtual TaiKhoan MaTaiKhoanNavigation { get; set; } = null!;
}
