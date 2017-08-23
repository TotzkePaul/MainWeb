using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Main.Data.Core.Domain;
using Main.Data.Core.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using FileResult = Main.Data.Core.Domain.FileResult;

namespace Main.Web.Features.File
{
    public class FileController : Controller
    {
        private readonly IFileRepository _fileRepository;

        public FileController(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }

        public IActionResult Index(int id)
        {
            var fileToRetrieve = _fileRepository.GetFileDescription(id);
            return File(fileToRetrieve.Description, fileToRetrieve.ContentType);
        }

        [Route("download/{id}")]
        [HttpGet]
        public FileContentResult Download(int id)
        {
            var fileDescription = _fileRepository.GetFileDescription(id);
            
            return File(Encoding.ASCII.GetBytes(fileDescription.Description), fileDescription.ContentType);
        }

        public ActionResult Upload()
        {
            ViewBag.Title = "Ex Upload";

            return View();
        }

        public ActionResult MultiUpload()
        {
            ViewBag.Title = "Ex MultiUpload";

            return View();
        }

        [Route("files")]
        [HttpPost]
        [ServiceFilter(typeof(Filters.ValidateMimeMultipartContentFilter))]
        public async Task<IActionResult> UploadFiles(FileDescriptionShort fileDescriptionShort)
        {
            var names = new List<string>();
            var contentTypes = new List<string>();
            if (ModelState.IsValid)
            {
                // http://www.mikesdotnetting.com/article/288/asp-net-5-uploading-files-with-asp-net-mvc-6
                // http://dotnetthoughts.net/file-upload-in-asp-net-5-and-mvc-6/
                foreach (var file in fileDescriptionShort.File)
                {
                    if (file.Length > 0)
                    {
                        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.ToString().Trim('"');
                        contentTypes.Add(file.ContentType);

                        names.Add(fileName);

                        // Extension method update RC2 has removed this 
                        //await file.SaveAsAsync(Path.Combine(_optionsApplicationConfiguration.Value.ServerUploadFolder, fileName));
                    }
                }
            }

            var files = new FileResult
            {
                FileNames = names,
                ContentTypes = contentTypes,
                Description = fileDescriptionShort.Description,
                CreatedTimestamp = DateTime.UtcNow,
                UpdatedTimestamp = DateTime.UtcNow,
            };

            _fileRepository.AddFileDescriptions(files);

            return RedirectToAction("ViewAllFiles", "FileClient");
        }

        public ActionResult ViewAllFiles()
        {
            var model = new AllUploadedFiles { FileShortDescriptions = _fileRepository.GetAllFiles().ToList() };
            return View(model);
        }
    }
}