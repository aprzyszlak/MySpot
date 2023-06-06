using MySpot.Api.Exceptions;
using MySpot.Api.Model;

namespace MySpot.Api.Entities
{
    public class WeeklyParkingSpot
    {
        public readonly HashSet<Reservation> _reservation = new();
        public Guid Id { get; }
        public DateTime From { get; }
        public DateTime To { get; }
        public string Name { get; }
        public IEnumerable<Reservation> Reservations { get; }

        public WeeklyParkingSpot(Guid id, DateTime from, DateTime to, string name)
        {
            Id = id;
            From = from;
            To = to;
            Name = name;
        }

        public void AddReservation(Reservation reservation)
        {
            var isInvalidDate = reservation.Date.Date < From ||
                                reservation.Date.Date > To ||
                                reservation.Date.Date < DateTime.UtcNow.Date;

            if (isInvalidDate)
            {
                throw new InvalidReservationDateException(reservation.Date);
            }
            
            var reservationAlreadyExists = Reservations.Any(x => x.Date.Date == reservation.Date.Date);
            if (reservationAlreadyExists)
            {
                throw new ParkingSpotAlreadyReservedException(Name, reservation.Date);
            }

            _reservation.Add(reservation);
        }
    }
}
