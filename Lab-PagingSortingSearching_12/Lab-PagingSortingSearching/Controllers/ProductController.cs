using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab_PagingSortingSearching.Controllers
{
    [Route("api/[Controller]")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly AppDBContext _appDBContext;
        public ProductController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        [HttpPost]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts() 
        {
            if(_appDBContext.Products == null) 
            {
                return Problem("Entities set are empty:AppDBContext");
            }
            return await _appDBContext.Products.ToListAsync();

        }
        [HttpGet]
        public async Task<ActionResult<Product>> GetProducts(string SearchText = "") 
        {
            var query=_appDBContext.Products.AsQueryable();

            if (!string.IsNullOrEmpty(SearchText)) 
            {
                query=_appDBContext.Products.Where(p=>p.Name.Contains(SearchText)).AsQueryable();

            }

            return  Ok(query.ToListAsync());
        }

        
    }
}
