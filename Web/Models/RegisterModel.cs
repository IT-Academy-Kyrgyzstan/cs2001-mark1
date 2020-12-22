﻿using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class RegisterModel
    {
        [BindNever]
        public int Id { get; set; }
        [Display(Name = "Введите имя")]
        [StringLength(10)]
        [Required(ErrorMessage = "Не указан Имя")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Не указан Логин")]
        [StringLength(10)]
        public string Login { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare("Password")]
        [Required(ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Required(ErrorMessage = "Не указан Телефон")]
        public string Phone { get; set; }
    }
}
