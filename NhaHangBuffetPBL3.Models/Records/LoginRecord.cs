using System.ComponentModel.DataAnnotations;

namespace NhaHangBuffetPBL3.Models.Records
{
    public class LoginRecord
    {

        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage = "Nhập tên đăng nhập")]
        [MaxLength(20)]

        public string Username { get; set; }
        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "Nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}