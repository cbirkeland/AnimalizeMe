using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AnimalizeMe.Data;
using AnimalizeMe.Models;
using AnimalizeMe.Models.AnalyzerModel;
using AnimalizeMe.Services;
using Microsoft.AspNetCore.Mvc;
using static AnimalizeMe.Models.AnalyzerModel.All;

namespace AnimalizeMe.Controllers
{

	public class ImageWithData
	{
		public Rootobject Result { get; set; }
		public string Url { get; set; }
	}
	public class AnimalController : Controller
	{
		private readonly AnimalizeMeDbContext _context;
		private readonly AnimalService _animalService;

		public AnimalController(AnimalizeMeDbContext context, AnimalService animalService)
		{
			_context = context;
			_animalService = animalService;
		}

		public async Task<IActionResult> Index()
		{
            try
            {


                var imageWithDataList = new List<ImageWithData>();

                List<string> urlList = _animalService.CreatesFileListOfImagePathFromFolder();

                List<string> tagNames = new List<string>();

                // Bygg upp "imageWithDataList" + en lista med alla nya taggar

                foreach (var url in urlList)
                {
                    Rootobject result = await _animalService.MakeAnalysisRequest(url);

                    imageWithDataList.Add(new ImageWithData
                    {
                        Result = result,
                        Url = url.ToLower()
                    });

                    foreach (string tag in result.description.tags)
                    {
                        if (!tagNames.Contains(tag.ToLower()))
                        {
                            tagNames.Add(tag.ToLower());
                        }
                    }
                }

                // Skapa unika taggar

                foreach (var t in tagNames)
                {
                    var tag = new Tag();
                    tag.Name = t;
                    if (!_context.Tags.Any(x => x.Name == t))
                    {
                        _context.Add(tag);
                        _context.SaveChanges();
                    }
                }

                // Sparar creatures med tillhörande bildlänk

                foreach (var url in urlList)
                {
                    if (!_context.Creatures.Any(x => x.ImagePath.ToLower() == url.ToLower()))
                    {
                        var creature = new Creature();

                        creature.ImagePath = url;

                        var result = imageWithDataList.Single(x => x.Url.ToLower() == url.ToLower()).Result;

                        List<CreatureTags> creatureTags = new List<CreatureTags>();
                        foreach (string t in result.description.tags)
                        {
                           
                            Tag oneTag = _context.Tags.Single(x => x.Name.ToLower() == t.ToLower());
                            oneTag.Name = oneTag.Name.ToLower();
                            creatureTags.Add(new CreatureTags { Tag = oneTag });
                        }

                        creature.CreatureTags = creatureTags;
                        _context.Add(creature);
                        _context.SaveChanges();
                    }
                }


                return Ok();
            } catch (Exception ex)
            {
                return Ok(ex); 
            } 
		}


	}
}