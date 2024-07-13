using NhaHangBuffetPBL3.Models;

namespace NhaHangBuffetPBL3.Repository.IRepository
{
    public interface IRepositoryStaff : IRepository<Staff>
    {
        void Update(Staff obj);
    }
}
