namespace ProgramApplicationForm.Application.Exceptions;

public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string message)
        : base(message)
        {
        }
    }