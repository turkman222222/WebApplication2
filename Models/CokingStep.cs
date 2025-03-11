using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Models
{
    //[Keyless]
    public class CokingStep
    {
        public int ID{ get; set; }
        public int ReceptID { get; set; }
        public int StepNumber { get; set; }
        public string StepDiscription { get; set; }

        public Recipe Recipe { get; set; } // Связь с рецептом
    }
}
