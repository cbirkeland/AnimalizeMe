using AnimalizeMe.Models;
using System.Collections.Generic;

namespace AnimalizeMe.Services
{
    public interface IRestaurantData
    {
        IEnumerable<Restaurant> GetAll();
        Restaurant Get(int id);
        Restaurant Add(Restaurant restaurant);
        Restaurant Update(Restaurant restaurant);
        void Remove(int id);
    }
}
