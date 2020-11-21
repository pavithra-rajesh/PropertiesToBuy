namespace test {


  public class GetAanbodResponse {
    public Object[] objects { get; set; }
  }

  public class Object {
    public int MakelaarId { get; set; }
    public string MakelaarNaam { get; set; }
    public string Woonplaats { get; set; }
  }
}