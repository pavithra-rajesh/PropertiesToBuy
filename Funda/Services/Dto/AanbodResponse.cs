namespace Funda.Services.Dto {
  public class AanbodResponse {
    public Object[] objects { get; set; }
  }

  public class Object {
    public int MakelaarId { get; set; }
    public string MakelaarNaam { get; set; }
    public string Woonplaats { get; set; }
  }
}
