using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Store.Models.ViewModels
{
    public class ProductVM
    {
        public Product product  { get; set; }
        public IEnumerable<SelectListItem> CategorySelectList { get; set;  }
        public IEnumerable<SelectListItem> ApplicationTypeSelectList { get; set;  }
    }
}
