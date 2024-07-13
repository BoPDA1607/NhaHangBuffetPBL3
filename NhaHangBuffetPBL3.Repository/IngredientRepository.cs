using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Repository.IRepository;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Models.Records;
using Microsoft.EntityFrameworkCore;

namespace NhaHangBuffetPBL3.Repository
{
    public class IngredientRepository : Repository<Ingredient>, IRepositoryIngredient
    {
        private NhaHangBuffetContext _db;
        public IngredientRepository(NhaHangBuffetContext db) : base(db)
        {
            _db = db;
        }

        public IEnumerable<Ingredient> Get(int IdMon)
        {
            var data = _db.Ingredients
                                    .Select(p=>p)
                                    .ToList() // Materialize the collection
                                    .Where(p => _db.FoodIngredients.Any(nlma => nlma.FoodId == IdMon));
            return data;
        }

        public bool CheckIngredients(int FoodId, string method, int quantity)
        {
            var records = _db.FoodIngredients.Where(nlma => nlma.FoodId == FoodId)
                                             .Select(p => new Ingredient
                                             {
                                                 Ingredient_id = p.Ingredient_id,
                                                 Ingredient_name = p.Ingredient.Ingredient_name,
                                                 Stored_quantity = (method == "add") ? p.Ingredient.Stored_quantity - p.Use_quantity * quantity : p.Ingredient.Stored_quantity + p.Use_quantity * quantity,
                                                 Order_quantity = p.Ingredient.Order_quantity,
                                                 Unit = p.Ingredient.Unit
                                             }).ToList();
            if (method == "add")
            {
                foreach (var nl in records)
                {
                    if (nl.Stored_quantity < 0)
                    {
                        return false;
                    }
                }
            }
            _db.UpdateRange(records);
            _db.SaveChanges();
            return true;
        }
        public void Update(Ingredient obj)
        {
            _db.Ingredients.Update(obj);
        }
        public override string ToString()
        {
            return _db.Ingredients.Select(p => p.Ingredient_name).ToString();
        }
    }
}
