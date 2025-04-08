using System.ComponentModel.DataAnnotations;

namespace CandidateHub.Dtos;

public class CandidateDto
{
    [Required(ErrorMessage = "First name is required")]
    [StringLength(100, ErrorMessage = "First name cannot be longer than 100 characters")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [StringLength(100, ErrorMessage = "Last name cannot be longer than 100 characters")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Phone number is required")]
    [Phone(ErrorMessage = "Invalid phone number format")]
    public string PhoneNumber { get; set; }

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; }

    public string? BestCallTime { get; set; }

    [Url(ErrorMessage = "Invalid URL format")]
    public string? LinkedInProfileUrl { get; set; }

    [Url(ErrorMessage = "Invalid URL format")]
    public string? GitHubProfileUrl { get; set; }

    [StringLength(500, ErrorMessage = "Comment cannot be longer than 500 characters")]
    public string? Comment { get; set; }
}
