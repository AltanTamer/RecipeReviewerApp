using RecipeReviewerApp.Models;

namespace RecipeReviewerApp.Interfaces
{
    public interface IPublisherRepository
    {
        ICollection<Publisher> GetPublishers();
        Publisher GetPublisher(int id);
        ICollection<Publisher> GetPublisherOfRecipe(int recipeId);
        ICollection<Recipe> GetRecipeByPublisher(int publisherId);
        bool PublisherExists(int id);
        bool CreatePublisher(Publisher publisher);
        bool UpdatePublisher(Publisher publisher);
        bool DeletePublisher(Publisher publisher);
        bool Save();

    }
}
