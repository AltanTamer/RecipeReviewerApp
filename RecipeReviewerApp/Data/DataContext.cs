using Microsoft.EntityFrameworkCore;
using RecipeReviewerApp.Models;

namespace RecipeReviewerApp.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Publisher> Publishers  { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Review> Reviews { get; set; } 
        public DbSet<Reviewer> Reviewers { get; set; }
    }
}
