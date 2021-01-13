using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Library.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Library.Controllers
{
  public class HomeController : Controller
  {
    private readonly LibraryContext _db;
    public HomeController(LibraryContext db)
    {
      _db = db;
    }
    
    [HttpGet("/")]
    public ActionResult Index()
    {
      var authorList = _db.Authors.ToList();
      var bookList = _db.Books.ToList();
      Dictionary<string, object> myDictionary = new Dictionary<string, object> ();
      myDictionary.Add("Author", authorList);
      myDictionary.Add("Book", bookList);

      ViewBag.manyBooks = _db.Books;
      ViewBag.manyAuthors = _db.Authors;
      return View(myDictionary);
    }


  }
}