using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using PharmacyApp.Data;
using PharmacyApp.Models;

namespace PharmacyApp.Pages.Medicines
{
    public class CreateModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;
        public IFormFile FormFile { get; set; }
        private readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff" };
        private readonly IWebHostEnvironment webHostEnvironment;

        public int? PageIndex { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public CreateModel(PharmacyApp.Data.PharmacyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public IActionResult OnGet(string sortOrder, string currentFilter, int? pageIndex)
        {
            PageIndex = pageIndex;
            CurrentSort = sortOrder;
            CurrentFilter = currentFilter;

            Medicine = new Medicine();
            return Page();
        }

        [BindProperty]
        public Medicine Medicine { get; set; }
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string sortOrder,
            string currentFilter, int? pageIndex)
        {
          if (!ModelState.IsValid)
            {
                return Page();
            }

            if (FormFile != null)
            {
                //Check permitted extensions for photo
                var ext = Path.GetExtension(FormFile.FileName).ToLowerInvariant();
                if (!string.IsNullOrEmpty(ext) || permittedExtensions.Contains(ext))
                {
                    //Get random filename for server storage
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, @"img/medicine"); //webHost adds 'wwwroot'
                    var trustedFileNameForFileStorage = Path.GetRandomFileName();
                    trustedFileNameForFileStorage = trustedFileNameForFileStorage.Substring(0, 8)
                        + trustedFileNameForFileStorage.Substring(9) + ext;
                    var filePath = Path.Combine(uploadsFolder, trustedFileNameForFileStorage);

                    //Copy data to a new file
                    using (var fileStream = System.IO.File.Create(filePath))
                    {
                        await FormFile.CopyToAsync(fileStream);
                    }

                    //Update photo
                    Medicine.BoxArt = trustedFileNameForFileStorage;
                }
            }


            _context.Medicines.Add(Medicine);
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
