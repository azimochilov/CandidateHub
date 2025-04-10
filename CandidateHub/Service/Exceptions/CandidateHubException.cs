namespace CandidateHub.Service.Exceptions;
public class CandidateHubException: Exception
{
    public int Code { get; set; }
    public CandidateHubException(int code = 500, string message = "Something went wrong") : base(message)
    {
        this.Code = code;
    }
}
