using RecipeReviewerApp.Models;

namespace RecipeReviewerApp.Interfaces
{
    public interface IReviewerRepository
    {
        ICollection<Reviewer> GetReviewers();
        Reviewer GetReviewer(int reviewerId); 
        ICollection<Review> GetReviewsByReviewer(int reviewerId);
        ICollection<Reviewer> GetReviewerFromReview(int reviewId);
        bool ReviewerExists(int reviewerId);  
        bool CreateReviewer(Reviewer reviewer);
        bool UpdateReviewer(Reviewer reviewer);
        bool DeleteReviewer(Reviewer reviewer);
        bool Save();
    }
}
