using DAL.Entities;
using DAL.Respositories;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace WebAPPUI.Controllers
{
    public class ProductController : Controller
    {
        IConfiguration _configuration;
        CategoryRespository _categoryRespository;
        ProductRespository _productRespository;

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
            _productRespository = new ProductRespository(_configuration.GetConnectionString("DbConnection"));
            _categoryRespository = new CategoryRespository(_configuration.GetConnectionString("DbConnection"));
        }

        public IActionResult Index()
        {
            var products=_productRespository.GetProducts();

            return View(products);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = _categoryRespository.GetCategories();
            return View();
        }
        [HttpPost]
        public IActionResult Create(Product model)
        {
            ModelState.Remove("ProductId");
            if (ModelState.IsValid)
            {
                _productRespository.AddProducts(model); 
                return RedirectToAction("Index"); // product Home page
            }
            ViewBag.Categories = _categoryRespository.GetCategories(); 
            return View(); 
        }


        public IActionResult Edit(int id)
        {

            ViewBag.Categories = _categoryRespository.GetCategories(); 
            Product model = _productRespository.GetProductById(id); 
            return View("Create", model); //Call Create View and pass model  
        }

        [HttpPost]
        public IActionResult Edit(Product model) 
        {
            if (ModelState.IsValid)
            {
                _productRespository.UpdateProduct(model); 
                return RedirectToAction("Index"); // product Home page
            } 
            ViewBag.Categories = _categoryRespository.GetCategories(); 
            return View(); 
        }

        public IActionResult Delete(int id)
        {
            _productRespository.DeleteProduct(id);
            return RedirectToAction("Index"); // Go to the Listing Page 
        }

        }
}
