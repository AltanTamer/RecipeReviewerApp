using RecipeReviewerApp.Data;
using RecipeReviewerApp.Interfaces;
using RecipeReviewerApp.Models;
using System.Collections.Immutable;

namespace RecipeReviewerApp.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly DataContext _context;

        public PublisherRepository(DataContext context)
        {
            _context = context;
        }
        public ICollection<Publisher> GetPublishers()
        {
            return _context.Publishers.ToList();
        }

        public Publisher GetPublisher(int id)
        {
            return _context.Publishers.Where(p => p.Id == id).FirstOrDefault();
        }

        public ICollection<Publisher> GetPublisherOfRecipe(int recipeId)
        {
            return _context.Recipes.Where(p => p.Id == recipeId).Select(p => p.Publisher).ToList();
        }

        public ICollection<Recipe> GetRecipeByPublisher(int publisherId)
        {
            return _context.Recipes.Where(p=> p.Publisher.Id == publisherId).ToList();
        }

        public bool PublisherExists(int id)
        {
            return _context.Publishers.Any(r => r.Id == id);
        }

        public bool CreatePublisher(Publisher publisher)
        {
            _context.Add(publisher);
            return Save();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdatePublisher(Publisher publisher)
        {
            _context.Update(publisher);
            return Save();
        }

        public bool DeletePublisher(Publisher publisher)
        {
            _context.Remove(publisher);
            return Save();
        }
    }
}
