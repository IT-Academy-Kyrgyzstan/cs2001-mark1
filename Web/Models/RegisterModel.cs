using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Не указан Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указан Логин")]
        public string Login { get; set; }
        
        [Required, DataType(DataType.Password)]
        
        public string Password { get; set; }
        
        [DataType(DataType.Password), Compare(nameof(Password))]
        [Required(ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Не указан Телефон")]
        public string Phone { get; set; }
    }
}
