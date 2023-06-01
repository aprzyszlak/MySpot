using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Model;
using MySpot.Api.Services;
using System.Linq;

namespace MySpot.Api.Controllers
{
    [ApiController]
    [Route(template: "reservations")]
    public class ReservationsController : ControllerBase
    {
        private readonly ReservationsService _serivce = new ReservationsService();
  
        [HttpGet]
        public ActionResult<IEnumerable<Reservation>> Get()  => Ok(_serivce.GetAll());

        [HttpGet(template: "{id:int}")]
        public ActionResult<Reservation> Get(int id)
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

        [HttpPut(template:"{id:int}")]
        public ActionResult Put(int id, Reservation reservation)
        {
            reservation.Id = id;
            if(_serivce.Update(reservation))
            {
                return NoContent();
            }

            return NoContent();
        }

        [HttpDelete(template:"{id:int}")]
        public ActionResult Delete(int id)
        {
            if (_serivce.Delete(id))
            {
                return NoContent();
            }

            return NotFound();
        }
    }
}
