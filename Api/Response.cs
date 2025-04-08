namespace CandidateHub.Api;
public class Response<TBody>
{
    public int Code { get; set; }
    public string Message { get; set; }
    public TBody Body { get; set; }
}