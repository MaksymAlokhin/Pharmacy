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

namespace PharmacyApp.Pages.Pharmacies
{
    public class CreateModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;

        public int? PageIndex { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public SelectList ManagersSelectList { get; set; }

        public CreateModel(PharmacyApp.Data.PharmacyContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(string sortOrder, string currentFilter, int? pageIndex)
        {
            PageIndex = pageIndex;
            CurrentSort = sortOrder;
            CurrentFilter = currentFilter;

            var ManagersQuery = _context.Managers
                .OrderBy(e => e.LastName)
                .AsNoTracking();

            ManagersSelectList = new SelectList(ManagersQuery, "Id", "FullName"); //list, id, value

            return Page();
        }

        [BindProperty]
        public Pharmacy Pharmacy { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string sortOrder,
            string currentFilter, int? pageIndex)
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


            _context.Pharmacies.Add(Pharmacy);
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
