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
    public class EditModel : PageModel
    {
        private readonly PharmacyApp.Data.PharmacyContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public IFormFile FormFile { get; set; }
        private readonly string[] permittedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".bmp", ".tif", ".tiff" };

        public int? PageIndex { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }

        public EditModel(PharmacyApp.Data.PharmacyContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this.webHostEnvironment = webHostEnvironment;
        }

        public SelectList PharmaciesSelectList { get; set; }

        [BindProperty]
        public Manager Manager { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(string sortOrder, string currentFilter, int? pageIndex, int? id)
        {
            PageIndex = pageIndex;
            CurrentSort = sortOrder;
            CurrentFilter = currentFilter;

            var PharmaciesQuery = _context.Pharmacies
                .OrderBy(e => e.Name)
                .AsNoTracking();

            PharmaciesSelectList = new SelectList(PharmaciesQuery, "Id", "Name"); //list, id, value

            if (id == null || _context.Managers == null)
            {
                return NotFound();
            }

            var manager =  await _context.Managers
                .Include(m => m.Pharmacy)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (manager == null)
            {
                return NotFound();
            }
            Manager = manager;
           ViewData["PharmacyId"] = new SelectList(_context.Set<Pharmacy>(), "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string sortOrder,
            string currentFilter, int? pageIndex, int PharmacyId)
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

                    bool isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production";
                    #region Delete old photo file
                    if (isProduction)
                    {
                        var oldFile = Manager.Photo;
                        var fileToDelete = string.Empty;
                        if (!string.IsNullOrEmpty(oldFile))
                        {
                            fileToDelete = Path.Combine(uploadsFolder, oldFile);
                        }

                        if (System.IO.File.Exists(fileToDelete))
                        {
                            System.IO.File.Delete(fileToDelete);
                        }
                    }
                    #endregion

                    //Update photo
                    Manager.Photo = trustedFileNameForFileStorage;
                }

            }


            Manager formerManager = await _context.Managers
                    .Where(m => m.PharmacyId == Manager.PharmacyId && m.Id != Manager.Id)
                    .FirstOrDefaultAsync();
            if (formerManager != null)
            {
                formerManager.PharmacyId = null;
                _context.Attach(formerManager).State = EntityState.Modified;
            }

            _context.Attach(Manager).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ManagerExists(Manager.Id))
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

        private bool ManagerExists(int id)
        {
          return _context.Managers.Any(e => e.Id == id);
        }
    }
}
