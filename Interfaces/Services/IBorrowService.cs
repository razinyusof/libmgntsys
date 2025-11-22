using LibraryManagementApi.DTOs.Borrow;

namespace LibraryManagementApi.Services;

public interface IBorrowService
{
    Task<BorrowBookResponse?> BorrowBookAsync(BorrowBookRequest request);
    Task<ReturnBookResponse?> ReturnBookAsync(ReturnBookRequest request);
    Task<IEnumerable<BorrowBookResponse>> GetBorrowedByMemberAsync(int memberId);
    Task<IEnumerable<BorrowBookResponse>> GetBorrowHistoryByBookAsync(int bookId);
    Task<IEnumerable<BorrowBookResponse>> GetPendingReturnsAsync();
}
