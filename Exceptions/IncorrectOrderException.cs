namespace OrderService.Exceptions
{
    public class IncorrectOrderException : Exception
    {
        public IncorrectOrderException() { }

        public IncorrectOrderException(string message) : base(message) { }
    }
}
