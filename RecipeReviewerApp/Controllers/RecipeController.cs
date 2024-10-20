using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RecipeReviewerApp.Dto;
using RecipeReviewerApp.Interfaces;
using RecipeReviewerApp.Models;
using RecipeReviewerApp.Repository;

namespace RecipeReviewerApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : Controller
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public RecipeController(IRecipeRepository recipeRepository,IReviewRepository reviewRepository, IMapper mapper)
        {
            _recipeRepository = recipeRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        public IActionResult GetRecipes()
        {
            var recipes = _mapper.Map<List<RecipeDto>>(_recipeRepository.GetRecipes());
            //var recipe = _recipeRepository.GetRecipes();
            //var recipeDto = new RecipeDto()
            //{
            //    Name = recipe.FirstOrDefault().Name
            //};
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(recipes);
        }
        [HttpGet("{recipeId}")]
        [ProducesResponseType(200, Type = typeof(Recipe))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipe(int recipeId) 
        {
            if(!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var recipe = _mapper.Map<RecipeDto>(_recipeRepository.GetRecipe(recipeId));
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(recipe);
        }
        [HttpGet("{recipeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult getRecipeRating(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();
            var rating = _recipeRepository.GetRecipeRating(recipeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateRecipe([FromQuery] int publisherId, [FromQuery] int categoryId,[FromBody] RecipeDto recipeCreate)
        {
            if (recipeCreate == null)
                return BadRequest(ModelState);

            var recipe = _recipeRepository.GetRecipes()
                .Where(c => c.Name.Trim().ToUpper() == recipeCreate.Name.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (recipe != null)
            {
                ModelState.AddModelError("", "Recipe already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var recipeMap = _mapper.Map<Recipe>(recipeCreate);

            if (!_recipeRepository.CreateRecipe(publisherId, categoryId,recipeMap)) 
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");

        }

        [HttpPut("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdateRecipe(int recipeId, [FromQuery] int publisherId, [FromQuery] int categoryId, [FromBody] RecipeDto updatedRecipe)
        {
            if (updatedRecipe == null)
                return BadRequest(ModelState);

            if (recipeId != updatedRecipe.Id)
                return BadRequest(ModelState);

            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var recipeMap = _mapper.Map<Recipe>(updatedRecipe);

            if (!_recipeRepository.UpdateRecipe(publisherId, categoryId, recipeMap))
            {
                ModelState.AddModelError("", "Something went wrong updating recipe");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{recipeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeleteRecipe(int recipeId)
        {
            if (!_recipeRepository.RecipeExists(recipeId))
                return NotFound();

            var reviewsToDelete = _reviewRepository.GetReviewsOfRecipe(recipeId);
            var recipeToDelete = _recipeRepository.GetRecipe(recipeId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Somethig went wrong when deleting reviews");
            }

            if (!_recipeRepository.DeleteRecipe(recipeToDelete))
            {
                ModelState.AddModelError("", "Somethig went wrong when deleting recipe");
            }
            return NoContent();
        }

    }
}
