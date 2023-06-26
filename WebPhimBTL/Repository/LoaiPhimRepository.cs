using WebPhimBTL.Models;
namespace WebPhimBTL.Repository
{
    public class LoaiPhimRepository : ILoaiPhimRepository
    {
        private readonly DbphimContext _context;
        public LoaiPhimRepository(DbphimContext context)
        {
            _context = context;
        }
        public TheLoai Add(TheLoai loaiPhim)
        {
            _context.TheLoais.Add(loaiPhim);
            _context.SaveChanges();
            return loaiPhim;
        }

        public TheLoai Update(TheLoai loaiPhim)
        {
            _context.TheLoais.Update(loaiPhim);
            _context.SaveChanges();
            return loaiPhim;
        }

        public TheLoai Delete(string maThePhim)
        {
            throw new NotImplementedException();
        }

        public TheLoai GetTheLoai(string maThePhim)
        {
            return _context.TheLoais.Find(maThePhim);
        }

        public IEnumerable<TheLoai> GetAllTheLoai()
        {
            return _context.TheLoais;
        }

        public TheLoai GeTheLoai(string maThePhim)
        {
            return _context.TheLoais.Find(maThePhim);
        }
    }
}
