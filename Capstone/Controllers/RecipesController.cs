using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Capstone.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstone.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class RecipesController : ControllerBase
  {
    private readonly RecipesContext _context;

    public RecipesController(RecipesContext context)
    {
      _context = context;
      _context.Database.EnsureCreated();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Recipes>>> GetTodoChores()
    {
      return await _context.Recipe.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Recipes>> GetRecipe(int id)
    {
      var recipe = await _context.Recipe.FindAsync(id);

      if (recipe == null)
      {
        return NotFound();
      }

      return recipe;
    }

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutRecipe(int id, Recipes recipe)
    {
      if (id != recipe.Id)
      {
        return BadRequest();
      }

      _context.Entry(recipe).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!RecipeExists(id))
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

    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Recipes>> PostRecipe(Recipes recipe)
    {
      _context.Recipe.Add(recipe);
      await _context.SaveChangesAsync();

      return CreatedAtAction(nameof(GetRecipe), new { id = recipe.Id }, recipe);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
      var todoChore = await _context.Recipe.FindAsync(id);
      if (todoChore == null)
      {
        return NotFound();
      }

      _context.Recipe.Remove(todoChore);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool RecipeExists(int id)
    {
      return _context.Recipe.Any(e => e.Id == id);
    }
  }
}
