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

		public AnimalController(AnimalizeMeDbContext context)
		{
			_context = context;
			_animalService = new AnimalService();
		}

		AnimalService _animalService;


		public async Task<IActionResult> Index()
		{

			var imageWithDataList = new List<ImageWithData>();

			List<string> urlList = _animalService.GetTagsFromAPI();

			List<string> tagNames = new List<string>();

			// Bygg upp "imageWithDataList" + en lista med alla nya taggar

			foreach (var url in urlList)
			{
				Rootobject result = await _animalService.MakeAnalysisRequest(url);

				imageWithDataList.Add(new ImageWithData
				{
					Result = result,
					Url = url
				});

				foreach(string tag in result.description.tags)
				{
					if (!tagNames.Contains(tag))
					{
						tagNames.Add(tag);
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
				if (!_context.Creatures.Any(x => x.ImagePath == url))
				{
					var creature = new Creature();

					creature.ImagePath = url;

					var result = imageWithDataList.Single(x => x.Url == url).Result;

					List<CreatureTags> creatureTags = new List<CreatureTags>();
					foreach (string t in result.description.tags)
					{
						Tag ttt = _context.Tags.Single(x => x.Name == t);
						
						creatureTags.Add(new CreatureTags { Tag = ttt });
					}

					creature.CreatureTags = creatureTags;
					_context.Add(creature);
					_context.SaveChanges();
				}
			}

			return Ok();
		}



		//[HttpPost]  // add creature to database POST
		//public IActionResult AddTagsToDatabase(List<string> allTags)
		//{
		//	allTags = Index();
		//	foreach (var item in allTags)
		//	{
		//		var tags = new Tag();
				
		//		var desc = new Description();

		//		tags.Description.tags = desc.tags;
		//	}
			
			
  //          return Ok("");

			


		//}




		//public async Task<IActionResult> Board(string id)
		//{
		//	//return Ok("Du vill se listor för boarden " + id);
		//	List<TrelloList> result = await _trelloService.GetAllListsForBoard(id);
		//	return View(result);

		//}

		//public IActionResult AddCardForm(string id)
		//{
		//	var vm = new AddTrelloPost
		//	{
		//		TrelloListId = id
		//	};

		//	return View("AddCardForm", vm);
		//}

		//[HttpPost]
		//public async Task<IActionResult> AddCardResponse(AddTrelloPost post)
		//{
		//    await _animalService.MakeAnalysisRequest(@"C:\Users\Administrator\Desktop\AnimalizeMe\Bilder\bowtie.jpg");

		//    return View("AddCardResponse", post);
		//}
	}
}