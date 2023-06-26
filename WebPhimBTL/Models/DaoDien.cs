using System;
using System.Collections.Generic;

namespace WebPhimBTL.Models;

public partial class DaoDien
{
    public int MaDaoDien { get; set; }

    public string? TenDaoDien { get; set; }

    public string? ThongTin { get; set; }

    public virtual ICollection<TPhim> TPhims { get; } = new List<TPhim>();
}
