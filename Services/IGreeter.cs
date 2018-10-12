using System;
using Microsoft.Extensions.Configuration;

namespace AnimalizeMe.Services
{
    public interface IGreeter
    {
        string GetMessageOfTheDay();
        string GetTime();
    }

    public class Greeter : IGreeter
    {
        private IConfiguration _configuration;
        private DateTime _now;

        // Dependency Injection av "IConfiguration" för att kunna komma åt konfigurering (t.ex värden från appsettings.json)

        public Greeter(IConfiguration configuration) 
        {
            _configuration = configuration;
            _now = DateTime.Now;
        }

        public string GetMessageOfTheDay()
        {
            return _configuration["Greeting"];
        }

        public string GetTime()
        {
            return "Klockan " + _now + " (vid anropet " + DateTime.Now + ")";
        }
    }
}