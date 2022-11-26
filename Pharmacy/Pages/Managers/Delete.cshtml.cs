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
    public class DeleteModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public DeleteModel(PharmacyApp.Data.PharmacyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        [BindProperty]
      public Manager Manager { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }

            var manager = await _context.Managers.FirstOrDefaultAsync(m => m.Id == id);

            if (manager == null)
            {
                return NotFound();
            }
            else 
            {
                Manager = manager;
                //Delete photo file
                bool isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
                if (isProduction)
                {
                    if (webHostEnvironment != null)
                    {
                        string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, @"img/people"); //webHost adds 'wwwroot'
                        var oldFile = Manager.Photo;
                        var fileToDelete = string.Empty;
                        if (!string.IsNullOrEmpty(oldFile))
                        {
                            fileToDelete = Path.Combine(uploadsFolder, oldFile);
                        }
                        //Delete photo file
                        if (System.IO.File.Exists(fileToDelete))
                            System.IO.File.Delete(fileToDelete);
                    }
                }

            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }
            var manager = await _context.Managers.FindAsync(id);

            if (manager != null)
            {
                Manager = manager;
                _context.Managers.Remove(Manager);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
