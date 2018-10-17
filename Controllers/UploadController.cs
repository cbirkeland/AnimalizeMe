﻿using AnimalizeMe.Services;
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
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            // full path to file in temp location
            var filePath = System.IO.Path.GetTempFileName();

            if (file.Length <= 0)
                throw new Exception("Filen är tom!");

            using (var stream = new FileStream(filePath, FileMode.Create))
            {

                await file.CopyToAsync(stream);
            }

            var x = new AnimalService();

            var svar = await x.MakeAnalysisRequest(filePath);


            string url = x.GetAnimalUrlThatMathcesTags(svar.description.tags);

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            return Ok(url);
            //return Ok(new { count = files.Count, size, filePath });
        }
    }
}
