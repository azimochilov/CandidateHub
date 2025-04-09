using CandidateHub.Service.Dtos;
using CandidateHub.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Emit;

namespace CandidateHub.Api.Controller;
[Controller]
[Route("api/candidates")]
public class CandidateController : ControllerBase
{
    private ICandidateService candidateService;

    public CandidateController(ICandidateService candidateService)
    {
        this.candidateService = candidateService;
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] CandidateRequestDto dto)
        => Ok(new Response<CandidateResponseDto>
        {
            Code = 200,
            Message = "OK",
            Body = await this.candidateService.AddOrUpdateCandidateAsync(dto)
        });

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        var candidates = await this.candidateService.RetriveAllAsync();
       
        return Ok(new Response<IEnumerable<CandidateResponseDto>>
        {
            Code = 200,
            Message = "OK",
            Body = candidates
        });
    }
}