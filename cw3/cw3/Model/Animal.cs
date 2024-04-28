using System.ComponentModel.DataAnnotations;

namespace cw3.Model;

public class Animal
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(200)]
    public string Name { get; set; }
    public string Description { get; set; }
    
    [Required]
    [MaxLength(124)]
    public string Category { get; set; }
    
    [Required]
    [MaxLength(124)]
    public string Area { get; set; }
}