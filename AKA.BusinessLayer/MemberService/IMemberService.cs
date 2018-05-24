using AKA.DataLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AKA.BusinessLayer.MemberService
{
    public interface IMemberService
    {
        Task SignOutBookAsync(int bookId, int libraryId, int memberId);
        Task ReturnBook(int bookId, int libraryId, int memberId);
        Task<int> GetMemberSignOutCount(int memberId, int libraryId);
        Task<bool> HasPendingSignOut(int memberId, int libraryId, int bookId);

        Task<List<Member>> GetAllMembers();
        Task<List<Member>> GetMembersWithPendingSignOuts(int libraryId);
    }
}
