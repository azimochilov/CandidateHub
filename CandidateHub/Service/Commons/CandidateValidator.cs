using CandidateHub.Service.Dtos;
using CandidateHub.Service.Exceptions;
using System.Text.RegularExpressions;

namespace CandidateHub.Service.Commons;
  public static class CandidateValidator
{
        public static void ValidateCandidate(CandidateRequestDto dto)
        {
        if (!Regex.IsMatch(dto.PhoneNumber ?? "", @"^\+?[0-9]{7,15}$"))
        {
            throw new CandidateHubException(400, "Invalid phone number format.");
        }

        if (!Regex.IsMatch(dto.Email ?? "", @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            throw new CandidateHubException(400, "Invalid email format.");
        }

        // LinkedIn URL: starts with https://www.linkedin.com/
        if (!string.IsNullOrWhiteSpace(dto.LinkedInProfileUrl) &&
            !Regex.IsMatch(dto.LinkedInProfileUrl, @"^https:\/\/(www\.)?linkedin\.com\/.*"))
        {
            throw new CandidateHubException(400, "Invalid LinkedIn profile URL.");
        }

        // GitHub URL: starts with https://github.com/
        if (!string.IsNullOrWhiteSpace(dto.GitHubProfileUrl) &&
            !Regex.IsMatch(dto.GitHubProfileUrl, @"^https:\/\/(www\.)?github\.com\/.*"))
        {
            throw new CandidateHubException(400, "Invalid GitHub profile URL.");
        }
    }
}
