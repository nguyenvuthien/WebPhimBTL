using WebPhimBTL.Models;

namespace WebPhimBTL.Repository
{
	public class TheLoaiRespository : ITheLoaiRespository
	{
		private readonly DbphimContext _context;
		public TheLoaiRespository(DbphimContext context)
		{
			_context = context;
		}
		public TheLoai Add(TheLoai theLoai)
		{
			_context.TheLoais.Add(theLoai);
			_context.SaveChanges();
			return theLoai;

		}

		public TheLoai Delete(string maTheLoai)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<TheLoai> GetAllTheLoai()
		{
			return _context.TheLoais;
		}

		public TheLoai GetLoaiSp(string maTheLoai)
		{
			return _context.TheLoais.Find(maTheLoai);
		}

		public TheLoai Update(TheLoai theLoai)
		{
			_context.Update(theLoai);
			_context.SaveChanges();
			return theLoai;
		}
	}
}
