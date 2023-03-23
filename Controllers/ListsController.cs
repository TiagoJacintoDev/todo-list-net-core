using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo_List.Data;
using Todo_List.DTOs;
using Todo_List.Models;

namespace Todo_List.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListsController : ControllerBase
    {
        private readonly Todo_ListContext _context;

        public ListsController(Todo_ListContext context)
        {
            _context = context;
        }

        // GET: api/Lists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<List>>> GetList()
        {
            if (_context.List == null)
            {
                return NotFound();
            }
            return await _context.List.Include(list => list.Tasks).ToListAsync();
        }

        // GET: api/Lists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<List>> GetList(int id)
        {
            if (_context.List == null)
            {
                return NotFound();
            }

            var list = await _context.List.FindAsync(id);

            if (list == null)
            {
                return NotFound();
            }

            var tasks = await _context.Task.Where(task => task.ListId == list.Id).ToListAsync();

            list.Tasks = tasks;

            return list;
        }

        // PUT: api/Lists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutList(int id, List list)
        {
            if (id != list.Id)
            {
                return BadRequest();
            }

            _context.Entry(list).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ListExists(id))
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

        // POST: api/Lists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List>> PostList(ListDTO listDTO)
        {
            if (_context.List == null)
            {
                return Problem("Entity set 'Todo_ListContext.List'  is null.");
            }

            var list = new List()
            {
                Name = listDTO.Name
            };

            _context.List.Add(list);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetList", new { id = list.Id }, list);
        }

        // DELETE: api/Lists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteList(int id)
        {
            if (_context.List == null)
            {
                return NotFound();
            }
            var list = await _context.List.FindAsync(id);
            if (list == null)
            {
                return NotFound();
            }

            _context.List.Remove(list);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ListExists(int id)
        {
            return (_context.List?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
