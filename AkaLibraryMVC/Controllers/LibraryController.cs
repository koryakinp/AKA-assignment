using AKA.BusinessLayer.LibraryService;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AkaLibraryMVC.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILibraryService _libraryService;

        public LibraryController(ILibraryService libraryService)
        {
            _libraryService = libraryService;
        }

        public async Task<ActionResult> Index()
        {
            var data = await _libraryService.GetLibraries();
            return View(data);
        }
    }
}