using AnimalizeMe.Data;
using AnimalizeMe.Repository;
using AnimalizeMe.Services;
using AnimalizeMe.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnimalizeMe.Controllers
{
    public class UploadController : Controller
    {
        private readonly AnimalService _animalService;
        private readonly IHostingEnvironment _env;

        public UploadController(AnimalService animalService, IHostingEnvironment env, IAnimalRepository repo)
        {
            _animalService = animalService;
            _env = env;
            _repo = repo;
        }

        public IAnimalRepository _repo;

        public IActionResult Index()
        {
            return View(new UploadViewModel
            {
                UploadedImage= "http://placehold.it/180"
            });
        }


        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var fileName = Guid.NewGuid().ToString()+".jpg";
            var fileNameWithPath = Path.Combine(_env.WebRootPath, "uploadedImages", fileName);

            // full path to file in temp location
            //var filePath = System.IO.Path.GetTempFileName();
            
            //if (file.Length <= 0)
            //    throw new Exception("Filen är tom!");

            using (var stream = new FileStream(fileNameWithPath, FileMode.Create))
            {

                await file.CopyToAsync(stream);
            }

            var svar = await _animalService.MakeAnalysisRequest(fileNameWithPath);


            string url = _animalService.GetAnimalUrlThatMathcesTags(svar.description.tags, _repo.GetAllCreatures());

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return View("Index", new UploadViewModel {
                AnimalImage = url,
                UploadedImage = fileName

            });
            //return Ok(new { count = files.Count, size, filePath });
        }
    }
}
