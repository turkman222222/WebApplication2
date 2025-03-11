using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    //[Keyless]
    public class Category
    {
        public int ID { get; set; }
        public string CategoryName { get; set; }

        public ICollection<Recipe> Recipes { get; set; } // Связь с рецептами
    }
}
