using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Models.Records;

namespace NhaHangBuffetPBL3.Repository.IRepository
{
    public interface IRepositoryCart :IRepository<Cart>
    {
        void Delete(int tableId);
        List<Cart> GetAll(int SeatingId);
    }
}
