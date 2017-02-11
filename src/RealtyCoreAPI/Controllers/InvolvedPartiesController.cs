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
    public class InvolvedPartiesController : Controller
    {

        private readonly RealEstateRepository RealEstRep;

        public InvolvedPartiesController(RealEstateRepository realEstRep)
        {
            RealEstRep = realEstRep;
        }

        /// <summary>
        /// Get All Involved Parties
        /// </summary>
        /// <returns>List of IP's</returns>
        /// <response code ="200">Returns all IP's</response>
        [ProducesResponseType(typeof(InvolvedParty),200)]
        [HttpGet]
        public IEnumerable<InvolvedParty> GetAll()
        {
            var res = RealEstRep.InvolvedPartyGetAll();
            return res;
        }

        /// <summary>
        /// Get All Involved Parties by type 
        /// </summary>
        /// <param name="typeId"> InvolvedParty Type Id</param>
        /// <returns>List of IP's</returns>
        /// <response code ="200">Returns matched IP's</response>
        [ProducesResponseType(typeof(InvolvedParty),200)]
        [HttpGet("GetByType/{typeId}", Name = "GetByType")]
        public IEnumerable<InvolvedParty> GetByType(InvolvedPartyType typeId)
        {
            return RealEstRep.InvolvedPartyFindByType(typeId);
        }

        /// <summary>
        /// Get Involved Parties By LastName
        /// </summary>
        /// <param name="name"> InvolvedParty LastName</param>
        /// <returns></returns>
        /// <response code ="200">Returns matched IP's</response>
        /// <response code ="404">Not found</response>
        [ProducesResponseType(typeof(InvolvedParty), 200)]
        [ProducesResponseType(typeof(InvolvedParty), 404)] //not found
        [HttpGet("GetByName/{name}", Name = "GetByName")]
        public IActionResult GetByName(string name)
        {
            IEnumerable<InvolvedParty> res = RealEstRep.InvolvedPartyFindByName(name);
            if (res.ToList().Count == 0)
            {
                return NotFound(); //404
            }
            return new ObjectResult(res); //200 
        }

        /// <summary>
        /// Get Involved Parties By type And LastName
        /// </summary>
        /// <param name="typeId"> InvolvedParty Type Id</param>
        /// <param name="name"> InvolvedParty LastName</param>
        /// <returns></returns>
        /// <response code ="200">Returns matched IP's</response>
        /// <response code ="404">Not found</response>
        [ProducesResponseType(typeof(InvolvedParty),200)]
        [ProducesResponseType(typeof(InvolvedParty),404)] //not found
        [HttpGet("GetByTypeAndName/{typeId}/{name}", Name = "GetByTypeAndName")]
        public IActionResult GetByTypeAndName(InvolvedPartyType typeId, string name)
        {
            IEnumerable<InvolvedParty> res = RealEstRep.InvolvedPartyFindByNameAndType(typeId, name);
            if (res.ToList().Count == 0 )
            {
                return NotFound(); //404
            }
            return new ObjectResult(res); //200 
        }

        /// <summary>
        /// Demand of Involved Party
        /// </summary>
        /// <param name="involvedPartyId"> InvolvedParty Type Id</param>
        /// <param name="demandId"> InvolvedParty Demand id</param>
        /// <returns></returns>
        /// <response code ="200">Returns specific demand of IP</response>
        /// <response code ="404">Not found</response>
        [ProducesResponseType(typeof(InvolvedParty), 200)]
        [ProducesResponseType(typeof(InvolvedParty), 404)] //not found
        [HttpGet("{involvedPartyId}/Demand/{demandId}", Name = "GetDemand")]
        public IActionResult GetDemand(int involvedPartyId, int demandId)
        {
            var item = RealEstRep.DemandGet(demandId);
            
            if (item == null)
            {
                return NotFound(); //404
            }
            return new ObjectResult(item); //200 
        }

        /// <summary>
        /// Get all demands of customer
        /// </summary>
        /// <returns>List of demands</returns>
        /// <response code ="200">Returns all demands</response>
        [ProducesResponseType(typeof(Demand), 200)]
        [HttpGet("{involvedPartyId}/Demand/", Name = "GetDemands")]
        public IEnumerable<Demand> GetDemands(int involvedPartyId)
        {
            return RealEstRep.DemandList(involvedPartyId, InvolvedPartyType.Customer);
        }

        /// <summary>
        /// Get all properties of customer
        /// </summary>
        /// <returns>List of real estate properies</returns>
        /// <response code ="200">Returns all properties</response>
        [ProducesResponseType(typeof(RealEstateProperty), 200)]
        [HttpGet("{involvedPartyId}/RealEstateProperties/", Name = "GetProperties")]
        public IEnumerable<RealEstateProperty> GetProperties(int involvedPartyId)
        {
            return RealEstRep.RealEstatePropertyOfCustomer(involvedPartyId);
        }

        /// <summary>
        /// Updates Demand
        /// </summary>
        /// <param name="involvedPartyId">InvilvedParty id</param>
        /// <param name="demandId">Demand id</param>
        /// <param name="item">Demand</param>
        /// <returns></returns>
        /// <response code ="204">No content - (Successful update - not returning any content in the body)</response>
        /// <response code ="400">If the item is null - Bad Request</response>
        /// <response code ="404">Not found </response>
        [ProducesResponseType(typeof(Demand), 204)] //No content - (Successful update - not returning any content in the body)
        [ProducesResponseType(typeof(Demand), 404)] //not found
        [ProducesResponseType(typeof(Demand), 400)] //http bad request
        [HttpPut("{involvedPartyId}/Demand/{demandId}", Name = "UpdateDemand")]
        public IActionResult UpdateDemand(int involvedPartyId, int demandId, [FromBody] Demand item)
        {
            if (item == null || item.DemandId != demandId)
            {
                return BadRequest(); //400
            }

            var dm = RealEstRep.DemandGet(demandId);
            if (dm == null)
            {
                return NotFound(); //404 not found
            }

            RealEstRep.DemandUpdate(demandId, item);
            return new NoContentResult(); //204 (successful update - not returning any content in the body)
        }

        /// <summary>
        /// Get Involved Party (By Id)
        /// </summary>
        /// <param name="id"> Involved Party Id</param>
        /// <remarks> Search for Involved Party</remarks>
        /// <returns>Involved Party</returns>
        /// <response code ="200">Returns matched IP</response>
        /// <response code ="404">Not found</response>
        [ProducesResponseType(typeof(InvolvedParty),200)]
        [ProducesResponseType(typeof(InvolvedParty),404)] //not found
        [HttpGet("{id}", Name = "GetById")]
        public IActionResult GetById(int id)
        {
            var item = RealEstRep.InvolvedPartyFind(id);
            if (item == null)
            {
                return NotFound(); //404
            }
            return new ObjectResult(item); //200 
        }

        /// <summary>
        /// Add new Involved Party
        /// </summary>
        /// <param name="item"> Involved Party </param>
        /// <returns>Involved Party </returns>
        /// <response code ="201">Returns the newly created IP</response>
        /// <response code ="400">If the item is null - Bad Request</response>
        [ProducesResponseType(typeof(InvolvedParty),201)]
        [ProducesResponseType(typeof(InvolvedParty),400)] //http bad request
        [HttpPost]
        public IActionResult Create([FromBody] InvolvedParty item)
        {
            if (item == null)
            {
                return BadRequest(); //400
            }
            RealEstRep.InvolvedPartyAdd(item);
            return CreatedAtRoute("GetById", new { controller = "InvolvedParty", id = item.InvolvedPartyId }, item); //201
        }

       	/// <summary>
        /// Add new Demand 
        /// </summary>
        /// <param name="item"> Demand </param>
        /// <returns> Demand </returns>
        /// <response code ="201">Returns the newly created Demand</response>
        /// <response code ="400">If the item is null - Bad Request</response>
        [ProducesResponseType(typeof(Demand), 201)]
		[ProducesResponseType(typeof(Demand), 400)] //http bad request
		[HttpPost("{involvedPartyId}/Demand/", Name = "CreateDemand")]
		public IActionResult CreateDemand([FromBody] Demand item)
		{
			if (item == null)
			{
				return BadRequest(); //400
			}
			RealEstRep.DemandAdd(item);
			return CreatedAtRoute("GetDemand", new { controller = "InvolvedParty", involvedPartyId = item.CustomerId, demandId = item.DemandId }, item); //201
		}


        /// <summary>
        /// Update Involved Party
        /// </summary>
        /// <param name="id">Involved Party Id</param>
        /// <param name="item">Involved Party</param>
        /// <returns>No content</returns>
        /// <response code ="204">No content - (Successful update - not returning any content in the body)</response>
        /// <response code ="400">If the item is null - Bad Request</response>
        /// <response code ="404">Not found </response>
        [ProducesResponseType(typeof(InvolvedParty), 204)] //No content - (Successful update - not returning any content in the body)
        [ProducesResponseType(typeof(InvolvedParty), 404)] //not found
        [ProducesResponseType(typeof(InvolvedParty), 400)] //http bad request
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] InvolvedParty item)
        {
            if (item == null || item.InvolvedPartyId != id)
            {
                return BadRequest(); //400
            }

            var todo = RealEstRep.InvolvedPartyFind(id);
            if (todo == null)
            {
                return NotFound(); //404 not found
            }

            RealEstRep.InvolvedPartyUpdate(item);
            return new NoContentResult(); //204 (successful update - not returning any content in the body)
        }

        /// <summary>
        /// Delete Involved Party
        /// </summary>
        /// <param name="id">Involved Party Id</param>
        /// <response code ="200">OK - (Successful Deleted existing item or item dosen't exist)</response>
        [ProducesResponseType(typeof(InvolvedParty), 200)] //OK - (Successful Deleted existing item or item dosen't exist)
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            RealEstRep.InvolvedPartyRemove(id); 
        }


        /// <summary>
        /// Get all demands of agent/broker
        /// </summary>
        /// <returns>List of demands</returns>
        /// <response code ="200">Returns all demands</response>
        [ProducesResponseType(typeof(Demand), 200)]
        [HttpGet("Agents/{agentId}/Demands/", Name = "GetAllDemandsOfAgent")]
        public IEnumerable<Demand> GetAllDemandsOfAgent(int agentId)
        {
            return RealEstRep.DemandList(agentId, InvolvedPartyType.Agent);
        }

        /// <summary>
        /// Get all properties of agent/broker
        /// </summary>
        /// <returns>List of real estate properties</returns>
        /// <response code ="200">Returns all properties</response>
        [ProducesResponseType(typeof(RealEstateProperty), 200)]
        [HttpGet("Agents/{agentId}/RealEstateProperties/", Name = "GetAllPropertiesOfAgent")]
        public IEnumerable<RealEstateProperty> GetAllPropertiesOfAgent(int agentId)
        {
            return RealEstRep.RealEstatePropertyOfAgent(agentId);
        }

    }
}
