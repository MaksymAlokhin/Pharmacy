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

namespace PharmacyApp.Pages.Pharmacies
{
    public class IndexModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;
        private readonly IConfiguration Configuration;
        public string NameSort { get; set; }
        public string CitySort { get; set; }
        public string ManagerSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<Pharmacy> Pharmacy { get; set; }

        public IndexModel(PharmacyApp.Data.PharmacyContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        //public IList<Pharmacy> Pharmacy { get;set; } = default!;

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            if (_context.Pharmacies != null)
            {
                //Pharmacy = await _context.Pharmacies.ToListAsync();

                CurrentSort = sortOrder;
                NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                CitySort = sortOrder == "city" ? "city_desc" : "city";
                ManagerSort = sortOrder == "manager" ? "manager_desc" : "manager";
                if (searchString != null)
                {
                    pageIndex = 1;
                }
                else
                {
                    searchString = currentFilter;
                }
                CurrentFilter = searchString;
                IQueryable<Pharmacy> pharmaciesIQ = _context.Pharmacies.Include(p => p.Manager);
                if (!String.IsNullOrEmpty(searchString))
                {
                    pharmaciesIQ = pharmaciesIQ.Where(s => s.Name.Contains(searchString)
                                           || s.City.Contains(searchString));
                }
                switch (sortOrder)
                {
                    case "name_desc":
                        pharmaciesIQ = pharmaciesIQ.OrderByDescending(s => s.Name);
                        break;
                    case "city":
                        pharmaciesIQ = pharmaciesIQ.OrderBy(s => s.City).ThenBy(s => s.Name);
                        break;
                    case "city_desc":
                        pharmaciesIQ = pharmaciesIQ.OrderByDescending(s => s.City).ThenBy(s => s.Name);
                        break;
                    case "manager":
                        pharmaciesIQ = pharmaciesIQ.OrderBy(s => s.Manager.LastName)
                            .ThenBy(s => s.Manager.FirstName).ThenBy(s => s.Manager.Patronymic).ThenBy(s => s.Name);
                        break;
                    case "manager_desc":
                        pharmaciesIQ = pharmaciesIQ.OrderByDescending(s => s.Manager.LastName)
                            .ThenBy(s => s.Manager.FirstName).ThenBy(s => s.Manager.Patronymic).ThenBy(s => s.Name);
                        break;
                    default:
                        pharmaciesIQ = pharmaciesIQ.OrderBy(s => s.Name);
                        break;
                }
                var pageSize = Configuration.GetValue("PageSize", 4);
                Pharmacy = await PaginatedList<Pharmacy>.CreateAsync(
                    pharmaciesIQ.AsNoTracking(), pageIndex ?? 1, pageSize);

            }
        }
    }
}
