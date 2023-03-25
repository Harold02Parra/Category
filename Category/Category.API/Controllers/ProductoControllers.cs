using Category.API.Data;
using Category.Shared.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Category.API.Data;
using Category.Shared.Entities;
using System.Diagnostics.Metrics;

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

        [HttpGet("{id:int}")]  ///api/productos/1
        public async Task<ActionResult> Get(int id)
        {
            var producto = await _context.Productos.FirstOrDefaultAsync(x => x.Id == id);
            if (producto is null)
            {
                return NotFound();
            }

            return Ok(producto);
        }

        // Actualización

        [HttpPut]
        public async Task<ActionResult> Put(Producto producto)
        {

            try
            {

                _context.Update(producto);
                await _context.SaveChangesAsync();


                return Ok(producto);

            }
            catch (DbUpdateException dbUpdateException)
            {
                if (dbUpdateException.InnerException!.Message.Contains("duplicate"))
                {
                    return BadRequest("Ya existe un registro con el mismo nombre.");
                }
                else
                {
                    return BadRequest(dbUpdateException.InnerException.Message);
                }
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }



        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var afectedRows = await _context.Productos
                .Where(x => x.Id == id)

                .ExecuteDeleteAsync();

            if (afectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }




    }
}
