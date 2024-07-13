using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Repository.IRepository;
using NhaHangBuffetPBL3.Models;

namespace NhaHangBuffetPBL3.Repository
{
    public class StaffRepository : Repository<Staff>, IRepositoryStaff
    {
        private NhaHangBuffetContext _db;
        public StaffRepository(NhaHangBuffetContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Staff obj)
        {
            _db.Staff.Update(obj);
        }
    }
}
