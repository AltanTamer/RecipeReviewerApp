using RecipeReviewerApp.Data;
using RecipeReviewerApp.Models;

namespace RecipeReviewerApp
{
    public class Seed
    {
        private readonly DataContext _context;
        public Seed(DataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if (!_context.Categories.Any() && !_context.Publishers.Any() && !_context.Recipes.Any() && !_context.Reviews.Any() && !_context.Reviewers.Any())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Dessert" },
                    new Category { Name = "Main Dish" },
                    new Category { Name = "Appetizer" }
                };

                var publishers = new List<Publisher>
                {
                    new Publisher { FirstName = "John", LastName = "Doe" },
                    new Publisher { FirstName = "Jane", LastName = "Smith" }
                };

                var reviewers = new List<Reviewer>
                {
                    new Reviewer { FirstName = "Alice", LastName = "Johnson" },
                    new Reviewer { FirstName = "Bob", LastName = "Brown" }
                };

                var recipes = new List<Recipe>
                {
                    new Recipe
                    {
                        Name = "Chocolate Cake",
                        HowTo = "Mix ingredients and bake for 30 minutes.",
                        Category = categories[0],
                        Publisher = publishers[0],
                        Reviews = new List<Review>
                        {
                            new Review { Title = "Chocolate Cake", Text = "This cake was amazing!", Rating = 5, Reviewer = reviewers[0] },
                            new Review { Title = "Chocolate Cake", Text = "A bit too sweet for my taste.", Rating = 3, Reviewer = reviewers[1] }
                        }
                    },
                    new Recipe
                    {
                        Name = "Grilled Chicken",
                        HowTo = "Season the chicken and grill for 20 minutes.",
                        Category = categories[1],
                        Publisher = publishers[1],
                        Reviews = new List<Review>
                        {
                            new Review { Title = "Grilled Chicken", Text = "The chicken was juicy and flavorful.", Rating = 4, Reviewer = reviewers[0] },
                            new Review { Title = "Grilled Chicken", Text = "Mine turned out a bit dry.", Rating = 2, Reviewer = reviewers[1] }
                        }
                    },
                    new Recipe
                    {
                        Name = "Bruschetta",
                        HowTo = "Toast the bread and top with tomatoes.",
                        Category = categories[2],
                        Publisher = publishers[0],
                        Reviews = new List<Review>
                        {
                            new Review { Title = "Bruschetta", Text = "A perfect appetizer!", Rating = 5, Reviewer = reviewers[0] },
                            new Review { Title = "Bruschetta", Text = "I added extra garlic.", Rating = 3, Reviewer = reviewers[1] }
                        }
                    }
                };

                _context.Categories.AddRange(categories);
                _context.Publishers.AddRange(publishers);
                _context.Reviewers.AddRange(reviewers);
                _context.Recipes.AddRange(recipes);
                _context.SaveChanges();
            }
        }
    }
}
