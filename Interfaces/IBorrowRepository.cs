using LibraryManagementApi.Entities;

namespace LibraryManagementApi.Repositories;

public interface IBorrowRepository
{
    Task<BorrowRecord?> GetByIdAsync(int id);
    Task AddAsync(BorrowRecord record);
    Task UpdateAsync(BorrowRecord record);
    Task<IEnumerable<BorrowRecord>> GetByMemberAsync(int memberId);
    Task<IEnumerable<BorrowRecord>> GetByBookAsync(int bookId);
    Task<IEnumerable<BorrowRecord>> GetPendingReturnsAsync();
    Task SaveChangesAsync();
}
