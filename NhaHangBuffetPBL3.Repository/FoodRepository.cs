using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Repository.IRepository;
using NhaHangBuffetPBL3.Models;

namespace NhaHangBuffetPBL3.Repository
{
    public class FoodRepository : Repository<Food>, IRepositoryFood
    {
        private NhaHangBuffetContext _db;
        public FoodRepository(NhaHangBuffetContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Food obj)
        {
            _db.Foods.Update(obj);
        }
    }
}
