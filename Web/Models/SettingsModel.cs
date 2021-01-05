using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Web.Models
{
    public class SettingsModel
    {
        [StringLength(25, MinimumLength = 3)]
        public string Name { get; set; }

        [Required, DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required, DataType(DataType.Password)]
        public string NewPassword { get; set; }
        
        [DataType(DataType.Password), Compare("NewPassword")]
        [Required(ErrorMessage = "Пароли не совпадают")]
        public string ConfirmNewPassword { get; set; }

    }
}
