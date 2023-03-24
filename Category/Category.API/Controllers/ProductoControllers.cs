using Category.API.Data;
using Category.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Category.API.Data;
using Category.Shared.Entities;

namespace Categorys.API.Controllers
{
    [ApiController]
    [Route("/api/productos")]
    public class ProductosController : ControllerBase
    {
        private readonly DataContext _context;

        public ProductosController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Productos.ToListAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post(Producto producto)
        {
            _context.Add(producto);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
