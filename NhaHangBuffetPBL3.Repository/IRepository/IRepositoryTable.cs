using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Models.Records;

namespace NhaHangBuffetPBL3.Repository.IRepository
{
    public interface IRepositoryTable : IRepository<Table>
    {
        object Select();
        void Update(Table obj);
        Orders GetOrderIdByTable(string OrderId);
    }
}
