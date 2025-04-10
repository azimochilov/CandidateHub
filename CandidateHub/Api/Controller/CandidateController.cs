using CandidateHub.Service.Dtos;
using CandidateHub.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Reflection.Emit;

namespace CandidateHub.Api.Controller;
[Controller]
[Route("api/candidates")]
public class CandidateController : ControllerBase
{
    private ICandidateService candidateService;
    private readonly ILogger<CandidateController> logger;
    private readonly IMemoryCache cache;

    private readonly string cacheKey = "candidatesCacheKey";

    public CandidateController(ICandidateService candidateService, ILogger<CandidateController> logger, IMemoryCache cache)
    {
        this.candidateService = candidateService;
        this.logger = logger;
        this.cache = cache;
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
    public async Task<IActionResult> GetAllAsync()
    {
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        if (cache.TryGetValue(cacheKey, out IEnumerable<CandidateResponseDto> cachedCandidates))
        {
            logger.LogInformation("Candidates found in cache.");
        }
        else
        {
            logger.LogInformation("Candidates NOT found in cache. Loading from service.");

            cachedCandidates = await this.candidateService.RetriveAllAsync();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(45))
                .SetAbsoluteExpiration(TimeSpan.FromHours(1))
                .SetPriority(CacheItemPriority.Normal);

            cache.Set(cacheKey, cachedCandidates, cacheEntryOptions);
        }

        stopwatch.Stop();
        logger.LogInformation($"Elapsed time: {stopwatch.ElapsedMilliseconds} ms");

        return Ok(new Response<IEnumerable<CandidateResponseDto>>
        {
            Code = 200,
            Message = "OK",
            Body = cachedCandidates
        });
    }
}