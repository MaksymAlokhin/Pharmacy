using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Data;
using PharmacyApp.Models;

namespace PharmacyApp.Pages.PharmacyMedicines
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

        public PharmacyMedicine PharmacyMedicine { get; set; }

        public async Task<IActionResult> OnGetAsync(string sortOrder, string currentFilter, int? pageIndex, int? id)
        {
            PageIndex = pageIndex;
            CurrentSort = sortOrder;
            CurrentFilter = currentFilter;

            if (id == null)
            {
                return NotFound();
            }

            PharmacyMedicine = await _context.PharmacyMedicine
                .Include(p => p.Medicine)
                .Include(p => p.Pharmacy).FirstOrDefaultAsync(m => m.Id == id);

            if (PharmacyMedicine == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
