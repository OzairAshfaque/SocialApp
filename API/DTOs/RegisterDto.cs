using System.ComponentModel.DataAnnotations;

namespace API.Data
{
public class RegiserDto 
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }

}

}