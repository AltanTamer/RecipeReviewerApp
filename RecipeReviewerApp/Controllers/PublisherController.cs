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
    public class PublisherController : Controller
    {
        private readonly IPublisherRepository _publisherRepository;
        private readonly IMapper _mapper;

        public PublisherController(IPublisherRepository publisherRepository, IMapper mapper)
        {
            _publisherRepository = publisherRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Publisher>))]
        public IActionResult GetPublishers()
        {
            var publishers = _mapper.Map<List<PublisherDto>>(_publisherRepository.GetPublishers());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(publishers);
        }

        [HttpGet("{publisherId}")]
        [ProducesResponseType(200, Type = typeof(Publisher))]
        [ProducesResponseType(400)]
        public IActionResult GetPublisher(int publisherId)
        {
            if (!_publisherRepository.PublisherExists(publisherId))
                return NotFound();

            var publisher = _mapper.Map<PublisherDto>(_publisherRepository.GetPublisher(publisherId));
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(publisher);
        }
        [HttpGet("recipeByPublisher/{publisherId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Recipe>))]
        [ProducesResponseType(400)]
        public IActionResult GetRecipeByPublisher(int publisherId)
        {
            if(!_publisherRepository.PublisherExists(publisherId))
                return NotFound();

            var recipes = _mapper.Map<List<RecipeDto>>(
                _publisherRepository.GetRecipeByPublisher(publisherId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(recipes);
        }


        [HttpGet("publisherOfRecipe/{recipeId}")]
        [ProducesResponseType(200, Type = typeof(Publisher))]
        [ProducesResponseType(400)]
        public IActionResult GetPublisherOfRecipe(int recipeId)
        {
            var publisher = _mapper.Map<List<PublisherDto>>(
                _publisherRepository.GetPublisherOfRecipe(recipeId));
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            return Ok(publisher);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreatePublisher([FromBody] PublisherDto publisherCreate)
        {
            if (publisherCreate == null)
                return BadRequest(ModelState);

            var publisher = _publisherRepository.GetPublishers()
                .Where(c => c.LastName.Trim().ToUpper() == publisherCreate.LastName.TrimEnd().ToUpper())
                .FirstOrDefault();

            if (publisher != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var publisherMap = _mapper.Map<Publisher>(publisherCreate);

            if (!_publisherRepository.CreatePublisher(publisherMap))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return Ok("Succesfully created");

        }

        [HttpPut("{publisherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult UpdatePublisher(int publisherId, [FromBody] PublisherDto updatedPublisher)
        {
            if (updatedPublisher == null)
                return BadRequest(ModelState);

            if (publisherId != updatedPublisher.Id)
                return BadRequest(ModelState);

            if (!_publisherRepository.PublisherExists(publisherId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var publisherMap = _mapper.Map<Publisher>(updatedPublisher);

            if (!_publisherRepository.UpdatePublisher(publisherMap))
            {
                ModelState.AddModelError("", "Something went wrong updating publisher");
                return StatusCode(500, ModelState);
            }
            return NoContent();

        }

        [HttpDelete("{publisherId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]

        public IActionResult DeletePublisher(int publisherId)
        {
            if (!_publisherRepository.PublisherExists(publisherId))
                return NotFound();

            var publisherToDelete = _publisherRepository.GetPublisher(publisherId);

            if (!ModelState.IsValid)
                return BadRequest();

            if (!_publisherRepository.DeletePublisher(publisherToDelete))
            {
                ModelState.AddModelError("", "Somethig went wrong when deleting publisher");
            }
            return NoContent();
        }
    }
}
