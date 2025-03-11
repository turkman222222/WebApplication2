using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{

    //[Keyless]
    public class Ingredient
    {
        public int ID { get; set; }
        public string IngredientName { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } // Связь с рецептами
    }
}
