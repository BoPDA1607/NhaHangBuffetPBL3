using Microsoft.AspNetCore.Mvc;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Repository.IRepository;


namespace NhaHangBuffetPBL3.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class TableController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public TableController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index(int? SeatingId, string? orderId, Orders model = null)
        {
            double ketthuc = 3600.0;
            ViewBag.MenuName = (_unitOfWork.Food.GetAll()).Select(menu => menu.Type).Distinct().ToList();
            ViewBag.Food = _unitOfWork.Food.GetAll();
            ViewBag.SeatingId = SeatingId;
            ViewBag.orderId = orderId;
            ViewData["isUsed"] = 2;
            ViewData["Orders"] = _unitOfWork.Orders.GetAll();
            if (model.IsUsed != null && model.SeatingDate != null && model.SeatingId != null && model.OrderId != null) ViewData["timeNow"] = DateTime.Now.Subtract((DateTime)model.SeatingDate).TotalSeconds;

            var CartItems = _unitOfWork.Cart.GetAll((int)SeatingId);
            bool HasItemInCart = false;
            if (CartItems.Count == 0)
            {
                HasItemInCart = false;
            }
            else if (CartItems.Count != 0)
            {
                HasItemInCart = true;

            }
            ViewBag.History = _unitOfWork.Menu.GetFoodByMenu(SeatingId);
            ViewData["HasItemInCart"] = HasItemInCart;
            ViewData["CartItems"] = CartItems;
            TempData["SeatingId"] = SeatingId;

            if (SeatingId == null)
            {
                return NotFound();
            }

            // Check if the table ID exists in the database
            var table = _unitOfWork.Table.Find(SeatingId);
            if (table == null)
            {
                return NotFound(); // Table ID not found in the database
            }
            table = _unitOfWork.Table.GetFirstOrDefault(p => p.SeatingId == SeatingId);
            var listOrder = _unitOfWork.Orders.GetOrdersByTable((int)SeatingId);
            var order = _unitOfWork.Orders.CheckExpire(listOrder);
            double dateSub = -1.0;
            //Check if the order is not null and subtract the current time from the seating time
            if (order.SeatingDate != null && order.SeatingId != 0 && order.IsUsed != 0 && order.OrderId != null)
                dateSub = DateTime.Now.Subtract((DateTime)order.SeatingDate).TotalSeconds;
            //Check if the order is expired
            if (dateSub != -1.0 && dateSub > ketthuc)
            {
                if (order.IsUsed != -1)
                {
                    order.IsUsed = -1;
                    table.Status = "waiting";
                }
                var deleteCartItems = _unitOfWork.Cart.Where(c => c.SeaatingId == SeatingId).Select(c => c);
                foreach (Cart cart in deleteCartItems)
                {
                    _unitOfWork.Cart.Remove(cart);
                }
                _unitOfWork.Orders.Update(order);
                _unitOfWork.Table.Update(table);
                _unitOfWork.Save();
                return RedirectToAction("Index", new { SeatingId = SeatingId });
            }
            //Check if the table is currently being used
            //Check for table forCustomers that ordered by staff
            if (orderId == null && table.Status == "dang dung")
            {
                ViewBag.orderId = order.OrderId;
                ViewData["timeNow"] = dateSub;
                ViewData["isUsed"] = 1;
                return View();
            }
            //Check for table for Customers that they ordered the table by themselves
            else if (orderId != null && table.Status == "waiting" && _unitOfWork.Orders.GetFirstOrDefault(p => p.OrderId == orderId).IsUsed == 0)
            {
                table.Status = "dang dung";
                _unitOfWork.Table.Update(table);
                _unitOfWork.Save();
                order = _unitOfWork.Orders.CheckOrder(orderId);
                ViewBag.orderId = orderId;
                ViewData["isUsed"] = 1;
                return View();
            }
            //Check for table for Customers that they are currently using the table
            else if (orderId != null && table.Status == "dang dung" && _unitOfWork.Orders.GetFirstOrDefault(p => p.OrderId == orderId).IsUsed == 1)
            {
                ViewData["timeNow"] = dateSub;
                ViewBag.orderId = orderId;
                ViewData["isUsed"] = 1;
                return View();
            }
            //Check for table for Customers that tthe table being expired by the Staff for some reasons
            else if (orderId != null && table.Status == "waiting" && _unitOfWork.Orders.GetFirstOrDefault(p => p.OrderId == orderId).IsUsed == -1)
            {
                ViewData["timeNow"] = ketthuc;
                return RedirectToAction("Index", new { SeatingId = table.SeatingId });
            }

            return View();
        }
        public IActionResult Check(string? code)
        {
            int TableId = (int)TempData["SeatingId"];
            Orders data = _unitOfWork.Table.GetOrderIdByTable(code);
            if (data == null)
            {
                TempData["ErrorOrder"] = "Bạn nhập sai mã code";
                return RedirectToAction("Index", new { SeatingId = TableId });
            }
            if (data.SeatingId != TableId)
            {
                TempData["ErrorOrder"] = "Bạn nhập sai mã code";
                return RedirectToAction("Index", new { SeatingId = TableId });
            }
            if (DateTime.Now.Subtract((DateTime)data.SeatingDate).TotalSeconds < 0)
            {
                TempData["ErrorOrder"] = "Mã code không có hiệu lực tại thời điểm này (vui lòng kiểm tra thời gian được ghi trong mail!).";
                return RedirectToAction("Index", new { SeatingId = TableId });
            }
            return RedirectToAction("Index", new { SeatingId = data.SeatingId, orderId = data.OrderId, model = data });
        }
        public IActionResult Call(int TableId, string OrderId)
        {
            if (TableId == 0)
            {
                return Redirect("/Customers/Booking/Index");
            }
            var cartItems = _unitOfWork.Cart.GetAll();

            foreach (var cartItem in cartItems)
            {
                if (cartItem.SeaatingId == TableId)
                {
                    var menu = new Menu
                    {
                        OrderId = OrderId,
                        FoodId = cartItem.FoodId,
                        Quantity = cartItem.Quantity,
                        TimeOrder = cartItem.TimeOrder
                    };
                    _unitOfWork.Menu.Add(menu);
                }
            }
            _unitOfWork.Cart.Delete(TableId);
            _unitOfWork.Save();
            return RedirectToAction("Index", new { SeatingId = TableId, orderId = OrderId });
        }
        public IActionResult AddToCart(int TableId, string? itemName, int IdMonAn, int Quantity, string OrderId)
        {
            if (TableId == 0)
            {
                return Redirect("/Customers/Booking/Index");
            }
            Cart cartItem = null;
            //Check ingredients
            var checkNL = _unitOfWork.Ingredient.CheckIngredients(IdMonAn, "add", Quantity);
            if (checkNL)
            {
                cartItem = _unitOfWork.Cart.GetFirstOrDefault(c => c.SeaatingId == TableId && c.FoodId == IdMonAn);
            }
            else
            {
                TempData["ErrorMessage"] = "Số lượng nguyên liệu " + itemName + " không đủ";

                return RedirectToAction("Index", new { SeatingId = TableId, orderId = OrderId, HasItemInCart = true });
            }
            //Check if the quantity is greater than 0
            if (Quantity == 0)
            {
                TempData["ErrorMessage"] = "Vui lòng chọn số lượng món " + itemName + " lớn hơn 0";

                return RedirectToAction("Index", new { SeatingId = TableId, orderId = OrderId, HasItemInCart = true });
            }
            //Check if the item already exists in the cart
            if (cartItem != null)
            {
                // If the item already exists in the cart, increment the quantity
                cartItem.Quantity += Quantity;
            }
            else if (checkNL)
            {
                // If the item doesn't exist, add a new item to the cart
                cartItem = new Cart { SeaatingId = TableId, FoodName = itemName, FoodId = IdMonAn, Quantity = Quantity, TimeOrder = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")) };
                _unitOfWork.Cart.Add(cartItem);
            }

            _unitOfWork.Save();

            return RedirectToAction("Index", new { SeatingId = TableId, orderId = OrderId, HasItemInCart = true });
        }
        public IActionResult RemoveFromCart(int TableId, int IdMonAn, int Quantity, string OrderId)
        {
            var cartItem = _unitOfWork.Cart.GetFirstOrDefault(c => c.SeaatingId == TableId && c.FoodId == IdMonAn);

            if (cartItem != null)
            {
                // If the item exists in the cart, decrement the quantity
                cartItem.Quantity -= Quantity;
                var checkNL = _unitOfWork.Ingredient.CheckIngredients(IdMonAn, "remove", Quantity);

                // If the quantity is 0, remove the item from the cart
                if (cartItem.Quantity <= 0)
                {
                    _unitOfWork.Cart.Remove(cartItem);
                }
                _unitOfWork.Save();
            }
            return RedirectToAction("Index", new { SeatingId = TableId, orderId = OrderId });
        }
    }
}
