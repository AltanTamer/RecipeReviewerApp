using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RecipeReviewerApp.Data;
using RecipeReviewerApp.Interfaces;
using RecipeReviewerApp.Models;

namespace RecipeReviewerApp.Repository
{
    public class ReviewerRepository : IReviewerRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public ReviewerRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CreateReviewer(Reviewer reviewer)
        {
            _context.Add(reviewer);
            return Save();
        }

        public bool DeleteReviewer(Reviewer reviewer)
        {
            _context.Remove(reviewer);
            return Save();
        }

        public Reviewer GetReviewer(int reviewerId)
        {
            return _context.Reviewers.Where(r=>r.Id==reviewerId).Include(e=>e.Reviews).FirstOrDefault();
        }

        public ICollection<Reviewer> GetReviewerFromReview(int reviewId)
        {
            return _context.Reviews.Where(r=>r.Id==reviewId).Select(r => r.Reviewer).ToList();
        }

        public ICollection<Reviewer> GetReviewers()
        {
            return _context.Reviewers.ToList();
        }

        public ICollection<Review> GetReviewsByReviewer(int reviewerId)
        {
            return _context.Reviews.Where(r=>r.Reviewer.Id==reviewerId).ToList();
        }

        public bool ReviewerExists(int reviewerId)
        {
            return _context.Reviews.Any(r =>r.Id==reviewerId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReviewer(Reviewer reviewer)
        {
            _context.Update(reviewer);
            return Save();
        }
    }
}
