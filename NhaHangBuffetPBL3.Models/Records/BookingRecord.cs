using System.ComponentModel.DataAnnotations;

namespace NhaHangBuffetPBL3.Models.Records
{
    public class BookingRecord
    {
        [Required(ErrorMessage = "Nhập email")]
        public string email { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn bàn")]
        public int TableId { get; set; }
        [Required(ErrorMessage = "Vui lòng chọn thời gian")]
        public DateTime SeatingDate { get; set; }
        [Required(ErrorMessage = "Vui lòng điền số lượng người")]
        public int NumberOfPeople { get; set; }
    }
}
