using AutoMapper;
using CandidateHub.Data.IRepositories;
using CandidateHub.Domain.Entities;
using CandidateHub.Service.Commons;
using CandidateHub.Service.Dtos;
using CandidateHub.Service.Exceptions;
using CandidateHub.Service.IServices;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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
        CandidateValidator.ValidateCandidate(candidateDto);

        var candidate = await this._candidateRepository.SelectAsync(c => c.Email == candidateDto.Email);
        if (candidate is null)
        {
            var creationEntity = _mapper.Map<Candidate>(candidateDto);
            candidate = await this._candidateRepository.InsertAsync(creationEntity);
        }
        else
        {
            _mapper.Map(candidateDto, candidate);
            candidate.UpdatedAt = DateTime.UtcNow;
            await this._candidateRepository.SaveAsync();
        }

        return _mapper.Map<CandidateResponseDto>(candidate);
    }
   
    

    public async Task<IEnumerable<CandidateResponseDto>> RetriveAllAsync()
    {
        var candidates = await this._candidateRepository.SelectAll()
            .ToListAsync();
        return _mapper.Map<IEnumerable<CandidateResponseDto>>(candidates);
    }

}
