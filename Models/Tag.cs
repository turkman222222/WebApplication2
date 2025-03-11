using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    //[Keyless]
    public class Tag
    {
        public int Id { get; set; }
        public string? Tegname { get; set; }

        public ICollection<RecipeTag>? RecipeTags { get; set; } // Связь с рецептами
    }
}
