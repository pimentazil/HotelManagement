namespace HotelManagement.Exceptions
{
    public class ErrorOrValidationException : HotelManagementException
    {
        public ErrorOrValidationException(string message) : base(message)
        {
            
        }
    }
}
