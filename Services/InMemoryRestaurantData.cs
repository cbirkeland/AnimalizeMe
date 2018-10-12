using System.Collections.Generic;
using System.Linq;
using AnimalizeMe.Models;

namespace AnimalizeMe.Services
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        static List<Restaurant> _restaurants = new List<Restaurant>();

        public IEnumerable<Restaurant> GetAll()
        {
            return _restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Get(int id)
        {
            return _restaurants.FirstOrDefault(r => r.Id == id);
        }

        public Restaurant Add(Restaurant restaurant)
        {
            if (_restaurants.Any())
                restaurant.Id = _restaurants.Max(r => r.Id) + 1;
            else
                restaurant.Id = 1;

            _restaurants.Add(restaurant);
            return restaurant;
        }

        public Restaurant Update(Restaurant restaurant)
        {
            var r = _restaurants.Single(x => x.Id == restaurant.Id);
            r.Cuisine = restaurant.Cuisine;
            r.Name = restaurant.Name;
            return r;
        }

        public void Remove(int id)
        {
            _restaurants = _restaurants.Where(x => x.Id != id).ToList();
        }


    }
}
