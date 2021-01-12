using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
using Library.Models;
// using System.Linq;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.AspNetCore.Mvc.Rendering;

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
      ViewBag.Authors = _db.Authors;
      return View();
    }

  }
}


// dictionary, list with both Author/Book

//instantiating lists from database _db.author...
// use model.Add...books
// model.Add(authors)
//use one model statement