using RecipeReviewerApp.Models;

namespace RecipeReviewerApp.Interfaces
{
    public interface IRecipeRepository
    {
        ICollection<Recipe> GetRecipes();
        Recipe GetRecipe(int id);
        Recipe GetRecipe(string name);
        decimal GetRecipeRating(int recipeId);
        bool RecipeExists(int recipeId);
        bool CreateRecipe(int publisherId, int categoryId, Recipe recipe);
        bool UpdateRecipe(int publisherId, int categoryId, Recipe recipe);
        bool DeleteRecipe(Recipe recipe);
        bool Save();
    }
}
