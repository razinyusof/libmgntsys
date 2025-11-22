using LibraryManagementApi.DTOs.Books;

namespace LibraryManagementApi.Services;

public interface IBookService
{
    Task<IEnumerable<BookResponse>> GetAllAsync();
    Task<BookResponse?> GetByIdAsync(int id);
    Task<BookResponse> CreateBookAsync(CreateBookRequest request);
    Task<BookResponse?> UpdateBookAsync(int id, UpdateBookRequest request);
    Task<bool> DeleteBookAsync(int id);
    Task<IEnumerable<BookResponse>> GetAvailableAsync();
    Task<IEnumerable<BookResponse>> SearchBooksAsync(string? title, string? author, string? isbn);
    Task<bool> CheckAvailabilityAsync(int id);
}
