using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Data;
using PharmacyApp.Models;

namespace PharmacyApp.Pages.Managers
{
    public class DetailsModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;

        public int? PageIndex { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public DetailsModel(PharmacyApp.Data.PharmacyContext context)
        {
            _context = context;
        }

      public Manager Manager { get; set; }

        public async Task<IActionResult> OnGetAsync(string sortOrder, string currentFilter, int? pageIndex, int? id)
        {
            PageIndex = pageIndex;
            CurrentSort = sortOrder;
            CurrentFilter = currentFilter;

            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers
                .Include(m => m.Pharmacy)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (manager == null)
            {
                return NotFound();
            }
            else 
            {
                Manager = manager;
            }
            return Page();
        }
    }
}
