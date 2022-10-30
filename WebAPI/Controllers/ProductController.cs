using DbFirst;
using DbFirst.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        AppDBContext _db;
        public ProductController(AppDBContext db)
        {
            _db = db;

        }

        //GET: /api/product
        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            return _db.Products;
        }
            
        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _db.Products.Find(id);
        }


        //[HttpGet("{id}")]
        //public IActionResult Get(int id)
        //{
        //    var data = _db.Products.Find(id);
        //    return Ok(data);
        //}

        //[HttpGet("{id}")]
        //public ActionResult<Product> Get(int id)
        //{
        //    var data = _db.Products.Find(id);
        //    return Ok(data);
        //}

        //POST: /api/product
        [HttpPost]
        public IActionResult Add(Product model)
        {
           try
            {
                _db.Products.Add(model);
                _db.SaveChanges();
                return CreatedAtAction("Add", model);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
            
        }
        [HttpPut("{id}")]
        public IActionResult Update(int id,Product model)
        {
            try
            {

                if (id != model.ProductId)
                    return BadRequest();

                _db.Products.Update(model);
                _db.SaveChanges();
                return Ok(); //200
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {

                Product model = _db.Products.Find(id);

                if (model != null)
                {
                    _db.Products.Remove(model);
                    _db.SaveChanges();
                    return Ok(); //200
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }

        }
    }
}
