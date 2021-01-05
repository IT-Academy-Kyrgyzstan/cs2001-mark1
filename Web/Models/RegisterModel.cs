using System.ComponentModel.DataAnnotations;


namespace Web.Models
{
    public class RegisterModel
    {
        [Display(Name = "Введите имя")]
        [StringLength(25, MinimumLength = 3)]
        [Required(ErrorMessage = "Не указан Имя")]
        public string Name { get; set; }
        
        [Required(ErrorMessage = "Не указан Логин")]
        public string Login { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare("Password")]
        [Required(ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
