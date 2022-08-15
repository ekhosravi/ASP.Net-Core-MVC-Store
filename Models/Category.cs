using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Store.Models
{
    public class Category
    {
        //prop :  double Tab
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Display Order for category must be greater than 0")]
        public int DisplayOrder { get; set; }
    }
}
