using AutoMapper;
using CandidateHub.Data.IRepositories;
using CandidateHub.Domain.Entities;
using CandidateHub.Service.Dtos;
using CandidateHub.Service.IServices;

namespace CandidateHub.Service.Services;

public class CandidateService : ICandidateService
{
    private readonly IRepository<Candidate> _candidateRepository;
    private readonly IMapper _mapper;    

    public CandidateService(
        IRepository<Candidate> candidateRepository,
        IMapper mapper    )        
    {
        _candidateRepository = candidateRepository;
        _mapper = mapper;        
    }

    public async Task<CandidateResponseDto> AddOrUpdateCandidateAsync(CandidateRequestDto candidateDto)
    {
        var candidate = await this._candidateRepository.SelectAsync(c => c.Email ==  candidateDto.Email);
        if( candidate is null)
        {
            var creationEntity = _mapper.Map<Candidate>(candidateDto);
            candidate = await this._candidateRepository.InsertAsync(creationEntity);
        }
        else
        {
            _mapper.Map(candidateDto, candidate);

            candidate.UpdatedAt = DateTime.UtcNow;

            candidate = this._candidateRepository.Update(candidate);
        }

        return _mapper.Map<CandidateResponseDto>(candidate);
    }

}
