using AKA.DataLayer;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AKA.BusinessLayer.LibraryService
{
    public interface ILibraryService
    {
        Task<List<Library>> GetLibraries();
        Task<Library> GetLinraryById(int id);
    }
}
