using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    //[Keyless]
    public class RecipeIngredient
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public int IngredientID { get; set; }
        public int Quantity { get; set; }

        public Recipe Recipe { get; set; } // Связь с рецептом
        public Ingredient Ingredient { get; set; } // Связь с ингредиентом
    }
}
