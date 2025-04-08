using CandidateHub.Domain.Entities;
using CandidateHub.Service.Dtos;

namespace CandidateHub.Service.IServices;
public interface ICandidateService
{
    Task<CandidateResponseDto> AddOrUpdateCandidateAsync(CandidateRequestDto dto);
    
}
