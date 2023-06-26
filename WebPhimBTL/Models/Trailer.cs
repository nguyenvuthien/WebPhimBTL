using System;
using System.Collections.Generic;

namespace WebPhimBTL.Models;

public partial class Trailer
{
    public int MaTrailer { get; set; }

    public string? Link { get; set; }

    public virtual ICollection<TPhim> TPhims { get; } = new List<TPhim>();
}
