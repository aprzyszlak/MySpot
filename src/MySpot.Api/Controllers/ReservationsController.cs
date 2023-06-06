using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Entities;
using MySpot.Api.Model;
using System.Linq;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route(template: "reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsService _serivce = new ReservationsService();
  
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get()  => Ok(_serivce.GetAllWeekly());

        [HttpGet(template: "{id:guid}")]
        public ActionResult<Reservation> Get(Guid id)
        {
            var resevation = _serivce.Get(id);
            if (resevation is null)
            {
                return NotFound();
            }

            return Ok(resevation);
        }

        [HttpPost]
        public ActionResult Post(Reservation reservation)
        {
            var id = _serivce.Create(reservation);
            if(id is null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(Get), routeValues:new {id}, value:null);
        }

        [HttpPut(template:"{id:guid}")]
        public ActionResult Put(Guid id, Reservation reservation)
        {
            reservation.Id = id;
            if(_serivce.Update(reservation))
            {
                return NoContent();
            }

            return NoContent();
        }

        [HttpDelete(template:"{id:guid}")]
        public ActionResult Delete(Guid id)
        {
            if (_serivce.Delete(id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
