using System.ComponentModel.DataAnnotations;

namespace WebApplication2.Models;

public class Phone
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    public string Colour { get; set; }

    public decimal Price { get; set; }
}


public class PhoneWithoutId
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Colour { get; set; }

    public decimal Price { get; set; }
}