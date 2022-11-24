using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PharmacyApp.Data;
using PharmacyApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PharmacyApp.Pages.Managers
{
    public class IndexModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;
        private readonly IConfiguration Configuration;
        public string LastNameSort { get; set; }
        public string PharmacySort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public PaginatedList<Manager> Manager { get; set; }

        public IndexModel(PharmacyApp.Data.PharmacyContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        //public IList<Manager> Manager { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            if (_context.Managers != null)
            {
                //Manager = await _context.Managers
                //.Include(m => m.Pharmacy).OrderBy(m => m.LastName).ToListAsync();

                CurrentSort = sortOrder;
                LastNameSort = String.IsNullOrEmpty(sortOrder) ? "last_name_desc" : "";
                PharmacySort = sortOrder == "pharmacy" ? "pharmacy_desc" : "pharmacy";
                if (searchString != null)
                {
                    pageIndex = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                CurrentFilter = searchString;
                IQueryable<Manager> managersIQ = _context.Managers.Include(m => m.Pharmacy);
                if (!String.IsNullOrEmpty(searchString))
                {
                    managersIQ = managersIQ.Where(s => s.LastName.Contains(searchString)
                                           || s.FirstName.Contains(searchString)
                                           || s.Patronymic.Contains(searchString)
                                           || s.Pharmacy.Name.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "last_name_desc":
                        managersIQ = managersIQ.OrderByDescending(s => s.LastName).ThenBy(s => s.FirstName).ThenBy(s => s.Patronymic);
                        break;
                    case "pharmacy":
                        managersIQ = managersIQ.OrderBy(s => s.Pharmacy.Name).ThenBy(s => s.LastName);
                        break;
                    case "pharmacy_desc":
                        managersIQ = managersIQ.OrderByDescending(s => s.Pharmacy.Name).ThenBy(s => s.LastName);
                        break;
                    default:
                        managersIQ = managersIQ.OrderBy(s => s.LastName).ThenBy(s => s.FirstName).ThenBy(s => s.Patronymic);
                        break;
                }
                var pageSize = Configuration.GetValue("PageSize", 4);
                Manager = await PaginatedList<Manager>.CreateAsync(
                    managersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            }
        }
    }
}
