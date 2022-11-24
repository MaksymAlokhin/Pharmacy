using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Data;
using PharmacyApp.Models;

namespace PharmacyApp.Pages.Managers
{
    public class IndexModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;

        public IndexModel(PharmacyApp.Data.PharmacyContext context)
        {
            _context = context;
        }

        public IList<Manager> Manager { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Manager != null)
            {
                Manager = await _context.Manager
                .Include(m => m.Pharmacy).ToListAsync();
            }
        }
    }
}
