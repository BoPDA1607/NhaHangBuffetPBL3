using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Repository.IRepository;
using System.Linq.Expressions;
using NhaHangBuffetPBL3.Models;

namespace NhaHangBuffetPBL3.Repository
{
    public class FoodIngredientRepository : Repository<FoodIngredient>, IRepositoryFoodIngredient
    {
        private NhaHangBuffetContext _db;
        public FoodIngredientRepository(NhaHangBuffetContext db) : base(db)
        {
            _db = db;
        }

        public void Update(FoodIngredient obj)
        {
            _db.FoodIngredients.Update(obj);
        }
        public List<FoodIngredient> GetFoodInfo(int FoodId)
        {
            var records = _db.FoodIngredients.Where(p => p.FoodId == FoodId)
                         .Select(p => new FoodIngredient
                         {
                             FoodId = p.FoodId,
                             Use_quantity = p.Use_quantity,
                             Ingredient_id = p.Ingredient_id,
                             Unit = p.Unit,
                             Food = p.Food,
                             Ingredient = p.Ingredient
                         });
            return records.ToList();
        }
    }
}
