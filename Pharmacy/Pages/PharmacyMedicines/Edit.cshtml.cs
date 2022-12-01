using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Data;
using PharmacyApp.Models;

namespace PharmacyApp.Pages.PharmacyMedicines
{
    public class EditModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;
        public int? PageIndex { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public EditModel(PharmacyApp.Data.PharmacyContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["MedicineId"] = new SelectList(_context.Medicines.OrderBy(m => m.Name), "Id", "Name");
           ViewData["PharmacyId"] = new SelectList(_context.Pharmacies.OrderBy(p => p.Name), "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string sortOrder,
            string currentFilter, int? pageIndex)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(PharmacyMedicine).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PharmacyMedicineExists(PharmacyMedicine.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index", new
            {
                pageIndex = $"{pageIndex}",
                sortOrder = $"{sortOrder}",
                currentFilter = $"{currentFilter}"
            });
        }

        private bool PharmacyMedicineExists(int id)
        {
            return _context.PharmacyMedicine.Any(e => e.Id == id);
        }
    }
}
