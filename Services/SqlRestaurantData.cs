using System.Collections.Generic;
using System.Linq;
using AnimalizeMe.Models;
using AnimalizeMe.Data;
using Microsoft.EntityFrameworkCore;

namespace AnimalizeMe.Services
{
    public class SqlRestaurantData : IRestaurantData
    {
        private AnimalizeMeDbContext _context;

        public SqlRestaurantData(AnimalizeMeDbContext context)
        {
            _context = context;
        }

        public Restaurant Add(Restaurant restaurant)
        {
            _context.Restaurants.Add(restaurant);
            _context.SaveChanges();
            return restaurant;
        }

        public Restaurant Get(int id)
        {
            return _context.Restaurants.FirstOrDefault(r => r.Id == id);
        }

        public IEnumerable<Restaurant> GetAll()
        {
            return _context.Restaurants.OrderBy(r => r.Name);
        }

        public Restaurant Update(Restaurant restaurant)
        {
            _context.Attach(restaurant).State = 
                EntityState.Modified;
            _context.SaveChanges();
            return restaurant;
        }

        public void Remove(int id)
        {
            var r = _context.Restaurants.First(x => x.Id == id);
            _context.Restaurants.Remove(r);
            _context.SaveChanges();
        }
    }
}
