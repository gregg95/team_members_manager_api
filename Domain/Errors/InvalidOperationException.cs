namespace Domain.Errors;

public class InvalidOperationException : Exception
{
    public InvalidOperationException(string message) : base(message)
    {
    }
}
