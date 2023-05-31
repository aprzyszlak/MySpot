using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Model;
using System.Linq;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route(template: "reservations")]
    public class ReservationsController : ControllerBase
    {
        private static readonly List<string> _parkingSpotNames = new List<string>()
        {
            "P1", "P2", "P3", "P4", "P5"
        };

        private static int _id = 1;
        private static readonly List<Reservation> Reservations = new List<Reservation>();


        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get()  => Ok(Reservations);

        [HttpGet(template: "{id:int}")]
        public ActionResult<Reservation> Get(int id)
        {
            var reservation = Reservations.SingleOrDefault(x => x.Id == id); 
            if (reservation is null)
            {
                return NotFound();
            }

            return Ok(reservation);
        }

        [HttpPost]
        public ActionResult Post(Reservation reservation)
        {
            if(_parkingSpotNames.All(x => x != reservation.ParkingSpotName))
            {
                return BadRequest();
            }

            reservation.Date = DateTime.UtcNow.AddDays(1).Date;
            var reservationAlreadyExists = Reservations.Any(x => x.ParkingSpotName == reservation.ParkingSpotName &&
            x.Date.Date == reservation.Date.Date);

            if(reservationAlreadyExists)
            {
                return BadRequest();
            }

            reservation.Id = _id;
            _id++;
            Reservations.Add(reservation);

            return CreatedAtAction(nameof(Get), routeValues:new {id = reservation.Id}, value:null);
        }

        [HttpPut(template:"{id:int}")]
        public ActionResult Put(int id, Reservation reservation)
        {
            var existingReservation = Reservations.SingleOrDefault(x => x.Id == id);
            if (existingReservation is null)
            {
                return NotFound();
            }

            existingReservation.LicensePlate = reservation.LicensePlate;
            return NoContent();
        }

        [HttpDelete(template:"{id:int}")]
        public ActionResult Delete(int id)
        {
            var existingReservation = Reservations.SingleOrDefault(x => x.Id == id);
            if (existingReservation is null)
            {
                return NotFound();
            }

            Reservations.Remove(existingReservation);
            return NoContent();
        }
    }
}
