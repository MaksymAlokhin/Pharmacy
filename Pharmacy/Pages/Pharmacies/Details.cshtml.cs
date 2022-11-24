using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Data;
using PharmacyApp.Models;

namespace PharmacyApp.Pages.Pharmacies
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

      public Pharmacy Pharmacy { get; set; }

        public async Task<IActionResult> OnGetAsync(string sortOrder, string currentFilter, int? pageIndex, int? id)
        {
            PageIndex = pageIndex;
            CurrentSort = sortOrder;
            CurrentFilter = currentFilter;

            if (id == null || _context.Pharmacies == null)
            {
                return NotFound();
            }

            var pharmacy = await _context.Pharmacies
                .Include(p => p.Manager)
                .Include(p => p.PharmacyMedicine)
                .ThenInclude(pm => pm.Medicine)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (pharmacy == null)
            {
                return NotFound();
            }
            else 
            {
                Pharmacy = pharmacy;
            }
            return Page();
        }
    }
}
