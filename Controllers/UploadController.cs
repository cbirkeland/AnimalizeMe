﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
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

            return View();

            //long size = files.Sum(f => f.Length);

            //// full path to file in temp location
            //var filePath = Path.GetTempFileName();

            //foreach (var formFile in files)
            //{
            //    if (formFile.Length > 0)
            //    {
            //        using (var stream = new FileStream(filePath, FileMode.Create))
            //        {
            //            await formFile.CopyToAsync(stream);
            //        }
            //    }
            //}

            //// process uploaded files
            //// Don't rely on or trust the FileName property without validation.

            //return Ok(new { count = files.Count, size, filePath });
        }
    }
}