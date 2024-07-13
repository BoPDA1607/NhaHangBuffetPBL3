namespace NhaHangBuffetPBL3.Models.Records
{
    public class VnPayRequestRecord
    {
        public string email { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedDate { get; set; }
        public int OrderId { get; set; }
        public int NumberOfPeople { get; set; }
        public DateTime date { get; set; }
        public int TableId { get; set; }
    }
}
