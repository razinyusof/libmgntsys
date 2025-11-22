using LibraryManagementApi.Data;
using LibraryManagementApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementApi.Repositories;

public class BorrowRepository : IBorrowRepository
{
    private readonly LibraryDbContext _context;
    public BorrowRepository(LibraryDbContext context) => _context = context;

    public async Task AddAsync(BorrowRecord record) => await _context.BorrowRecords.AddAsync(record);
    public async Task<BorrowRecord?> GetByIdAsync(int id) => await _context.BorrowRecords.FindAsync(id);

    public async Task<IEnumerable<BorrowRecord>> GetByBookAsync(int bookId) =>
        await _context.BorrowRecords.Where(r => r.BookId == bookId).ToListAsync();

    public async Task<IEnumerable<BorrowRecord>> GetByMemberAsync(int memberId) =>
        await _context.BorrowRecords.Where(r => r.MemberId == memberId).ToListAsync();

    public async Task<IEnumerable<BorrowRecord>> GetPendingReturnsAsync() =>
        await _context.BorrowRecords.Where(r => r.ReturnedAt == null).ToListAsync();

    public Task SaveChangesAsync() => _context.SaveChangesAsync();

    public Task UpdateAsync(BorrowRecord record)
    {
        _context.BorrowRecords.Update(record);
        return Task.CompletedTask;
    }
}
