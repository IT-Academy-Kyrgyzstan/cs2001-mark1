using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string LastName { get; set; }
        //[Unique Login]
        public string Login { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [DataType(DataType.Password)]
        public string PasswordConfirm { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
    }
}
