using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DataAccess
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [StringLength(20, MinimumLength = 5)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Description { get; set; }
    }
}
