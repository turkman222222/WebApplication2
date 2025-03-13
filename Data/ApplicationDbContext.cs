using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        { }
        



        public DbSet<Author> IDAuthor { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<CokingStep> CokingSteps { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<RecipeTag> RecipeTags { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    // Настройка отношений между таблицами
        //    modelBuilder.Entity<Recipe>()
        //        .HasOne(r => r.Author)
        //        .WithMany(a => a.Recipes)
        //        .HasForeignKey(r => r.AuthorID);

        //    modelBuilder.Entity<Recipe>()
        //        .HasOne(r => r.Category)
        //        .WithMany(c => c.Recipes)
        //        .HasForeignKey(r => r.CategoryID);

        //    modelBuilder.Entity<RecipeIngredient>()
        //        .HasKey(ri => new { ri.RecipeID, ri.IngredientID });

        //    modelBuilder.Entity<RecipeIngredient>()
        //        .HasOne(ri => ri.Recipe)
        //        .WithMany(r => r.RecipeIngredients)
        //        .HasForeignKey(ri => ri.RecipeID);

        //    modelBuilder.Entity<RecipeIngredient>()
        //        .HasOne(ri => ri.Ingredient)
        //        .WithMany(i => i.RecipeIngredients)
        //        .HasForeignKey(ri => ri.IngredientID);

        //    modelBuilder.Entity<CokingStep>()
        //        .HasOne(cs => cs.Recipe)
        //        .WithMany(r => r.CokingSteps)
        //        .HasForeignKey(cs => cs.ID);

        //    modelBuilder.Entity<Review>()
        //        .HasOne(r => r.Recipe)
        //        .WithMany(r => r.Reviews)
        //        .HasForeignKey(r => r.RecipeID);

        //    modelBuilder.Entity<RecipeTag>()
        //        .HasKey(rt => new { rt.ID, rt.TagId });

        //    modelBuilder.Entity<RecipeTag>()
        //        .HasOne(rt => rt.Recipe)
        //        .WithMany(r => r.RecipeTags)
        //        .HasForeignKey(rt => rt.ID);

        //    modelBuilder.Entity<RecipeTag>()
        //        .HasOne(rt => rt.Teg)
        //        .WithMany(t => t.RecipeTags)
        //        .HasForeignKey(rt => rt.TagId);

            // Добавьте дополнительные настройки, если необходимо
        //}
    }
}

