using RecipeReviewerApp.Models;

namespace RecipeReviewerApp.Interfaces
{
    public interface IReviewRepository
    {
        ICollection<Review> GetReviews();
        Review GetReview(int reviewId);
        ICollection<Review> GetReviewsOfRecipe(int recipeId);
        bool ReviewExists(int reviewId);
        bool CreateReview(int recipeId, int reviewerId, Review review);
        bool DeleteReview(Review review);
        bool DeleteReviews(List<Review> reviews);
        bool Save();    
    }
}
