using System;
using System.Collections.Generic;

namespace WebPhimBTL.Models;

public partial class HinhThuc
{
    public int MaHinhThuc { get; set; }

    public string? TenHinhThuc { get; set; }

    public virtual ICollection<TPhim> TPhims { get; } = new List<TPhim>();
}
