using AutoMapper;
using LibraryManagementApi.DTOs.Borrow;
using LibraryManagementApi.Entities;
using LibraryManagementApi.Repositories;

namespace LibraryManagementApi.Services;

public class BorrowService : IBorrowService
{
    private readonly IBorrowRepository _borrowRepo;
    private readonly IBookRepository _bookRepo;
    private readonly IMemberRepository _memberRepo;
    private readonly IMapper _mapper;

    public BorrowService(IBorrowRepository borrowRepo, IBookRepository bookRepo, IMemberRepository memberRepo, IMapper mapper)
    {
        _borrowRepo = borrowRepo;
        _bookRepo = bookRepo;
        _memberRepo = memberRepo;
        _mapper = mapper;
    }

    public async Task<BorrowBookResponse?> BorrowBookAsync(BorrowBookRequest request)
    {
        var member = await _memberRepo.GetByIdAsync(request.MemberId);
        var book = await _bookRepo.GetByIdAsync(request.BookId);
        if (member == null || book == null) return null;
        if (book.AvailableCopies <= 0) return null;

        var record = new BorrowRecord {
            MemberId = request.MemberId,
            BookId = request.BookId,
            BorrowedAt = DateTime.UtcNow
        };

        book.AvailableCopies -= 1;
        await _borrowRepo.AddAsync(record);
        await _bookRepo.UpdateAsync(book);
        await _borrowRepo.SaveChangesAsync();
        await _bookRepo.SaveChangesAsync();

        return new BorrowBookResponse {
            BorrowId = record.Id,
            Message = "Borrowed successfully",
            BorrowedAt = record.BorrowedAt
        };
    }

    public async Task<ReturnBookResponse?> ReturnBookAsync(ReturnBookRequest request)
    {
        var record = await _borrowRepo.GetByIdAsync(request.BorrowId);
        if (record == null || record.ReturnedAt != null) return null;

        var book = await _bookRepo.GetByIdAsync(record.BookId);
        if (book == null) return null;

        record.ReturnedAt = DateTime.UtcNow;
        book.AvailableCopies += 1;

        await _borrowRepo.UpdateAsync(record);
        await _bookRepo.UpdateAsync(book);
        await _borrowRepo.SaveChangesAsync();
        await _bookRepo.SaveChangesAsync();

        return new ReturnBookResponse {
            Message = "Returned successfully",
            ReturnedAt = record.ReturnedAt.Value
        };
    }

    public async Task<IEnumerable<BorrowBookResponse>> GetBorrowedByMemberAsync(int memberId)
    {
        var records = await _borrowRepo.GetByMemberAsync(memberId);
        return records.Select(r => new BorrowBookResponse {
            BorrowId = r.Id,
            Message = r.ReturnedAt == null ? "Borrowed" : "Returned",
            BorrowedAt = r.BorrowedAt
        }).ToList();
    }

    public async Task<IEnumerable<BorrowBookResponse>> GetBorrowHistoryByBookAsync(int bookId)
    {
        var records = await _borrowRepo.GetByBookAsync(bookId);
        return records.Select(r => new BorrowBookResponse {
            BorrowId = r.Id,
            Message = r.ReturnedAt == null ? "Borrowed" : "Returned",
            BorrowedAt = r.BorrowedAt
        }).ToList();
    }

    public async Task<IEnumerable<BorrowBookResponse>> GetPendingReturnsAsync()
    {
        var records = await _borrowRepo.GetPendingReturnsAsync();
        return records.Select(r => new BorrowBookResponse {
            BorrowId = r.Id,
            Message = "Borrowed",
            BorrowedAt = r.BorrowedAt
        }).ToList();
    }
}
