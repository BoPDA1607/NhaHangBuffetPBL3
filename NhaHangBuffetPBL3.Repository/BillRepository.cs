using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Repository.IRepository;
using NhaHangBuffetPBL3.Models.Records;

namespace NhaHangBuffetPBL3.Repository
{
    public class BillRepository: Repository<Bill>, IRepositoryBill
    {
        private NhaHangBuffetContext _db;
        public BillRepository(NhaHangBuffetContext db) : base(db)
        {
            _db = db;
        }

        public List<RevenueRecord> Sum(DateOnly date)
        {
            var hoadon = _db.Bills.Select(hd => hd).ToList();
            List<RevenueRecord> doanhthu = new List<RevenueRecord>();
            List<decimal> tong = new List<decimal>(4) { 0, 0, 0, 0};
            foreach (var item in hoadon)
            {
                if(DateOnly.Parse(item.BillDate.ToString("yyyy-MM-dd")) != date)
                {
                    continue;
                }
                if(item.PaymentType == "Tiền mặt")
                {
                    tong[0] += item.TotalAmount;
                }
                else if(item.PaymentType == "Chuyển khoản")
                {
                    tong[1] += item.TotalAmount;
                }
                else if (item.PaymentType == "Thẻ ngân hàng")
                {
                    tong[2] += item.TotalAmount;
                }
            }
            doanhthu.AddRange(new List<RevenueRecord>()
            {
                new RevenueRecord
                {
                    PaymentType = "Tiền mặt",
                    TotalAmount = tong[0]
                },
                new RevenueRecord
                {
                    PaymentType = "Chuyển khoản",
                    TotalAmount = tong[1]
                },
                new RevenueRecord
                {
                    PaymentType = "Thẻ ngân hàng",
                    TotalAmount = tong[2]
                },
            });
            foreach (var item in doanhthu)
            {
                Console.WriteLine(item.PaymentType + " " + item.TotalAmount);
            }
            
            return doanhthu;
        }

        public void Update(Bill obj)
        {
            _db.Bills.Update(obj);
        }
    }
}
