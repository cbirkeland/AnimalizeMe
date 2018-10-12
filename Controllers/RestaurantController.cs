using Microsoft.AspNetCore.Mvc;
using AnimalizeMe.Models;
using AnimalizeMe.Services;
using AnimalizeMe.ViewModels;

namespace AnimalizeMe.Controllers
{
    //[Authorize]
    public class RestaurantController : Controller
    {
        private IRestaurantData _restaurantData;
        private IGreeter _greeter;
        private readonly IRestaurantService _restaurantService;
        private readonly IHttpService _httpService;

        public RestaurantController(IRestaurantData restaurantData, IGreeter greeter, IRestaurantService restaurantService, IHttpService httpService)
        {
            _restaurantData = restaurantData;
            _greeter = greeter;
            _restaurantService = restaurantService;
            _httpService = httpService;
        }

        //[AllowAnonymous]
        public IActionResult Index()
        {
            var model = new RestaurantIndexViewModel();
            model.Restaurants = _restaurantData.GetAll();
            model.Hello = _greeter.GetTime();
            model.SomeCountMessage = _restaurantService.CountMessage();
            model.SomethingFromGoogle =  _httpService.Get("http://www.google.com").Result.Substring(0,100);
            return View(model);
        }

        public IActionResult Details(int id)
        {
            var model = _restaurantData.Get(id);
            if(model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpGet]   
        public IActionResult Create()
        {            
            return View();
        }


        public IActionResult Edit(int id)
        {
            var model = _restaurantData.Get(id);
            if (model == null)
            {
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Restaurant restaurant)
        {
            if (ModelState.IsValid)
            {
                _restaurantData.Update(restaurant);
                return RedirectToAction("Details", new { id = restaurant.Id });
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(RestaurantEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var newRestaurant = new Restaurant();
                newRestaurant.Name = model.Name;
                newRestaurant.Cuisine = model.Cuisine;

                newRestaurant = _restaurantData.Add(newRestaurant);

                return RedirectToAction(nameof(Details), new { id = newRestaurant.Id });
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            if (ModelState.IsValid)
            {
                _restaurantData.Remove(id);
                return RedirectToAction("Index");

            }

            return RedirectToAction("Edit");
        }

    }
}
