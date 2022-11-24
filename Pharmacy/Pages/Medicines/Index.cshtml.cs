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

namespace PharmacyApp.Pages.Medicines
{
    public class IndexModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;
        private readonly IConfiguration Configuration;
        public string NameSort { get; set; }
        public string PriceSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Medicine> Medicine { get; set; }

        public IndexModel(PharmacyApp.Data.PharmacyContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        //public IList<Medicine> Medicine { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            if (_context.Medicines != null)
            {
                //Medicine = await _context.Medicines.OrderBy(m => m.Name).ToListAsync();

                CurrentSort = sortOrder;
                NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                PriceSort = sortOrder == "price" ? "price_desc" : "price";
                if (searchString != null)
                {
                    pageIndex = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                CurrentFilter = searchString;
                IQueryable<Medicine> medicinesIQ = _context.Medicines;
                if (!String.IsNullOrEmpty(searchString))
                {
                    medicinesIQ = medicinesIQ.Where(s => s.Name.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        medicinesIQ = medicinesIQ.OrderByDescending(s => s.Name);
                        break;
                    case "price":
                        medicinesIQ = medicinesIQ.OrderBy(s => s.Price).ThenBy(s => s.Name);
                        break;
                    case "price_desc":
                        medicinesIQ = medicinesIQ.OrderByDescending(s => s.Price).ThenBy(s => s.Name);
                        break;
                    default:
                        medicinesIQ = medicinesIQ.OrderBy(s => s.Name);
                        break;
                }
                var pageSize = Configuration.GetValue("PageSize", 4);
                Medicine = await PaginatedList<Medicine>.CreateAsync(
                    medicinesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            }
        }
    }
}
