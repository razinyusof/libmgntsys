namespace LibraryManagementApi.DTOs.Borrow;

public class ReturnBookResponse
{
    public string Message { get; set; } = string.Empty;
    public DateTime ReturnedAt { get; set; }
}
