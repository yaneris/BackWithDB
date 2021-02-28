using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Back.Data;
using Back.Models;

namespace Back.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {

        private readonly Context _context;

        public ValuesController(Context context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Values>>> GetValues()
        {
            return await _context.Values.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Values>> GetValues_ById(int id)
        {
            var value = await _context.Values.FindAsync(id);
            if(value != null)
            {
                return value;
            }
            else
            {
                return NotFound();
            }
        }
        
        [HttpPost]
        public async Task<ActionResult<Values>> PostValues(Values value)
        {
            _context.Values.Add(value);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetValues", new { id = value.Id }, value);
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<Values>> Delete_values(int id)
        {
            var value = await _context.Values.FindAsync(id);
            if (value == null)
            {
                return NotFound();
            }

            _context.Values.Remove(value);
            await _context.SaveChangesAsync();

            return value;
        }
        
        [HttpPut("{id}")]
        public async Task<IActionResult> Put_Values(int id, Values values)
        {
            if (id != values.Id)
            {
                return BadRequest();
            }

            _context.Entry(values).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ValuesExists(id))
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

        private bool ValuesExists(int id)
        {
            return _context.Values.Any(e => e.Id == id);
        }
    }
}
