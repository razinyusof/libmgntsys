namespace LibraryManagementApi.DTOs.Borrow;

public class BorrowBookResponse
{
    public int BorrowId { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime BorrowedAt { get; set; }
}
