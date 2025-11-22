using LibraryManagementApi.Entities;

namespace LibraryManagementApi.Repositories;

public interface IMemberRepository
{
    Task<Member?> GetByIdAsync(int id);
    Task<IEnumerable<Member>> GetAllAsync();
    Task AddAsync(Member member);
    Task UpdateAsync(Member member);
    Task DeleteAsync(int id);
    Task SaveChangesAsync();
}
