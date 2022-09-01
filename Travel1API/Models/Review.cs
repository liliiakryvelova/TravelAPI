#nullable disable

using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Travel1API.Models;
#nullable disable

namespace Travel1API
{
  public class Review
  {
    public int ReviewId { get; set; }
    public string Description {get; set; }
    [Required]
    [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
    public int Rating { get; set; }
    public int DestinationId { get; set; }

  }
}