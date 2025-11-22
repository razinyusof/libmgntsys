using LibraryManagementApi.DTOs.Members;

namespace LibraryManagementApi.Services;

public interface IMemberService
{
    Task<IEnumerable<MemberResponse>> GetAllAsync();
    Task<MemberResponse?> GetByIdAsync(int id);
    Task<MemberResponse> CreateMemberAsync(CreateMemberRequest request);
    Task<MemberResponse?> UpdateMemberAsync(int id, CreateMemberRequest request);
    Task<bool> DeleteMemberAsync(int id);
    Task<IEnumerable<object>> GetBorrowedBooksAsync(int memberId);
    Task<IEnumerable<object>> GetBorrowHistoryAsync(int memberId);
}
