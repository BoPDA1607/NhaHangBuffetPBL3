using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Repository.IRepository;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Models.Records;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NhaHangBuffetPBL3.Repository
{
    public class MenuRepository : Repository<Menu>, IRepositoryMenu
    {
        private NhaHangBuffetContext _db;
        public MenuRepository(NhaHangBuffetContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<dynamic> GetFoodByMenu(int? SeatingId)
        {
            if(SeatingId == null) return null;
            var records = _db.Menus.Where(p => p.Order.OrderId == p.OrderId && p.Order.IsUsed == 1 && p.Order.SeatingId == SeatingId)
                         .Select(p => new
                         {
                             seatingId = p.Order.SeatingId,
                             foodName = p.Food.FoodName,
                             quantity = p.Quantity,
                             status = p.Status,
                             foodId = p.FoodId,
                             orderId = p.OrderId,
                             timeOrder = p.TimeOrder,
                             Orders = p.Order
                         });
            return records.ToList();
        }

        public void Update(Menu obj)
        {
            _db.Menus.Update(obj);
        }
    }
}
