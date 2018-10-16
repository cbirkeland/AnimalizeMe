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

			var list = _animalService.GetTagsFromAPI();
			var resultList = new List<Rootobject>();
			foreach (var item in list)
			{
				var result = await _animalService.MakeAnalysisRequest(item);
				resultList.Add(result);
			}

			var tags = resultList.Select(x => x.description.tags);

			foreach (var item in tags)
			{
				

				foreach (var item2 in item)
				{
					var tag = new Tag();
					tag.Name = item2;
					if (!_context.Tags.Any(x => x.Name == item2))
					{
						_context.Add(tag);
						_context.SaveChanges();
					}
				
					
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