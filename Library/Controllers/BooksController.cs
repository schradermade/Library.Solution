using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace Library.Controllers
{
  [Authorize]
  public class BooksController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public BooksController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }


    public async Task<ActionResult> Index()
    {
        var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var currentUser = await _userManager.FindByIdAsync(userId);
        var userBooks = _db.Books.Where(entry => entry.User.Id == currentUser.Id).ToList();
        return View(userBooks);
    }
    

    public ActionResult SearchBook(string title)
    {
      var thisBook = _db.Books //return Book name and id 
          .Include(book => book.JoinEntries) //find books(JoinEntries) related to the author
          .ThenInclude(join => join.Author) //With all join entries add the related book 
          .FirstOrDefault(book => book.BookName == title); // find the Author that matches the ID
      return View(thisBook);
    }

    public ActionResult Create()
    {
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View();
    }

[HttpPost]
public async Task<ActionResult> Create(Book book, int AuthorId)
{
    var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
    var currentUser = await _userManager.FindByIdAsync(userId);
    book.User = currentUser;
    _db.Books.Add(book);
    if (AuthorId != 0)
    {
        _db.BookAuthor.Add(new BookAuthor() { AuthorId = AuthorId, BookId = book.BookId });
    }
    _db.SaveChanges();
    return RedirectToAction("Index");
}

    public ActionResult Details(int id)
    {
      var thisBook = _db.Books
          .Include(book => book.JoinEntries)
          .ThenInclude(join => join.Author)
          .FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }

    public ActionResult Edit(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName"); // ViewBag only transfers data from controller to view
      return View(thisBook);
    }
    
    [HttpPost]
    public ActionResult Edit(Book book, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.BookAuthor.Add(new BookAuthor() { AuthorId = AuthorId, BookId = book.BookId });
      }
      _db.Entry(book).State=EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddAuthor(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View(thisBook);
    }
    
    [HttpPost]
    public ActionResult AddAuthor(Book book, int AuthorId)
    {
      if(AuthorId != 0)
      {
        _db.BookAuthor.Add(new BookAuthor() { AuthorId = AuthorId, BookId = book.BookId});
      }
        _db.SaveChanges();
        return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      return View(thisBook);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(books => books.BookId == id);
      _db.Books.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  
    [HttpPost]
    public ActionResult DeleteAuthor(int joinId)
    {
      var joinEntry = _db.BookAuthor.FirstOrDefault(entry => entry.BookAuthorId == joinId);
      _db.BookAuthor.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

  }
}
