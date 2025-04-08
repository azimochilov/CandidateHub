using CandidateHub.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace CandidateHub.Domain.Entities;

public class Candidate : Auditable
{
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public string? BestCallTime { get; set; }
    public string? LinkedInProfileUrl { get; set; }
    public string? GitHubProfileUrl { get; set; }
    public string? Comment { get; set; }
}

