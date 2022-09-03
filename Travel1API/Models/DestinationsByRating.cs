namespace Travel1API
{
  public class DestinationByRating
  {
    public string DestinationName { get; set; }
    public int Rating { get; set; }

    public DestinationByRating(string destinationName, int rating){
        DestinationName = destinationName;
        Rating = rating;
    }

  }
}