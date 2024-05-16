namespace ProgramApplicationForm.Application.Exceptions;

public class UnProcessedEntityException : Exception
{
    public UnProcessedEntityException(string message) : base(message)
    {
    }
}