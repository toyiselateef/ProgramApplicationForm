namespace ProgramApplicationForm.Application.Exceptions;

public class ServerErrorException : Exception
    {
        public ServerErrorException(string message)
        : base(message)
        {
        }
    }