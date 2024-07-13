using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Models.Records;

namespace NhaHangBuffetPBL3.Repository.IRepository
{
    public interface IRepositoryOrders : IRepository<Orders>
    {
        void Update(Orders obj);
        Orders CheckOrder(string? code);
        void StorePreOrderCode(string code, BookingRecord model);
        void StorePreOrderCode(string code, int tableId);
        string GenerateUniqueCode();
        List<Orders> GetOrdersByTable(int SeatingId);
        Orders CheckExpire(List<Orders> obj);
    }
}
