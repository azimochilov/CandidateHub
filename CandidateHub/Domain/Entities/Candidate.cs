﻿using CandidateHub.Domain.Commons;
using System.ComponentModel.DataAnnotations;

namespace CandidateHub.Domain.Entities;

public class Candidate : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string? BestCallTime { get; set; }
    public string? LinkedInProfileUrl { get; set; }
    public string? GitHubProfileUrl { get; set; }
    public string? Comment { get; set; }
}

