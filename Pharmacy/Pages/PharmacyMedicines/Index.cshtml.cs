using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using PharmacyApp.Data;
using PharmacyApp.Models;

namespace PharmacyApp.Pages.PharmacyMedicines
{
    public class IndexModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;
        private readonly IConfiguration Configuration;
        public string QuantitySort { get; set; }
        public string MedicineSort { get; set; }
        public string PharmacySort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public PaginatedList<PharmacyMedicine> PharmacyMedicine { get; set; }
        public SelectList PharmaciesSelectList { get; set; }
        public int SelectedPharmacyId { get; set; }

        public IndexModel(PharmacyApp.Data.PharmacyContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        //public IList<PharmacyMedicine> PharmacyMedicine { get;set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex, int SelectedPharmacyId)
        {
            this.SelectedPharmacyId = SelectedPharmacyId;
            var PharmaciesQuery = _context.Pharmacies
                .OrderBy(e => e.Name)
                .AsNoTracking();

            PharmaciesSelectList = new SelectList(PharmaciesQuery, "Id", "Name", SelectedPharmacyId); //list, id, value

            CurrentSort = sortOrder;
            MedicineSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            PharmacySort = sortOrder == "pharmacy" ? "pharmacy_desc" : "pharmacy";
            QuantitySort = sortOrder == "quantity" ? "quantity_desc" : "quantity";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;
            IQueryable<PharmacyMedicine> pharmacyMedicineIQ = _context.PharmacyMedicine
                .Include(p => p.Medicine).Include(p => p.Pharmacy);

            if (!String.IsNullOrEmpty(searchString))
            {
                pharmacyMedicineIQ = pharmacyMedicineIQ
                    .Where(s => s.Medicine.Name.Contains(searchString)
                                       || s.Pharmacy.Name.Contains(searchString));
            }
            var TEST = pharmacyMedicineIQ.ToList();
            switch (sortOrder)
            {
                case "name_desc":
                    pharmacyMedicineIQ = pharmacyMedicineIQ.OrderByDescending(s => s.Medicine.Name);
                    break;
                case "pharmacy":
                    pharmacyMedicineIQ = pharmacyMedicineIQ.OrderBy(s => s.Pharmacy.Name);
                    break;
                case "pharmacy_desc":
                    pharmacyMedicineIQ = pharmacyMedicineIQ.OrderByDescending(s => s.Pharmacy.Name);
                    break;
                case "quantity":
                    pharmacyMedicineIQ = pharmacyMedicineIQ.OrderBy(s => s.Quantity);
                    break;
                case "quantity_desc":
                    pharmacyMedicineIQ = pharmacyMedicineIQ.OrderByDescending(s => s.Quantity);
                    break;
                default:
                    pharmacyMedicineIQ = pharmacyMedicineIQ.OrderBy(s => s.Medicine.Name);
                    break;
            }

            if (SelectedPharmacyId != 0)
            {
                pharmacyMedicineIQ = pharmacyMedicineIQ.Where(p => p.PharmacyId == SelectedPharmacyId);
            }

            var pageSize = Configuration.GetValue("PageSize", 4);

            PharmacyMedicine = await PaginatedList<PharmacyMedicine>.CreateAsync(
                pharmacyMedicineIQ.AsNoTracking(), pageIndex ?? 1, pageSize);


            //PharmacyMedicine = await _context.PharmacyMedicine
            //    .Include(p => p.Medicine)
            //    .Include(p => p.Pharmacy)
            //    .ToListAsync();
        }
    }
}
