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
        public List<int> SelectedMedicines { get; set; }
        public SelectList MedicinesSelectList { get; set; }

        public EditModel(PharmacyApp.Data.PharmacyContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Pharmacy Pharmacy { get; set; } = default!;

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
            Pharmacy = pharmacy;

            var MedicinesQuery = _context.Medicines
                .OrderBy(m => m.Name)
                .AsNoTracking();
            MedicinesSelectList = new SelectList(MedicinesQuery, "Id", "Name"); //list, id, value

            SelectedMedicines = new List<int>();
            foreach (var pharmacyMedicine in Pharmacy.PharmacyMedicine)
            {
                SelectedMedicines.Add(pharmacyMedicine.Medicine.Id);
            }

            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int[] SelectedMedicines)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            UpdateMedicines(SelectedMedicines, Pharmacy);

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

            return RedirectToPage("./Index");
        }

        private bool PharmacyExists(int id)
        {
            return _context.Pharmacies.Any(e => e.Id == id);
        }

        private void UpdateMedicines(int[] SelectedMedicines, Pharmacy Pharmacy)
        {
            //if (SelectedMedicines == null || SelectedMedicines.Length == 0)
            //{
            //    Pharmacy.Branches = new List<Branch>();
            //    return;
            //}

            //var SelectedBranchesHS = new HashSet<int>(SelectedMedicines);
            //var CompanyBranchesHS = new HashSet<int>
            //    (Pharmacy.Branches.Select(s => s.Id));
            //foreach (var branch in _context.Branches)
            //{
            //    //If items are selected
            //    if (SelectedBranchesHS.Contains(branch.Id))
            //    {
            //        //If item not present
            //        if (!CompanyBranchesHS.Contains(branch.Id))
            //        {
            //            Pharmacy.Branches.Add(branch);
            //            if (!CompaniesWithModifiedState.Contains(Pharmacy.Id))
            //                CompaniesWithModifiedState.Add(Pharmacy.Id);
            //        }
            //    }
            //    //If items are not selected
            //    else
            //    {
            //        //If item is present
            //        if (CompanyBranchesHS.Contains(branch.Id))
            //        {
            //            var toRemove = Pharmacy.Branches.Single(s => s.Id == branch.Id);
            //            Pharmacy.Branches.Remove(toRemove);
            //        }
            //    }
            //}
        }

    }
}
