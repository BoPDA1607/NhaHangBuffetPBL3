using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Repository.IRepository;
using NhaHangBuffetPBL3.Models.Records;

namespace NhaHangBuffetPBL3.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private NhaHangBuffetContext _db;
        public IRepositoryTable Table { get; private set; }

        public IRepositoryMenu Menu { get; private set; }

        public IRepositoryFood Food { get; private set; }

        public IRepositoryIngredient Ingredient { get; private set; }

        public IRepositoryFoodIngredient FoodIngredient { get; private set; }

        public IRepositoryStaff Staff { get; private set; }
        public IRepositoryOrders Orders { get; private set; }
        public IRepositoryCart Cart { get; private set; }
        public IRepositoryBill Bill { get; private set; }

        public UnitOfWork(NhaHangBuffetContext db)
        {
            _db = db;
            Table = new TableRepository(_db);
            Menu = new MenuRepository(_db);
            Food = new FoodRepository(_db);
            Ingredient = new IngredientRepository(_db);
            FoodIngredient = new FoodIngredientRepository(_db);
            Staff = new StaffRepository(_db);
            Orders = new OrdersRepository(_db);
            Cart = new CartRepository(_db);
            Bill = new BillRepository(_db);
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
