using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Data;
using PharmacyApp.Models;

namespace PharmacyApp.Pages.Medicines
{
    public class IndexModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;

        public IndexModel(PharmacyApp.Data.PharmacyContext context)
        {
            _context = context;
        }

        public IList<Medicine> Medicine { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Medicines != null)
            {
                Medicine = await _context.Medicines.OrderBy(m => m.Name).ToListAsync();
            }
        }
    }
}
