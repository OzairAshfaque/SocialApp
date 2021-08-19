using System.ComponentModel.DataAnnotations;

namespace API.Data
{
public class RegiserDto 
{
    [Required]
    public string Username { get; set; }
    [Required]
    [StringLength(maximumLength:10,MinimumLength =4)]
    public string Password { get; set; }

}

}