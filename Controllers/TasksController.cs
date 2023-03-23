using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todo_List.Data;
using Todo_List.DTOs;

namespace Todo_List.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly Todo_ListContext _context;

        public TasksController(Todo_ListContext context)
        {
            _context = context;
        }

        // GET: api/Tasks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Models.Task>>> GetTask()
        {
            if (_context.Task == null)
            {
                return NotFound();
            }
            return await _context.Task.ToListAsync();
        }

        // GET: api/Tasks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Task>> GetTask(int id)
        {
            if (_context.Task == null)
            {
                return NotFound();
            }
            var task = await _context.Task.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            return task;
        }

        // PUT: api/Tasks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, TaskDTO taskDTO)
        {
            if (id != taskDTO.Id)
            {
                return BadRequest();
            }

            var task = await _context.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            task.Name = taskDTO.Name;
            task.DueDate = taskDTO.DueDate;
            task.Description = taskDTO.Description;
            task.Note = taskDTO.Note;
            task.Id = taskDTO.Id;
            task.ListId = taskDTO.ListId;
            task.Priority = taskDTO.Priority;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!TaskExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Tasks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Models.Task>> PostTask(TaskDTO taskDTO)
        {
            if (_context.Task == null)
            {
                return Problem("Entity set 'Todo_ListContext.Task'  is null.");
            }

            var list = await _context.List.FindAsync(taskDTO.ListId);

            if (list == null)
            {
                return NotFound();
            }

            var task = DtoToTask(taskDTO);

            _context.Task.Add(task);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTask", new { id = task.Id }, task);
        }

        // DELETE: api/Tasks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(int id)
        {
            if (_context.Task == null)
            {
                return NotFound();
            }
            var task = await _context.Task.FindAsync(id);
            if (task == null)
            {
                return NotFound();
            }

            _context.Task.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TaskExists(int id)
        {
            return (_context.Task?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private Models.Task DtoToTask(TaskDTO taskDTO)
        {
            return new Models.Task()
            {
                Description = taskDTO.Description,
                Name = taskDTO.Name,
                Note = taskDTO.Note,
                Priority = taskDTO.Priority,
                DueDate = taskDTO.DueDate,
                ListId = taskDTO.ListId
            };
        }
    }
}
