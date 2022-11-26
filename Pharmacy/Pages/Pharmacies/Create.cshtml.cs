using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacyApp.Data;
using PharmacyApp.Models;

namespace PharmacyApp.Pages.Pharmacies
{
    public class CreateModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;

        public CreateModel(PharmacyApp.Data.PharmacyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Pharmacy Pharmacy { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Pharmacies.Add(Pharmacy);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
