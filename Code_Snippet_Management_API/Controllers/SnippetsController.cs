using Code_Snippet_Management_API.Data;
using Code_Snippet_Management_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SnippetsController : ControllerBase
    {
        private readonly ApiContext _context;

        public SnippetsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CodeSnippet>>> GetSnippets()
        {
            return await _context.Snippets.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CodeSnippet>> GetSnippet(int id)
        {
            var snippet = await _context.Snippets.FindAsync(id);

            if (snippet == null)
            {
                return NotFound();
            }

            return snippet;
        }

        [HttpPost]
        public async Task<ActionResult<CodeSnippet>> PostSnippet(CodeSnippet snippet)
        {
            _context.Snippets.Add(snippet);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSnippet), new { id = snippet.Id }, snippet);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSnippet(int id, CodeSnippet snippet)
        {
            if (id != snippet.Id)
            {
                return BadRequest();
            }

            _context.Entry(snippet).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SnippetExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSnippet(int id)
        {
            var snippet = await _context.Snippets.FindAsync(id);
            if (snippet == null)
            {
                return NotFound();
            }

            _context.Snippets.Remove(snippet);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SnippetExists(int id)
        {
            return _context.Snippets.Any(e => e.Id == id);
        }
    }
}
