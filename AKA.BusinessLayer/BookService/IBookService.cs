using AKA.DataLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AKA.BusinessLayer.BookService
{
    public interface IBookService
    {
        Task<List<BookTitle>> GetAvailableBooks(int libraryId);
        Task<List<BookTitle>> GetSignedOutBooks(int libraryId, int memberId);
    }
}
