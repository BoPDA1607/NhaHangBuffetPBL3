using Microsoft.AspNetCore.Mvc;
using NhaHangBuffetPBL3.Repository.IRepository;


namespace NhaHangBuffetPBL3.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class MenuController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Menus
        public IActionResult Index()
        {
            ViewBag.MenuName = (_unitOfWork.Food.GetAll()).Select(menu => menu.Type).Distinct().ToList();
            ViewBag.Food = _unitOfWork.Food.GetAll();
            return View();
        }
    }
}
