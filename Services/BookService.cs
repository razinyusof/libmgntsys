using AutoMapper;
using LibraryManagementApi.DTOs.Books;
using LibraryManagementApi.Entities;
using LibraryManagementApi.Repositories;

namespace LibraryManagementApi.Services;

public class BookService : IBookService
{
    private readonly IBookRepository _repo;
    private readonly IMapper _mapper;

    public BookService(IBookRepository repo, IMapper mapper)
    {
        _repo = repo;
        _mapper = mapper;
    }

    public async Task<BookResponse> CreateBookAsync(CreateBookRequest request)
    {
        var book = _mapper.Map<Book>(request);
        book.AvailableCopies = request.TotalCopies;
        await _repo.AddAsync(book);
        await _repo.SaveChangesAsync();
        return _mapper.Map<BookResponse>(book);
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        var b = await _repo.GetByIdAsync(id);
        if (b == null) return false;
        await _repo.DeleteAsync(id);
        await _repo.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<BookResponse>> GetAllAsync()
    {
        var books = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<BookResponse>>(books);
    }

    public async Task<BookResponse?> GetByIdAsync(int id)
    {
        var book = await _repo.GetByIdAsync(id);
        return book == null ? null : _mapper.Map<BookResponse>(book);
    }

    public async Task<IEnumerable<BookResponse>> GetAvailableAsync()
    {
        var books = await _repo.GetAvailableAsync();
        return _mapper.Map<IEnumerable<BookResponse>>(books);
    }

    public async Task<IEnumerable<BookResponse>> SearchBooksAsync(string? title, string? author, string? isbn)
    {
        var books = await _repo.SearchAsync(title, author, isbn);
        return _mapper.Map<IEnumerable<BookResponse>>(books);
    }

    public async Task<bool> CheckAvailabilityAsync(int id)
    {
        var b = await _repo.GetByIdAsync(id);
        return b != null && b.AvailableCopies > 0;
    }

    public async Task<BookResponse?> UpdateBookAsync(int id, UpdateBookRequest request)
    {
        var existing = await _repo.GetByIdAsync(id);
        if (existing == null) return null;
        existing.ISBN = request.ISBN;
        existing.Title = request.Title;
        existing.Author = request.Author;
        // adjust available copies relative to total change
        int diff = request.TotalCopies - existing.TotalCopies;
        existing.TotalCopies = request.TotalCopies;
        existing.AvailableCopies += diff;
        await _repo.UpdateAsync(existing);
        await _repo.SaveChangesAsync();
        return _mapper.Map<BookResponse>(existing);
    }
}
