using AKA.BusinessLayer;
using AKA.BusinessLayer.BookService;
using AKA.BusinessLayer.MemberService;
using AkaLibraryMVC.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace AkaLibraryMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IMemberService _memberService;

        public BookController(
            IBookService bookService,
            IMemberService memberService)
        {
            _bookService = bookService;
            _memberService = memberService;
        }

        [HttpGet]
        public async Task<ActionResult> SignOutBook(int libraryId)
        {
            return View(await BuildSignOutBookVM(libraryId));
        }

        [HttpGet]
        public async Task<ActionResult> ReturnBook(int libraryId)
        {
            return View(await BuildReturnBookVM(libraryId));
        }

        [HttpGet]
        public async Task<JsonResult> GetSingedOutBooks(int libraryId, int memberId)
        {
            var data = await _bookService.GetSignedOutBooks(libraryId, memberId);
            return Json(data.Select(q => new { q.BookId, q.Title }), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> SignOutBook(BookTransactionDto req)
        {
            var count = await _memberService.GetMemberSignOutCount(req.MemberId, req.LibraryId);

            if (count >= Consts.MAX_BOOKS_SIGNOUT_PER_LIBRARY)
            {
                ModelState.AddModelError(string.Empty, Messages.SignOutBookLimitError);
                return View(await BuildSignOutBookVM(req.LibraryId));
            }

            await _memberService.SignOutBookAsync(req.BookId, req.LibraryId, req.MemberId);
            ViewBag.SuccessMessage = Messages.SignOutSuccessfully;
            return View(await BuildSignOutBookVM(req.LibraryId));
        }

        [HttpPost]
        public async Task<ActionResult> ReturnBook(BookTransactionDto req)
        {
            if(!await _memberService.HasPendingSignOut(req.MemberId, req.LibraryId, req.BookId))
            {
                ModelState.AddModelError(string.Empty, Messages.SignOutDifferentLibraryError);
                return View(await BuildReturnBookVM(req.LibraryId));
            }

            await _memberService.ReturnBook(req.BookId, req.LibraryId, req.MemberId);
            ViewBag.SuccessMessage = Messages.ReturnSuccessfully;
            return View(await BuildReturnBookVM(req.LibraryId));
        }

        private async Task<ReturnBookVM> BuildReturnBookVM(int libraryId)
        {
            var members = await _memberService.GetMembersWithPendingSignOuts(libraryId);

            return new ReturnBookVM
            {
                Members = members.Select(q => new SelectListItem { Text = q.FullName, Value = q.MemberId.ToString() }).ToList(),
                LibraryId = libraryId
            };
        }

        private async Task<SignOutBookVM> BuildSignOutBookVM(int libraryId)
        {
            var books = await _bookService.GetAvailableBooks(libraryId);
            var members = await _memberService.GetAllMembers();

            return new SignOutBookVM
            {
                Books = books.Select(q => new SelectListItem { Text = q.Title, Value = q.BookId.ToString() }).ToList(),
                Members = members.Select(q => new SelectListItem { Text = q.FullName, Value = q.MemberId.ToString() }).ToList(),
                LibraryId = libraryId
            };
        }
    }
}