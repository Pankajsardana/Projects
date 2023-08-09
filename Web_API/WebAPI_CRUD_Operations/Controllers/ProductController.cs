using DAL;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI_CRUD_Operations.Controllers
{

    [Route("api/[controller]")]
    public class ProductController : Controller
    {
        private readonly AppDBContext _appDBContext;
        public ProductController(AppDBContext appDBContext)
        {
            _appDBContext = appDBContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            if (_appDBContext.Products == null)
            {
                return NotFound();
            }
            return await _appDBContext.Products.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByID(int id) 
        {
            if (_appDBContext == null) 
            {
                return NotFound();
            }
            var product=await _appDBContext.Products.FindAsync(id);
            if (product == null) 
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost("SaveProduct")]
        public async Task<ActionResult<Product>> SaveProduct(Product model) 
        {
            if (_appDBContext == null) 
            {
                return Problem("Entity Set is blank");
            }
            if (model.ProductId != null) 
            {
                return Problem("Product ID should be kept blank as it is auto generated");
            }
            _appDBContext.Products.Add(model);
            await _appDBContext.SaveChangesAsync();
            return CreatedAtAction("GetProducts", new { id = model.ProductId }, model);

        }
        [HttpPost("UpdateProduct")]
        public async Task<ActionResult> UpdateProduct(Product model) 
        {
            _appDBContext.Entry(model).State = EntityState.Modified;
            try 
            {
                await _appDBContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) 
            {
                if (!ProductExists(model.ProductId))
                {
                    return NotFound();
                }
                else { throw; }
            }
            return NoContent();          

        }

        private bool ProductExists(int productId)
        {
            return (_appDBContext.Products?.Any(id => id.ProductId == productId)).GetValueOrDefault();
        }

        [HttpDelete("{Id}")]
        public async Task<ActionResult> DeleteProduct(int Id) 
        {
            if(_appDBContext.Products == null) 
            {
                return Problem("Entity Set is Blank");
            }
            var product=await _appDBContext.Products.FindAsync(Id);
            if(product == null) 
            {
                return NotFound();
            }
            _appDBContext.Products.Remove(product);
             await _appDBContext.SaveChangesAsync();
            return NoContent();
        }

       
    }
}
