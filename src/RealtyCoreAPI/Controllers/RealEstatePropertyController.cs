using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RealtyLibrary.Model;
using RealtyCoreAPI.Repository;
using Swashbuckle.SwaggerGen.Annotations;

namespace RealtyCoreAPI.Controllers
{
    [Route("api/[controller]")]
    public class RealEstatePropertiesController : Controller
    {

        private readonly RealEstateRepository RealEstRep;

        public RealEstatePropertiesController(RealEstateRepository realEstRep)
        {
            RealEstRep = realEstRep;
        }

        /// <summary>
        /// Get all prperties
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Returns all Real Estate Properties</response>
        [ProducesResponseType(typeof(RealEstateProperty), 200)]
        [HttpGet]
        public IEnumerable<RealEstateProperty> RealEstatePropertyGetAll()
        {
            return RealEstRep.RealEstatePropertyGetAll();
        }

        /// <summary>
        /// Search for RE's (SiteCode, RealEstatePropertyId)
        /// </summary>
        /// <param name="item">String to search</param>
        /// <returns></returns>
        /// <response code ="200">Returns matched Real Estate Properties</response>
        /// <response code ="404">Not found</response>
        [ProducesResponseType(typeof(RealEstateProperty), 200)]
        [ProducesResponseType(typeof(RealEstateProperty), 404)] //not found
        [HttpGet("search/{item}", Name = "search")]
        public IActionResult RealEstatePropertySearch(string item)
        {
            var res = RealEstRep.RealEstatePropertySearch(item);
            if (res.Count() == 0)
            {
                return NotFound(); //404
            }
            return new ObjectResult(res); //200
        }

        /// <summary>
        /// Get Real Estate Property (By Id)
        /// </summary>
        /// <param name="id">Real Estate Property Id</param>
        /// <returns>Real Estate Property</returns>
        /// <response code ="200">Returns matched Real Estate Properties</response>
        /// <response code ="404">Not found</response>
        [ProducesResponseType(typeof(RealEstateProperty), 200)]
        [ProducesResponseType(typeof(RealEstateProperty), 404)] //not found
        [HttpGet("{id}")]
        public IActionResult RealEstatePropertyGetById(string id)
        {
            var item = RealEstRep.RealEstatePropertyFindbyId(id);
            if (item == null)
            {
                return NotFound(); //404
            }
            return new ObjectResult(item); //200
        }

        /// <summary>
        /// Add new real estate property
        /// </summary>
        /// <param name="item">Real Estate Property</param>
        /// <returns>Created Real Estate Property</returns>
        /// <response code ="201">Returns the newly created Real Estate Property</response>
        /// <response code ="400">If the item is null - Bad Request</response>
        [ProducesResponseType(typeof(RealEstateProperty), 201)]
        [ProducesResponseType(typeof(RealEstateProperty), 400)] //http bad request
        [HttpPost]
        public IActionResult Create([FromBody] RealEstateProperty item)
        {
            if (item == null)
            {
                return BadRequest(); //400
            }
			var rng = new System.Random();
			var randomNumber = rng.Next(100, 1000000);
			item.RealEstatePropertyId = randomNumber;
            RealEstRep.RealEstatePropertyAdd(item);
            return CreatedAtRoute("RealEstatePropertyGetById", new { controller = "RealEstateProperties", id = item.RealEstatePropertyId }, item); //201
        }

        /// <summary>
        /// Update Real estate property
        /// </summary>
        /// <param name="id">Real estate property Id</param>
        /// <param name="item">Real Estate Property</param>
        /// <returns>No Content</returns>
        /// <response code ="204">No content - (Successful update - not returning any content in the body)</response>
        /// <response code ="400">If the item is null - Bad Request</response>
        /// <response code ="404">Not found </response>
        [ProducesResponseType(typeof(RealEstateProperty), 204)] //No content - (Successful update - not returning any content in the body)
        [ProducesResponseType(typeof(RealEstateProperty), 404)] //not found
        [ProducesResponseType(typeof(RealEstateProperty), 400)] //http bad request
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody] RealEstateProperty item)
        {
            if (item == null || item.RealEstatePropertyId.ToString() != id)
            {
                return BadRequest(); //400
            }

            var todo = RealEstRep.RealEstatePropertyFindbyId(id);
            if (todo == null)
            {
                return NotFound(); //404 not found
            }

            RealEstRep.RealEstatePropertyUpdate(item);
            return new NoContentResult(); //204
        }

        /// <summary>
        /// Delete Real Estate Property
        /// </summary>
        /// <param name="id">Real Estate Property Id</param>
        /// <response code ="200">OK - (Successful Deleted existing item or item dosen't exist)</response>
        [ProducesResponseType(typeof(InvolvedParty), 200)] //OK - (Successful Deleted existing item or item dosen't exist)
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            RealEstRep.RealEstatePropertyRemove(id);
        }

        /// <summary>
        /// Search for matching REs
        /// </summary>
        /// <param name="demand">demand</param>
        /// <returns></returns>
        /// <response code ="200">Returns matched Real Estate Properties</response>
        /// <response code ="404">Not found</response>
        [ProducesResponseType(typeof(RealEstateProperty), 200)]
        [ProducesResponseType(typeof(RealEstateProperty), 404)] //not found
        [HttpPost("demandmatching", Name = "demandmatching")]
        public IActionResult DemandMatching([FromBody] Demand demand)
        {

             var res = RealEstRep.RealEstateProperties.Where(
               x => x.PropertyCategory == demand.PropertyCategory
                && x.PropertyType == demand.PropertyType 
                && x.Price <= demand.PriceTo
                && (x.Purpose == Purpose.RentalOrSale || x.Purpose == demand.Purpose)
                );

            if (!res.Any())
            {
                return NotFound(); //404
            }
            return new ObjectResult(res); //200
        }
    }
}
