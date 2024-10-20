namespace RecipeReviewerApp.Models
{
    public class Publisher
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Recipe> Recipes { get; set; }

    }
}
