using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    
    public class Author
    {
        public int ID { get; set; }
        public string? Authorname { get; set; }
       
      

        public ICollection<Recipe>? Recipes { get; set; } // Связь с рецептами
    }
}
