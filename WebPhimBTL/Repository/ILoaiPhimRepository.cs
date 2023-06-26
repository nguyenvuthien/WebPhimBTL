using WebPhimBTL.Models;

namespace WebPhimBTL.Repository
{
    public interface ILoaiPhimRepository
    {
        TheLoai Add(TheLoai loaiPhim);
        TheLoai Update(TheLoai loaiPhim);
        TheLoai Delete(String maTheLoai);
        TheLoai GeTheLoai(String maTheLoai);
        IEnumerable<TheLoai> GetAllTheLoai();
    }
}
