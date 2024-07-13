using NhaHangBuffetPBL3.Models.Records;
using NhaHangBuffetPBL3.Models;

namespace NhaHangBuffetPBL3.Repository.IRepository
{
    public interface IRepositoryBill : IRepository<Bill>
    {
        void Update(Bill obj);
        List<RevenueRecord> Sum(DateOnly date);
    }
}
