namespace RecipeReviewerApp.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string HowTo { get; set; }
        public ICollection<Review> Reviews { get; set; }
        public Publisher Publisher { get; set; }
        public Category Category { get; set; }


    }

}
