#nullable disable

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Travel1API.Models;
#nullable disable

namespace Travel1API
{
  public class Destination
  {
    public Destination()
      {
        this.Reviews = new HashSet<Review>();
      }
    public int DestinationId { get; set; }
    [Required]
    [StringLength(20)]
    public string DestinationName {get; set; }
    public virtual ICollection<Review> Reviews { get; set; }
  }
}