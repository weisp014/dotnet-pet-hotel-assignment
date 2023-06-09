using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<PetOwner> GetPets()
        {
            // return new List<PetOwner>();
            return _context.PetOwners;
        }

        [HttpGet("{id}")]
        public ActionResult<PetOwner> Get(int id)
        {
            PetOwner petOwner = _context.PetOwners.SingleOrDefault(p => p.id == id);

            if (id != petOwner.id)
            {
                return BadRequest();
            }

            return petOwner;
        }

        [HttpPost]
        public IActionResult Create(PetOwner petOwner)
        {
            _context.Add(petOwner);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Get), new { id = petOwner.id }, petOwner);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, PetOwner petOwner)
        {
            if (id != petOwner.id)
            {
                return BadRequest();
            }

            _context.Update(petOwner);
            _context.SaveChanges();
            return Ok(petOwner);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            PetOwner petOwner = _context.PetOwners.SingleOrDefault(p => p.id == id);

            if (petOwner is null)
            {
                return NotFound();
            }

            _context.PetOwners.Remove(petOwner);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
