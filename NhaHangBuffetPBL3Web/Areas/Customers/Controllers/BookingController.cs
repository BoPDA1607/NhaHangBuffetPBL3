using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;
using NhaHangBuffetPBL3.Services.VNPay;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Repository.IRepository;
using Newtonsoft.Json;
using NhaHangBuffetPBL3.Models.Records;

namespace NhaHangBuffetPBL3.Areas.Customers.Controllers
{
    [Area("Customers")]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IVnPayService _vnPayservice;

        public BookingController(IUnitOfWork context, IVnPayService vnPayService) { _unitOfWork = context; _vnPayservice = vnPayService; }

        public IActionResult Index(string? message, DateTime? BookingTime)
        {
            if(message=="TimeConflict" && BookingTime != null)
            {
                string validate = "";
                if (((DateTime)BookingTime).AddHours(-1.0) > DateTime.Now)
                    validate += $"hoặc đặt trước {((DateTime)BookingTime).AddHours(-1.0)}";
                ModelState.AddModelError("error", $"Bàn đã được đặt! Bạn có thể đặt sau {((DateTime)BookingTime).AddHours(1.0)}" + validate);
            }
            else if (message == "InvalidTime")
            {
                ModelState.AddModelError("error", "Thời gian không hợp lệ!");
            }
            ViewBag.Item = _unitOfWork.Table.Select(t=>t.SeatingId);
            return View();
        }
        public static async Task<bool> SendMail(string _from, string _to, string _subject, string _body, SmtpClient client)
        {
            //create mail
            MailMessage message = new MailMessage(
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);

            try
            {
                await client.SendMailAsync(message);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public static async Task<bool> SendMailGoogleSmtp(string _from, string _to, string _subject,
                                                            string _body, string _gmailsend, string _gmailpassword)
        {
            MailMessage message = new MailMessage(
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);

            // create SmtpClient connect to smtp.gmail.com
            using (SmtpClient client = new SmtpClient("smtp.gmail.com"))
            {

                client.Port = 587;
                client.Credentials = new NetworkCredential(_gmailsend, _gmailpassword);
                client.EnableSsl = true;
                return await SendMail(_from, _to, _subject, _body, client);
            }
        }
        public async void SendAutoresponderEmail(string code, BookingRecord inf)
        {
            await SendMailGoogleSmtp("dh11112004@gmail.com", inf.email, "Xác Nhận Đặt Bàn", $"Bạn đặt trước bàn:{inf.TableId}, cho {inf.NumberOfPeople} người, vào ngày {inf.SeatingDate}\nHạn sử dụng là một tiếng\nMã Bàn Của Bạn Là:{code}",
                                              "dh11112004@gmail.com", "rtvycvvqnbwqwymo");
        }
        public IActionResult BookTable(BookingRecord booking)
        {
            if (ModelState.IsValid)
            {
                var orders = _unitOfWork.Orders.GetOrdersByTable(booking.TableId);
                foreach(var order in orders)
                {
                    if ((order.SeatingDate <= booking.SeatingDate && booking.SeatingDate <= ((DateTime)order.SeatingDate).AddHours(1.0)) && (order.IsUsed == 1 || order.IsUsed == 0))
                    {
                        return RedirectToAction("Index", new {message="TimeConflict", BookingTime =order.SeatingDate });
                    }
                    else if(booking.SeatingDate <= order.SeatingDate && order.SeatingDate <= ((DateTime)booking.SeatingDate).AddHours(1.0) && (order.IsUsed == 1 || order.IsUsed == 0))
                    {
                        return RedirectToAction("Index", new { message = "TimeConflict", BookingTime = order.SeatingDate });
                    }
                    else if(booking.SeatingDate<DateTime.Now)
                    {
                        return RedirectToAction("Index", new { message = "InvalidTime", BookingTime =DateTime.Now });
                    }
                    else if((DateTime.Now).Subtract((DateTime)order.SeatingDate).TotalSeconds >= 3600 && order.IsUsed == 1)
                    {
                        order.IsUsed = -1;
                        var table = _unitOfWork.Table.GetFirstOrDefault(p=>p.SeatingId==order.SeatingId);
                        table.Status = "waiting";
                        _unitOfWork.Table.Update(table);
                        _unitOfWork.Orders.Update(order);
                    }
                }
                _unitOfWork.Save();
                var vnPayModel = new VnPayRequestRecord
                {
                    Amount = booking.NumberOfPeople * 10000000,
                    CreatedDate = DateTime.Now,
                    email = $"{booking.email}",
                    OrderId = new Random().Next(1000, 10000),
                    NumberOfPeople= booking.NumberOfPeople,
                    date= booking.SeatingDate,
					TableId = booking.TableId
				};
                return Redirect(_vnPayservice.CreatePaymentUrl(HttpContext, vnPayModel));
            }
            else
            {
                return View("Index");
            }
        }

        public IActionResult PaymentFail()
        {
            return View();
        }
        public IActionResult PaymentSuccess(DateTime paymentDate)
        {
            ViewData["paymentDate"] = paymentDate;
            var model = TempData["BookingRecord"] != null ? System.Text.Json.JsonSerializer.Deserialize<BookingRecord>(TempData["BookingRecord"].ToString()) : null;
            if(model == null) { return RedirectToAction("PaymentFail"); }
            return View(model);
        }
        public IActionResult PaymentCallBack()
        {
            var response = _vnPayservice.PaymentExecute(Request.Query);
            if (response == null || response.VnPayResponseCode != "00")
            {
                TempData["Message"] = $"Lỗi thanh toán VNPay: {response.VnPayResponseCode}";
                return RedirectToAction("PaymentFail");
            }
            TempData["Message"] = "thanh toán VNPay thành công";
            string code = _unitOfWork.Orders.GenerateUniqueCode();
            // Get the vnp_OrderInfo field from the response
            var orderInfoJson = response.OrderDescription;

            // Deserialize the JSON string back to an object
            var orderInfo = JsonConvert.DeserializeObject<dynamic>(orderInfoJson);
            var data = new BookingRecord
            {
                email = (string)orderInfo.OrderEmail,
                TableId = (int)orderInfo.tableId,
                SeatingDate = (DateTime)orderInfo.seatingDate,
                NumberOfPeople = (int)orderInfo.People
            };
            
            TempData["BookingRecord"] = System.Text.Json.JsonSerializer.Serialize(data);
            _unitOfWork.Orders.StorePreOrderCode(code, data);
		    SendAutoresponderEmail(code, data);
            var model = new Bill
            {
                BillDate = DateTime.Now,
                NumberOfPeople = data.NumberOfPeople,
                TotalAmount = 100000 * data.NumberOfPeople,
                SeatingId = data.TableId,
                PaymentType = "Chuyển khoản"
            };
            _unitOfWork.Bill.Add(model);
            _unitOfWork.Save();
            return RedirectToAction("PaymentSuccess", new { paymentDate = model.BillDate });
        }
    }
}

