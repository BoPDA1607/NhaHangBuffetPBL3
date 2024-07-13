using NhaHangBuffetPBL3.Models;

namespace NhaHangBuffetPBL3.Repository.IRepository
{
    public interface IRepositoryFoodIngredient : IRepository<FoodIngredient>
    {
        void Update(FoodIngredient obj);
        public List<FoodIngredient> GetFoodInfo(int FoodId);
    }
}
