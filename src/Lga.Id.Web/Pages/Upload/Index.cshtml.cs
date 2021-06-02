using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Lga.Id.Core.Interfaces;
using Lga.Id.Infrastructure.Services.BulkUpload;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Lga.Id.Web.Pages.Upload
{
    public class IndexModel : PageModel
    {
        private IHostingEnvironment _environment;
        private readonly IAppLogger<IndexModel> _logger;

        public IndexModel(IHostingEnvironment environment, IAppLogger<IndexModel> logger)
        {
            _environment = environment;
            _logger = logger;


        }

        [BindProperty]
        public IFormFile Upload { get; set; }

        [BindProperty]
        public bool ShowValidationSection { get; set; }

        [BindProperty]
        public RowsValidationEntity ValidationMessages { get; set; }

        public void OnGet()
        {
        }

        [HttpPost]
        public async Task<IActionResult> OnPostAsync()
        {
            var fileName = string.Format("Bulk_{0}_{1}", Guid.NewGuid().ToString(), Path.GetFileName(Upload.FileName));
            if (Upload == null || Upload.Length == 0)
                return Content("No file was selected or file is empty!");

            if (!IsExcelFile(fileName))
                return Content("The selected file is not a valid Excel workbook!");

            // var path = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", Path.GetFileName(Upload.FileName));
            var destinationPath = Path.Combine(_environment.ContentRootPath, "wwwroot\\Uploads", fileName);
            var fileUploadStatus = await UploadFile(destinationPath);

            if (fileUploadStatus)
            {
                return LoadSpreadsheet(destinationPath, fileName);
            }
            else
            {
                return Page();
            }
        }

        private async Task<bool> UploadFile(string desinationPath)
        {
            try
            {
                using (var stream = new FileStream(desinationPath, FileMode.Create))
                {
                    await Upload.CopyToAsync(stream);
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarning("CCR-Failed ot upload the excel file");
                return false;
            }

        }

        public static bool IsExcelFile(string fileName)
        {
            if (String.IsNullOrEmpty(fileName)) return false;
            string fileExtension = Path.GetExtension(fileName);
            if (String.Compare(fileExtension, ".xls", true) == 0) return true;
            if (String.Compare(fileExtension, ".xlsx", true) == 0) return true;
            return false;
        }

        private IActionResult LoadSpreadsheet(string spreadSheetPath, string fileName)
        {
            //TODO
            //Keep here logic to upload the database     

            return Page();


        }
    }
}
