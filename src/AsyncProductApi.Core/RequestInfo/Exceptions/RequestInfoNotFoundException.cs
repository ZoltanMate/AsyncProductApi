namespace AsyncProductApi.Core.RequestInfo.Exceptions;

public class RequestInfoNotFoundException : Exception
{
    public RequestInfoNotFoundException(string errorMessage)
        : base(errorMessage)
    {

    }
}