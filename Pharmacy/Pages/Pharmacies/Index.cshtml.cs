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
    public class IndexModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;

        public IndexModel(PharmacyApp.Data.PharmacyContext context)
        {
            _context = context;
        }

        public IList<Pharmacy> Pharmacy { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Pharmacy != null)
            {
                Pharmacy = await _context.Pharmacy.ToListAsync();
            }
        }
    }
}
