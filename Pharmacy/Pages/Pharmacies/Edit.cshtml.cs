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

        public List<int> SelectedMedicines { get; set; }
        public SelectList MedicinesSelectList { get; set; }
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

            var MedicinesQuery = _context.Medicines
                .OrderBy(m => m.Name)
                .AsNoTracking();
            MedicinesSelectList = new SelectList(MedicinesQuery, "Id", "Name"); //list, id, value

            SelectedMedicines = new List<int>();
            foreach (var pharmacyMedicine in Pharmacy.PharmacyMedicine)
            {
                if (pharmacyMedicine.PharmacyId == Pharmacy.Id)
                {
                    SelectedMedicines.Add(pharmacyMedicine.MedicineId);
                }
            }

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

        private void UpdateMedicines(int[] SelectedMedicines, Pharmacy Pharmacy)
        {
            //if (SelectedMedicines == null || SelectedMedicines.Length == 0)
            //{
            //    //foreach (var pharmacyMedicine in Pharmacy.PharmacyMedicine)
            //    //{
            //    //    if (pharmacyMedicine.PharmacyId == Pharmacy.Id)
            //    //    {
            //    //        _context.PharmacyMedicine.Remove(pharmacyMedicine);
            //    //    }
            //    //}
            //    Pharmacy.PharmacyMedicine = new List<PharmacyMedicine>();
            //    return;
            //}

            //var SelectedMedicinesHS = new HashSet<int>(SelectedMedicines);
            //var PharmacyMedicinesHS = new HashSet<int>();
            //foreach (var pharmacyMedicine in Pharmacy.PharmacyMedicine)
            //{
            //    if (pharmacyMedicine.PharmacyId == Pharmacy.Id)
            //    {
            //        PharmacyMedicinesHS.Add(pharmacyMedicine.MedicineId);
            //    }
            //}

            //foreach (var medicine in _context.Medicines)
            //{
            //    //If items are selected
            //    if (SelectedMedicinesHS.Contains(medicine.Id))
            //    {
            //        //If item not present
            //        if (!PharmacyMedicinesHS.Contains(medicine.Id))
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
            //        if (PharmacyMedicinesHS.Contains(branch.Id))
            //        {
            //            var toRemove = Pharmacy.Branches.Single(s => s.Id == branch.Id);
            //            Pharmacy.Branches.Remove(toRemove);
            //        }
            //    }
            //}
        }

    }
}
