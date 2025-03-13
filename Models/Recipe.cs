using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    //[Keyless]
    public class Recipe
    {
        public int ID { get; set; }
        public string RecipeName { get; set; }
        public string Discription { get; set; }
        public int CategoryID { get; set; }
        public int AuthorID { get; set; }
        public TimeSpan CookingTime { get; set; }
        public byte[] Image { get; set; }

        public Category Category { get; set; } // Связь с категорией
        public Author Author { get; set; } // Связь с автором
        public ICollection<CokingStep> CokingSteps { get; set; } // Связь с шагами приготовления
        public ICollection<RecipeIngredient> RecipeIngredients { get; set; } // Связь с ингредиентами
        public ICollection<Review> Reviews { get; set; } // Связь с отзывами
        public ICollection<RecipeTag> RecipeTags { get; set; } // Связь с тегами
    }
}
