using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace Capstone.Models
{
  public class RecipesContext : DbContext
  {
    public RecipesContext(DbContextOptions<RecipesContext> options)
      : base(options)
    {
    }

    public DbSet<Recipes> Recipe { get; set; } = null!;
  }
}
