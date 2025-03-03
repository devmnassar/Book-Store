using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksApp.Models.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookApp.Models.ViewModels
{
    public class RoleManagementVM
    {
        public ApplicationUser ApplicationUser { get; set; }
        public IEnumerable<SelectListItem> RoleList{ get; set; }
        public IEnumerable<SelectListItem> CompanyList{ get; set; }

    }
}
