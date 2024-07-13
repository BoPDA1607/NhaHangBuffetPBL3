using NhaHangBuffetPBL3.Models;

namespace NhaHangBuffetPBL3.Repository.IRepository
{
    public interface IRepositoryFood : IRepository<Food>
    {
        void Update(Food obj);
    }
}
