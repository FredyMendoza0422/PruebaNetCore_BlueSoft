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
    public class AutoresController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ILogger<AutoresController> logger;
        public AutoresController(DataContext context, ILogger<AutoresController> logger)
        {
            _context = context;
            this.logger = logger;
        }

        // GET: api/Autores
        [HttpGet]
        public IEnumerable<Autor> GetAutor()
        {
            return _context.Autor;
        }

        // GET: api/Autores/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAutor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("ERR: Modelo no valido en GET: api/Autores");
                return BadRequest(ModelState);
            }

            var autor = await _context.Autor.FindAsync(id);

            if (autor == null)
            {
                logger.LogInformation("INF: Autor no encontrado");
                return NotFound();
            }

            return Ok(autor);
        }

        // PUT: api/Autores/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAutor([FromRoute] int id, [FromBody] Autor autor)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("ERR: Modelo no valido en PUT: api/Autores");
                return BadRequest(ModelState);
            }

            if (id != autor.IdAutor)
            {
                logger.LogError("ERR: Datos no consistentes");
                return BadRequest();
            }

            _context.Entry(autor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AutorExists(id))
                {
                    logger.LogInformation("INF: Autor con ID: "+ id +" no encontrado");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Autores
        [HttpPost]
        public async Task<IActionResult> PostAutor([FromBody] Autor autor)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("ERR: Modelo no valido en POST: api/Autores");
                return BadRequest(ModelState);
            }

            _context.Autor.Add(autor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAutor", new { id = autor.IdAutor }, autor);
        }

        // DELETE: api/Autores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAutor([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                logger.LogError("ERR: Modelo no valido en DELETE: api/Autores");
                return BadRequest(ModelState);
            }

            var autor = await _context.Autor.FindAsync(id);
            if (autor == null)
            {
                logger.LogInformation("INF: Autor con ID: " + id + " no encontrado");
                return NotFound();
            }

            _context.Autor.Remove(autor);
            await _context.SaveChangesAsync();

            return Ok(autor);
        }

        private bool AutorExists(int id)
        {
            return _context.Autor.Any(e => e.IdAutor == id);
        }
    }
}