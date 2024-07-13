using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Models.Records;

namespace NhaHangBuffetPBL3.Repository.IRepository
{
    public interface IRepositoryMenu : IRepository<Menu>
    {
        void Update(Menu obj);
        IEnumerable<dynamic> GetFoodByMenu(int? SeatingId);
    }
}
