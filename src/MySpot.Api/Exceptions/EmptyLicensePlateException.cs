namespace MySpot.Api.Exceptions
{
    public sealed class EmptyLicensePlateException : CustomException
    {
        public EmptyLicensePlateException() : base(message: "License plate is empty")
        {
        }
    }
}
