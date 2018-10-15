using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AnimalizeMe.Services;
using Microsoft.AspNetCore.Mvc;

namespace AnimalizeMe.Controllers
{
    public class AnimalController : Controller
    {
		AnimalService _animalService;
		public AnimalController()
		{
            _animalService = new AnimalService(); // "277e278c4d8c4a9402a60952299fe6cb", "9a5dff4f681b2b1c01f6019a2ad112469aec3af3eed58699cc7dc30a18ab6da3");


		}

        public async Task<IActionResult> Index()
        {

            var result = await _animalService.MakeAnalysisRequest(@"C:\Users\Administrator\Desktop\AnimalizeMe\Bilder\bowtie.jpg");
            return View("Index", result);
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