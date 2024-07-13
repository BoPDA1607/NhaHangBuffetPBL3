using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Models.Records;
using NhaHangBuffetPBL3.Repository.IRepository;

namespace NhaHangBuffetPBL3.Repository
{
    public class OrdersRepository : Repository<Orders>, IRepositoryOrders
    {
        private NhaHangBuffetContext _db;
        public OrdersRepository(NhaHangBuffetContext db) : base(db)
        {
            _db = db;
        }

        public Orders CheckExpire(List<Orders> obj)
        {
            Orders order = new Orders();
            foreach (var item in obj)
            {
                if (item.IsUsed == 0 && DateTime.Now.Subtract((DateTime)item.SeatingDate).TotalSeconds > 3600)
                {
                    item.IsUsed = -1;
                }
                else if(item.IsUsed == 1)
                {
                    order = item;
                }
            }
            _db.Orders.UpdateRange(obj);
            _db.SaveChanges();
            return order;
        }

        public Orders CheckOrder(string? code)
        {
            var data= _db.Orders.FirstOrDefault(t => t.OrderId == code);
            if(data.IsUsed== 0) data.IsUsed = 1;
            Update(data);
            _db.SaveChanges();
            return data;
        }
        public void StorePreOrderCode(string code, BookingRecord model)
        {
            var preOrderCode = new Orders
            {
                OrderId = code,
                SeatingId = model.TableId,
                IsUsed = 0,
                SeatingDate = model.SeatingDate
            };
            _db.Orders.Add(preOrderCode);
        }
        public void StorePreOrderCode(string code, int tableId)
        {
            var preOrderCode = new Orders
            {
                OrderId = code,
                SeatingId = tableId,
                IsUsed = 1,
                SeatingDate = DateTime.Now
            };

            _db.Orders.Add(preOrderCode);
        }
        public string GenerateUniqueCode()
        {
            string code;
            do
            {
                code = Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
            }
            while (_db.Orders.Any(p => p.OrderId == code)); // Check if the code exists in the database

            return code;
        }

        public List<Orders> GetOrdersByTable(int SeatingId)
        {
            return _db.Orders.Where(t => t.SeatingId == SeatingId).ToList();
        }

        public void Update(Orders obj)
        {
            _db.Orders.Update(obj);
        }
    }
}
