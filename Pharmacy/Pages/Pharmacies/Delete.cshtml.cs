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
    public class DeleteModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;

        public DeleteModel(PharmacyApp.Data.PharmacyContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Pharmacy Pharmacy { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Pharmacies == null)
            {
                return NotFound();
            }

            var pharmacy = await _context.Pharmacies
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Pharmacies == null)
            {
                return NotFound();
            }
            var pharmacy = await _context.Pharmacies.FindAsync(id);

            if (pharmacy != null)
            {
                Pharmacy = pharmacy;
                _context.Pharmacies.Remove(Pharmacy);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
