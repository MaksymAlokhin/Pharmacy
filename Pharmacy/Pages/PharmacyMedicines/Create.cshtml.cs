using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacyApp.Data;
using PharmacyApp.Models;

namespace PharmacyApp.Pages.PharmacyMedicines
{
    public class CreateModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;
        public int? PageIndex { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public CreateModel(PharmacyApp.Data.PharmacyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string sortOrder, string currentFilter, int? pageIndex)
        {
            PageIndex = pageIndex;
            CurrentSort = sortOrder;
            CurrentFilter = currentFilter;

            ViewData["MedicineId"] = new SelectList(_context.Medicines.OrderBy(m => m.Name), "Id", "Name");
            ViewData["PharmacyId"] = new SelectList(_context.Pharmacies.OrderBy(p => p.Name), "Id", "Name");
            return Page();
        }

        [BindProperty]
        public PharmacyMedicine PharmacyMedicine { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string sortOrder,
            string currentFilter, int? pageIndex)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PharmacyMedicine.Add(PharmacyMedicine);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index", new
            {
                pageIndex = $"{pageIndex}",
                sortOrder = $"{sortOrder}",
                currentFilter = $"{currentFilter}"
            });
        }
    }
}
