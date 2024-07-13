using Microsoft.EntityFrameworkCore;
using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Models.Records;
using NhaHangBuffetPBL3.Repository.IRepository;

namespace NhaHangBuffetPBL3.Repository
{
    public class CartRepository : Repository<Cart>, IRepositoryCart
    {
        private NhaHangBuffetContext _db;
        public CartRepository(NhaHangBuffetContext db) : base(db)
        {
            _db = db;
        }

        public void Delete(int tableId)
        {
            var records = _db.Carts.Where(p => p.SeaatingId == tableId);

            foreach (var record in records)
            {
                _db.Carts.Remove(record);
            }

            _db.SaveChanges();
        }
        public List<Cart> GetAll(int SeatingId)
        {
            var data = _db.Carts
                            .Select(p => p)
                            .Where(p => p.SeaatingId == SeatingId)
                            .ToList();
                            
            return data;
        }
    }
}
