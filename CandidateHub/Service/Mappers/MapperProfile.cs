using AutoMapper;
using CandidateHub.Domain.Entities;
using CandidateHub.Service.Dtos;
using System.Data;
using System.Security.Claims;
namespace CandidateHub.Service.Mappers;
public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<Candidate, CandidateRequestDto>().ReverseMap();
        CreateMap<Candidate, CandidateResponseDto>().ReverseMap();        
    }
}
