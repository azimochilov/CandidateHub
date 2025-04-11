using CandidateHub.Api;
using CandidateHub.Api.Controller;
using CandidateHub.Service.Dtos;
using CandidateHub.Service.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Moq;

namespace CandidateHub.Tests.Controllers;

public class CandidateControllerTests
{
    private readonly Mock<ICandidateService> _mockCandidateService;
    private readonly IMemoryCache _memoryCache;
    private readonly CandidateController _controller;

    public CandidateControllerTests()
    {
        _mockCandidateService = new Mock<ICandidateService>();
        var logger = new LoggerFactory().CreateLogger<CandidateController>();
        _memoryCache = new MemoryCache(new MemoryCacheOptions());

        _controller = new CandidateController(_mockCandidateService.Object, logger, _memoryCache);
    }

    [Fact]
    public async Task PostAsync_ReturnsOk_WithCreatedCandidate()
    {       
        var requestDto = new CandidateRequestDto
        {
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Email = "john@example.com"
        };

        var responseDto = new CandidateResponseDto
        {
            Id = 1,
            FirstName = "John",
            LastName = "Doe",
            PhoneNumber = "1234567890",
            Email = "john@example.com"
        };

        _mockCandidateService
            .Setup(s => s.AddOrUpdateCandidateAsync(requestDto))
            .ReturnsAsync(responseDto);
        
        var result = await _controller.PostAsync(requestDto) as OkObjectResult;

        Assert.NotNull(result);
        var response = Assert.IsType<Response<CandidateResponseDto>>(result.Value);
        Assert.Equal(200, response.Code);
        Assert.Equal("OK", response.Message);
        Assert.Equal("John", response.Body.FirstName);
        Assert.Equal("Doe", response.Body.LastName);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsFromService_WhenCacheIsEmpty()
    {       
        var candidates = new List<CandidateResponseDto>
        {
            new CandidateResponseDto
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Smith",
                Email = "alice@example.com",
                PhoneNumber = "1112223333"
            },
            new CandidateResponseDto
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Brown",
                Email = "bob@example.com",
                PhoneNumber = "4445556666"
            }
        };

        _mockCandidateService
            .Setup(s => s.RetriveAllAsync())
            .ReturnsAsync(candidates);
        
        var result = await _controller.GetAllAsync() as OkObjectResult;

        Assert.NotNull(result);
        var response = Assert.IsType<Response<IEnumerable<CandidateResponseDto>>>(result.Value);
        Assert.Equal(200, response.Code);
        Assert.Equal(2, response.Body.Count());
    }

    [Fact]
    public async Task GetAllAsync_ReturnsFromCache_WhenAvailable()
    {        
        var cachedCandidates = new List<CandidateResponseDto>
        {
            new CandidateResponseDto
            {
                Id = 3,
                FirstName = "Cached",
                LastName = "User",
                Email = "cached@example.com",
                PhoneNumber = "0001112222"
            }
        };

        _memoryCache.Set("candidatesCacheKey", cachedCandidates);
        
        var result = await _controller.GetAllAsync() as OkObjectResult;
        
        Assert.NotNull(result);
        var response = Assert.IsType<Response<IEnumerable<CandidateResponseDto>>>(result.Value);
        Assert.Single(response.Body);
        Assert.Equal("Cached", response.Body.First().FirstName);
    }
}