using System;
using System.Collections.Generic;

namespace WebPhimBTL.Models;

public partial class TrangThai
{
    public int MaTrangThai { get; set; }

    public string? TenTrangThai { get; set; }

    public virtual ICollection<TPhim> TPhims { get; } = new List<TPhim>();
}
