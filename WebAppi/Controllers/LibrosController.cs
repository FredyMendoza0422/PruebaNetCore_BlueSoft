using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PruebaNetCore;

namespace WebAppi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibrosController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<AutoresController> logger;

        public LibrosController(DataContext context, ILogger<AutoresController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: api/Libros
        [HttpGet]
        public IEnumerable<Libro> GetLibro()
        {
            return _context.Libro;
        }

        // GET: api/Libros/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLibro([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("ERR: Modelo no valido en GET: api/Libros");
                return BadRequest(ModelState);
            }

            var libro = await _context.Libro.FindAsync(id);

            if (libro == null)
            {
                logger.LogInformation("INF: Libro no encontrado");
                return NotFound();
            }

            return Ok(libro);
        }

        // PUT: api/Libros/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLibro([FromRoute] int id, [FromBody] Libro libro)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("ERR: Modelo no valido en PUT: api/Libros");
                return BadRequest(ModelState);
            }

            if (id != libro.IdLibro)
            {
                logger.LogError("ERR: Datos no consistentes");
                return BadRequest();
            }

            _context.Entry(libro).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibroExists(id))
                {
                    logger.LogInformation("INF: Lirbo con ID: " + id + " no encontrado");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Libros
        [HttpPost]
        public async Task<IActionResult> PostLibro([FromBody] Libro libro)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("ERR: Modelo no valido en POST: api/Libros");
                return BadRequest(ModelState);
            }

            _context.Libro.Add(libro);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibro", new { id = libro.IdLibro }, libro);
        }

        // DELETE: api/Libros/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLibro([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("ERR: Modelo no valido en DELETE: api/Libros");
                return BadRequest(ModelState);
            }

            var libro = await _context.Libro.FindAsync(id);
            if (libro == null)
            {
                logger.LogInformation("INF: Lirbo con ID: " + id + " no encontrado");
                return NotFound();
            }

            _context.Libro.Remove(libro);
            await _context.SaveChangesAsync();

            return Ok(libro);
        }

        private bool LibroExists(int id)
        {
            return _context.Libro.Any(e => e.IdLibro == id);
        }
    }
}