using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using AthleteReviewApp.Dto;
using AthleteReviewApp.Interfaces;
using AthleteReviewApp.Models;

namespace AthleteReviewApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AthleteController : Controller
    {
        private readonly IAthleteRepository _AthleteRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IMapper _mapper;

        public AthleteController(IAthleteRepository AthleteRepository,
            IReviewRepository reviewRepository,
            IMapper mapper)
        {
            _AthleteRepository = AthleteRepository;
            _reviewRepository = reviewRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Athlete>))]
        public IActionResult GetAthletes()
        {
            var Athletes = _mapper.Map<List<AthleteDto>>(_AthleteRepository.GetAthletes());

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Athletes);
        }

        [HttpGet("{pokeId}")]
        [ProducesResponseType(200, Type = typeof(Athlete))]
        [ProducesResponseType(400)]
        public IActionResult GetAthlete(int pokeId)
        {
            if (!_AthleteRepository.AthleteExists(pokeId))
                return NotFound();

            var Athlete = _mapper.Map<AthleteDto>(_AthleteRepository.GetAthlete(pokeId));

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(Athlete);
        }

        [HttpGet("{pokeId}/rating")]
        [ProducesResponseType(200, Type = typeof(decimal))]
        [ProducesResponseType(400)]
        public IActionResult GetAthleteRating(int pokeId)
        {
            if (!_AthleteRepository.AthleteExists(pokeId))
                return NotFound();

            var rating = _AthleteRepository.GetAthleteRating(pokeId);

            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(rating);
        }

        [HttpPost]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateAthlete([FromQuery] int ownerId, [FromQuery] int catId, [FromBody] AthleteDto AthleteCreate)
        {
            if (AthleteCreate == null)
                return BadRequest(ModelState);

            var Athletes = _AthleteRepository.GetAthleteTrimToUpper(AthleteCreate);

            if (Athletes != null)
            {
                ModelState.AddModelError("", "Owner already exists");
                return StatusCode(422, ModelState);
            }

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var AthleteMap = _mapper.Map<Athlete>(AthleteCreate);

      
            if (!_AthleteRepository.CreateAthlete(ownerId, catId, AthleteMap))
            {
                ModelState.AddModelError("", "Something went wrong while savin");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully created");
        }

        [HttpPut("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult UpdateAthlete(int pokeId, 
            [FromQuery] int ownerId, [FromQuery] int catId,
            [FromBody] AthleteDto updatedAthlete)
        {
            if (updatedAthlete == null)
                return BadRequest(ModelState);

            if (pokeId != updatedAthlete.Id)
                return BadRequest(ModelState);

            if (!_AthleteRepository.AthleteExists(pokeId))
                return NotFound();

            if (!ModelState.IsValid)
                return BadRequest();

            var AthleteMap = _mapper.Map<Athlete>(updatedAthlete);

            if (!_AthleteRepository.UpdateAthlete(ownerId, catId,AthleteMap))
            {
                ModelState.AddModelError("", "Something went wrong updating owner");
                return StatusCode(500, ModelState);
            }

            return NoContent();
        }

        [HttpDelete("{pokeId}")]
        [ProducesResponseType(400)]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public IActionResult DeleteAthlete(int pokeId)
        {
            if (!_AthleteRepository.AthleteExists(pokeId))
            {
                return NotFound();
            }

            var reviewsToDelete = _reviewRepository.GetReviewsOfAAthlete(pokeId);
            var AthleteToDelete = _AthleteRepository.GetAthlete(pokeId);

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!_reviewRepository.DeleteReviews(reviewsToDelete.ToList()))
            {
                ModelState.AddModelError("", "Something went wrong when deleting reviews");
            }

            if (!_AthleteRepository.DeleteAthlete(AthleteToDelete))
            {
                ModelState.AddModelError("", "Something went wrong deleting owner");
            }

            return NoContent();
        }
    }
}
