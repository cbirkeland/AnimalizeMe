using Microsoft.AspNetCore.Mvc;

namespace AnimalizeMe.ViewComponents
{
    public class LoginLogoutViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
