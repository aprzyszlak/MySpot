namespace MySpot.Api.Exceptions
{
    public sealed class InvalidReservationDateException : CustomException
    {
        public DateTime Date { get; }
        public InvalidReservationDateException(DateTime date) : base(message: $"Reservation date: {date:d} is invalid.")
        {
            Date = date;
        }
    }

    public sealed class ParkingSpotAlreadyReservedException : CustomException
    {
        public DateTime Date { get; }
        public ParkingSpotAlreadyReservedException(string name, DateTime date) 
            : base(message: $"Parking spot: {name:d} already reserved at: {date:d}.")
        {
            Date = date;
        }
    }
}
