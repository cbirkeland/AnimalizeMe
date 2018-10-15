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

namespace AnimalizeMe.Controllers
{
    public class AnimalController : Controller
    {
		private readonly AnimalizeMeDbContext _context;

		public AnimalController(AnimalizeMeDbContext context)
		{
			_context = context;
		}

		AnimalService _animalService;
		public AnimalController()
		{
            _animalService = new AnimalService(); 


		}

        public async Task<IActionResult> Index()
        {

            var result = await _animalService.MakeAnalysisRequest(@"C:\Users\Administrator\Desktop\AnimalizeMe\Bilder\bowtie.jpg");
            return View("Index", result);
        }

		[HttpPost]  // add creature to database POST
        public IActionResult Add([FromBody]Creature creature)
        {
			var imageList = new List<string>();
			DirectoryInfo images = new DirectoryInfo("Images");//Assuming Test is your Folder
			FileInfo[] Files = images.GetFiles("*.jpg"); //Getting Text files
			string image = "";
			foreach (FileInfo file in Files)
			{
				image = image + ", " + file.Name;
				imageList.Add(image);

			}


			return View("Index", imageList);
			//string[] tags1 = All.Description.tags;
			//creature.CreatureTags.Add(All.Description.tags)
		}



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