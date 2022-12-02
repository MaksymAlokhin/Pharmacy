using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Data;
using PharmacyApp.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace PharmacyApp.Pages.Pharmacies
{
    public class EditModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;

        public int? PageIndex { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public SelectList ManagersSelectList { get; set; }

        public EditModel(PharmacyApp.Data.PharmacyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pharmacy Pharmacy { get; set; } = default!;

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
            Pharmacy = pharmacy;

            var ManagersQuery = _context.Managers
                .OrderBy(e => e.LastName)
                .AsNoTracking();

            ManagersSelectList = new SelectList(ManagersQuery, "Id", "FullName"); //list, id, value

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string sortOrder,
            string currentFilter, int? pageIndex, int[] SelectedMedicines)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Manager Manager = await _context.Managers
                .Where(m => m.Id == Pharmacy.Manager.Id)
                .FirstOrDefaultAsync();

            if (Manager != null)
            {
                Pharmacy.Manager = Manager;
                _context.Attach(Manager).State = EntityState.Modified;
            }

            Pharmacy pharmacyWithSameManager = await _context.Pharmacies
                .Where(p => p.Manager.Id == Pharmacy.Manager.Id)
                .FirstOrDefaultAsync();

            if (pharmacyWithSameManager != null)
            {
                pharmacyWithSameManager.Manager = null;
                _context.Attach(pharmacyWithSameManager).State = EntityState.Modified;
            }

            _context.Attach(Pharmacy).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PharmacyExists(Pharmacy.Id))
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

        private bool PharmacyExists(int id)
        {
            return _context.Pharmacies.Any(e => e.Id == id);
        }
    }
}
