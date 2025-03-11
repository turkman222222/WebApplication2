using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    //[Keyless]
    public class Review
    {
        public int ID { get; set; }
        public int RecipeID { get; set; }
        public string ReviewText { get; set; }
        public string Rating { get; set; }

        public Recipe Recipe { get; set; } // Связь с рецептом
    }
}
