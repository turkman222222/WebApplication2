using Azure;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    //[Keyless]
    public class RecipeTag
    {
        public int ID { get; set; }
        public int PecipeID { get; set; }
        public int TagId { get; set; }

        public Recipe Recipe { get; set; } // Связь с рецептом
        public Tag Teg { get; set; } // Связь с тегом
    }
}
