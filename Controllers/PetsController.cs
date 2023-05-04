using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        // [HttpGet]
        // public IEnumerable<Pet> GetPets() {
        //     return new List<Pet>();
        // }

        // Post -- new pet
        [HttpPost]
        public IActionResult Post(Pet pet)
        {
            _context.Add(pet);
            _context.SaveChanges();

            return CreatedAtAction(nameof(Post), new { id = pet.id }, pet);
        }

        [HttpGet]
        public IEnumerable<Pet> GetAll()
        {
            return _context.Pets.Include(Pet => Pet.petOwner);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Pet pet = _context.Pets.SingleOrDefault(p => p.id == id);
            if (pet is null)
            {
                return NotFound();
            }
            _context.Pets.Remove(pet);
            _context.SaveChanges();

            return NoContent(); //204
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, Pet pet)
        {
            if (id != pet.id)
            {
                return BadRequest(); //405
            }
            _context.Update(pet);
            _context.SaveChanges();

            return Ok(pet); //200
        }

        [HttpPut("{id}/checkin")]
        public IActionResult checkIn(int id)
        {
            Pet pet = _context.Pets.SingleOrDefault(p => p.id == id);
            // pet.checkInPet();
            pet.checkedInAt = DateTime.Now;
            if (pet is null)
            {
                return NotFound();
            }
            _context.Update(pet);
            _context.SaveChanges();

            return Ok(pet); //200
        }

            [HttpPut("{id}/checkout")]
        public IActionResult checkOut(int id)
        {
            Pet pet = _context.Pets.SingleOrDefault(p => p.id == id);
            // pet.checkInPet();
            pet.checkedInAt = null;
            if (pet is null)
            {
                return NotFound();
            }
            _context.Update(pet);
            _context.SaveChanges();

            return Ok(pet); //200
        }

         [HttpPatch("{id}")]
        public IActionResult Patch(int id, Pet pet)
        {
            if (id != pet.id)
            {
                return BadRequest(); //405
            }
            _context.Update(pet);
            _context.SaveChanges();

            return NoContent(); //204
        }

        // [HttpGet]
        // [Route("test")]
        // public IEnumerable<Pet> GetPets() {
        //     PetOwner blaine = new PetOwner{
        //         name = "Blaine"
        //     };

        //     Pet newPet1 = new Pet {
        //         name = "Big Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Black,
        //         breed = PetBreedType.Poodle,
        //     };

        //     Pet newPet2 = new Pet {
        //         name = "Little Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Golden,
        //         breed = PetBreedType.Labrador,
        //     };

        //     return new List<Pet>{ newPet1, newPet2};
        // }
    }
}
