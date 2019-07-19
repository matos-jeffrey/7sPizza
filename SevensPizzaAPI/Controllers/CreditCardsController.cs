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
    public class CreditCardsController : ControllerBase
    {
        private readonly SevensDBContext _context;

        public CreditCardsController(SevensDBContext context)
        {
            _context = context;
        }

        // GET: api/CreditCards
        [HttpGet]
        public IEnumerable<CreditCard> GetCreditCard()
        {
            return _context.CreditCard;
        }

        // GET: api/CreditCards/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCreditCard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var creditCard = await _context.CreditCard.FindAsync(id);

            if (creditCard == null)
            {
                return NotFound();
            }

            return Ok(creditCard);
        }

        // PUT: api/CreditCards/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCreditCard([FromRoute] int id, [FromBody] CreditCard creditCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != creditCard.CardID)
            {
                return BadRequest();
            }

            _context.Entry(creditCard).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CreditCardExists(id))
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

        // POST: api/CreditCards
        [HttpPost]
        public async Task<IActionResult> PostCreditCard([FromBody] CreditCard creditCard)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.CreditCard.Add(creditCard);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCreditCard", new { id = creditCard.CardID }, creditCard);
        }

        // DELETE: api/CreditCards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCreditCard([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var creditCard = await _context.CreditCard.FindAsync(id);
            if (creditCard == null)
            {
                return NotFound();
            }

            _context.CreditCard.Remove(creditCard);
            await _context.SaveChangesAsync();

            return Ok(creditCard);
        }

        private bool CreditCardExists(int id)
        {
            return _context.CreditCard.Any(e => e.CardID == id);
        }
    }
}