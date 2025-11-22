namespace LibraryManagementApi.DTOs.Books;

public class UpdateBookRequest
{
    public string ISBN { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int TotalCopies { get; set; }
}
