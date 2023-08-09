using DAL;
using Microsoft.AspNetCore.Mvc;

namespace Lab_CascadingDropDown.Controllers
{
    public class CascadingController : Controller
    {
        AppDBContext _appDBContext;
        public CascadingController(AppDBContext appDBContext)
        {
                _appDBContext = appDBContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Country() 
        {
            var co=_appDBContext.Countries.ToList();
            return new JsonResult(co);

        }

        public JsonResult City(int id) 
        { 
            var ci = _appDBContext.Cities.Where(e => e.Country.CountryId == id).ToList(); 
            return new JsonResult(ci);
        }
    }
}
