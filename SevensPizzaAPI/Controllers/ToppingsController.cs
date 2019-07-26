using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SevensPizzaEntity;

namespace SevensPizzaAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("CorsPolicy")]
    public class ToppingsController : ControllerBase
    {
        private readonly SevensDBContext _context;

        public ToppingsController(SevensDBContext context)
        {
            _context = context;
        }

        // GET: api/Toppings
        [HttpGet]
        public IEnumerable<Topping> GetTopping()
        {
            return _context.Topping;
        }

        // GET: api/Toppings/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTopping([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var topping = await _context.Topping.FindAsync(id);

            if (topping == null)
            {
                return NotFound();
            }

            return Ok(topping);
        }

        // PUT: api/Toppings/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopping([FromRoute] int id, [FromBody] Topping topping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != topping.ToppingID)
            {
                return BadRequest();
            }

            _context.Entry(topping).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ToppingExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Toppings
        [HttpPost]
        public async Task<IActionResult> PostTopping([FromBody] Topping topping)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Topping.Add(topping);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopping", new { id = topping.ToppingID }, topping);
        }

        // DELETE: api/Toppings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTopping([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var topping = await _context.Topping.FindAsync(id);
            if (topping == null)
            {
                return NotFound();
            }

            _context.Topping.Remove(topping);
            await _context.SaveChangesAsync();

            return Ok(topping);
        }

        private bool ToppingExists(int id)
        {
            return _context.Topping.Any(e => e.ToppingID == id);
        }
    }
}