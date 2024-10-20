using AutoMapper;
using RecipeReviewerApp.Data;
using RecipeReviewerApp.Interfaces;
using RecipeReviewerApp.Models;

namespace RecipeReviewerApp.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateReview(int recipeId, int reviewerId, Review review)
        {
            var recipe = _context.Recipes.Where(a => a.Id == recipeId).FirstOrDefault();
            var reviewer = _context.Reviewers.Where(a => a.Id == reviewerId).FirstOrDefault();

            review.Reviewer = reviewer;
            review.Recipe = recipe;
            _context.Add(review);
            return Save();
        }

        public bool DeleteReview(Review review)
        {
            _context.Remove(review);
            return Save();
        }

        public bool DeleteReviews(List<Review> reviews)
        {
            _context.RemoveRange(reviews);
            return Save();
        }

        public Review GetReview(int reviewId)
        {
            return _context.Reviews.Where(r=>r.Id  == reviewId).FirstOrDefault();  
        }

        public ICollection<Review> GetReviews()
        {
            return _context.Reviews.ToList();
        }

        public ICollection<Review> GetReviewsOfRecipe(int recipeId)
        {
            return _context.Reviews.Where(r=>r.Recipe.Id == recipeId).ToList();
        }

        public bool ReviewExists(int reviewId)
        {
            return _context.Reviews.Any(r=>r.Id==reviewId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
