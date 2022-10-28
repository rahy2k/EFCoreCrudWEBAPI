using DbFirst;
using DbFirst.Entities;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        AppDBContext _db;
        public CategoryController(AppDBContext db)
        {
            _db = db;
        }

        //GET: /api/product/getall
        [HttpGet]
        public IEnumerable<Category> GetAll()
        {
            return _db.Categories; //200 OK
        }
    }
}
