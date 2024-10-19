using Microsoft.AspNetCore.Mvc;

namespace MEAlumniAssociationDUET.Web.Controllers
{
    public class AdminDashboard : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
