using NhaHangBuffetPBL3.Repository.IRepository;
using NhaHangBuffetPBL3.DataAccess.Data;
using NhaHangBuffetPBL3.Models;
using NhaHangBuffetPBL3.Models.Records;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Web.Mvc;

namespace NhaHangBuffetPBL3.Repository
{
    public class TableRepository : Repository<Models.Table>, IRepositoryTable
    {
        private NhaHangBuffetContext _db;
        public TableRepository(NhaHangBuffetContext db) : base(db)
        {
            _db = db;
        }
        public object Select()
        {
            return _db.Tables.Select(t => new SelectListItem
            {
                Value = t.SeatingId.ToString(),
                Text = t.SeatingId.ToString()
            }).ToList();
        }
        public void Update(Models.Table obj)
        {
            _db.Tables.Update(obj);
        }
        public double Subtract(DateTime date)
        {
            var dateNow = System.DateTime.Now;
            var dateSub = dateNow.Subtract((DateTime)date);
            return dateSub.TotalSeconds;
        }
        public Orders GetOrderIdByTable(string OrderId)
        {
            var records = _db.Orders
                        .Select(p => p);
            foreach (var record in records)
            {
                var date = Subtract((DateTime)record.SeatingDate);
                if (date>=3600.0)
                {
                    record.IsUsed=-1;
                }
                else if(record.OrderId==OrderId)
                {
                    return record;
                }
            }
            return null;
        }      
    }
}
