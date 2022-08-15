using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Store.Models
{
    public class ApplicationUser : IdentityUser
    {
        public String Fullname { get; set; }
    }
}
