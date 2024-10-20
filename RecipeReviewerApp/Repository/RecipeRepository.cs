using RecipeReviewerApp.Data;
using RecipeReviewerApp.Interfaces;
using RecipeReviewerApp.Models;

namespace RecipeReviewerApp.Repository
{
    public class RecipeRepository : IRecipeRepository
    {
        private readonly DataContext _context;
        public RecipeRepository(DataContext context) 
        {
            _context = context;
        }
        public bool RecipeExists(int recipeId)
        {
            return _context.Recipes.Any(r => r.Id == recipeId);
        }
        public Recipe GetRecipe(int id)
        {
            return _context.Recipes.Where(r =>r.Id == id).FirstOrDefault();
        }

        public Recipe GetRecipe(string name)
        {
            return _context.Recipes.Where(r =>r.Name == name).FirstOrDefault();
        }

        public decimal GetRecipeRating(int recipeId)
        {
            var review = _context.Reviews.Where(r => r.Recipe.Id == recipeId);
            if (review.Count() <= 0)
                return 0;
            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public ICollection<Recipe> GetRecipes() 
        {
            return _context.Recipes.OrderBy(r=> r.Id).ToList();
        }

        public bool CreateRecipe(int publisherId, int categoryId, Recipe recipe)
        {
            var publisher = _context.Publishers.Where(a =>a.Id == publisherId).FirstOrDefault();
            var category = _context.Categories.Where(a=>a.Id == categoryId).FirstOrDefault();

            recipe.Publisher = publisher;
            recipe.Category = category;

            _context.Recipes.Add(recipe);

            return Save();  

        }

        public bool Save()
        {
            var saved= _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateRecipe(int publisherId, int categoryId, Recipe recipe)
        {
            var publisher = _context.Publishers.FirstOrDefault(p => p.Id == publisherId);
            var category = _context.Categories.FirstOrDefault(c => c.Id == categoryId);

            recipe.Publisher = publisher;
            recipe.Category = category;

            _context.Update(recipe);
            return Save();
        }

        public bool DeleteRecipe(Recipe recipe)
        {
            _context.Remove(recipe);
            return Save();
        }
    }
}
