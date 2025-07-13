namespace LibraryManagement.Communication;

public class RegisterBookJson
{
  public string Title { get; set; } = string.Empty;
  public string Author { get; set; } = string.Empty;
  public string Category { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public int Stock { get; set; }
}