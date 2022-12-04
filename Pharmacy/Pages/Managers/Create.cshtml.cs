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
using Microsoft.EntityFrameworkCore;
using PharmacyApp.Data;
using PharmacyApp.Models;

namespace PharmacyApp.Pages.Managers
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
        public SelectList PharmaciesSelectList { get; set; }

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

            var PharmaciesQuery = _context.Pharmacies
                .OrderBy(e => e.Name)
                .AsNoTracking();

            PharmaciesSelectList = new SelectList(PharmaciesQuery, "Id", "Name"); //list, id, value

            Manager = new Manager();
            ViewData["PharmacyId"] = new SelectList(_context.Set<Pharmacy>(), "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Manager Manager { get; set; }


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
                    string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, @"img/people"); //webHost adds 'wwwroot'
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
                    Manager.Photo = trustedFileNameForFileStorage;
                }
            }

            Manager formerManager = await _context.Managers
                .Where(m => m.PharmacyId == Manager.PharmacyId)
                .FirstOrDefaultAsync();
            if (formerManager != null)
            {
                formerManager.PharmacyId = null;
                _context.Attach(formerManager).State = EntityState.Modified;
            }


            _context.Managers.Add(Manager);
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
