namespace NhaHangBuffetPBL3.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IRepositoryTable Table { get; }
        IRepositoryMenu Menu { get; }
        IRepositoryFood Food { get; }
        IRepositoryIngredient Ingredient { get; }
        IRepositoryFoodIngredient FoodIngredient { get; }
        IRepositoryStaff Staff { get; }
        IRepositoryOrders Orders { get; }
        IRepositoryCart Cart { get; }
        IRepositoryBill Bill { get; }
        void Save();
    }
}
