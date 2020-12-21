using System.ComponentModel.DataAnnotations;

namespace DataAccess
{
    public class User
    {
      
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }                
        public string Login { get; set; }
        public string Password { get; set; }                
        public string Phone { get; set; }
    }
}
