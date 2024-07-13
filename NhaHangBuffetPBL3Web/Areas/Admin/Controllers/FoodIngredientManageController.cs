using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Repository;
using NhaHangBuffetPBL3.Repository.IRepository;

namespace NhaHangBuffetPBL3.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin", Policy = "AdminPolicy")]
    public class FoodIngredientManageController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public FoodIngredientManageController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Admin/FoodIngredientManage
        public IActionResult Index(int FoodId)
        {
            var records= _unitOfWork.FoodIngredient.GetFoodInfo(FoodId);
            ViewData["FoodInfo"]= records;
            var food = _unitOfWork.Food.GetFirstOrDefault(p => p.FoodId == FoodId);
            ViewData["FoodName"] = food.FoodName;
            ViewData["FoodId"] = FoodId;
            return View(_unitOfWork.Ingredient.GetAll());
        }
        public IActionResult Update(List<FoodIngredient> ingredients)
        {
            int FoodId = ingredients[0].FoodId;
            foreach (var item in ingredients)
            {
                try
                {
                    if(!_unitOfWork.FoodIngredient.Any(p => p.FoodId == FoodId && p.Ingredient_id == item.Ingredient_id))
                    _unitOfWork.FoodIngredient.Add(item);
                    else _unitOfWork.FoodIngredient.Update(item);
                }
                catch (Exception)
                {
                    TempData["error"] = $"Nguyên liệu {_unitOfWork.Ingredient.GetFirstOrDefault(p => p.Ingredient_id == item.Ingredient_id).Ingredient_name} đã tồn tại";
                    return RedirectToAction("Index", new { FoodId = FoodId });
                }                  
            }
            _unitOfWork.Save();
            return RedirectToAction("Index", new { FoodId = FoodId });
        }
        public IActionResult Delete(int FoodId,string ingredient_id)
        {
            var data = _unitOfWork.FoodIngredient.GetFirstOrDefault(p=>p.FoodId== FoodId&&p.Ingredient_id==ingredient_id);
            _unitOfWork.FoodIngredient.Remove(data);
            _unitOfWork.Save();
            return RedirectToAction("Index", new { FoodId = FoodId });
        }     
    }
}
