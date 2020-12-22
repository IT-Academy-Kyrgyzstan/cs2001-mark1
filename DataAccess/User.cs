using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public class User
    {
      
        [Key]
        public int Id { get; set; }
        [MaxLength(10)]
        public string Name { get; set; }
        [MaxLength(10)]
        public string Login { get; set; }
        [MaxLength(10)]
        public string Password { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
    }
}
